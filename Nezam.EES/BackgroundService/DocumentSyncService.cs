using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Application.Dto;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Repositories;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EEs.Slice.Secretariat.Services
{
    /// <summary>
    /// Background service to synchronize documents from the tbl_ees_documents table to the domain repository.
    /// </summary>
    public class DocumentSyncService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        // Admin identification - adjust as per your setup
        private readonly long _adminOzviyatNo = 123456789; // Replace with actual admin ozviyat_no

        public DocumentSyncService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

       protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    // Run the synchronization process until the service is stopped
    while (!stoppingToken.IsCancellationRequested)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbConnection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
                var documentRepository = scope.ServiceProvider.GetRequiredService<IDocumentRepository>();
                var userDomainService = scope.ServiceProvider.GetRequiredService<IUserDomainService>();
                var participantRepository = scope.ServiceProvider.GetRequiredService<IParticipantRepository>();
                var unitOfWorkManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

                Participant adminParticipant;

                // Ensure admin participant exists
                using (var uow = unitOfWorkManager.Begin())
                {
                    var adminUser = await userDomainService.GetUserByUsernameAsync(UserNameId.NewId("admin"));
                    if (!adminUser.IsSuccess || adminUser.Data == null)
                    {
                        Console.WriteLine("Admin user not found. Skipping synchronization.");
                        return;
                    }

                    adminParticipant = await participantRepository.FindOneAsync(x => x.UserId == adminUser.Data.UserId);
                    if (adminParticipant == null)
                    {
                        Console.WriteLine($"Participant for admin user not found. Skipping synchronization.");
                        return;
                    }

                    await uow.CommitAsync(stoppingToken);
                }

                // Begin a unit of work for document synchronization
                using (var uow = unitOfWorkManager.Begin())
                {
                    const string query =
                        "SELECT id, title, ozviyat_no, filePath, trackingCode, createdAt, state, type FROM tbl_ees_documents";
                    var documents = await dbConnection.QueryAsync<DocumentDto>(query);

                    Console.WriteLine($"Fetched {documents.Count()} documents from tbl_ees_documents.");

                    foreach (var doc in documents)
                    {
                        try
                        {
                            // Check if the document already exists
                            var existingDocument = await documentRepository.FindOneAsync(x => x.TrackingCode == doc.trackingCode);
                            if (existingDocument != null)
                            {
                                Console.WriteLine($"Document with TrackingCode '{doc.trackingCode}' already exists. Skipping.");
                                continue;
                            }

                            // Find the user by ozviyat_no
                            var userResult = await userDomainService.GetUserByUsernameAsync(UserNameId.NewId(doc.ozviyat_no.ToString()));
                            if (!userResult.IsSuccess || userResult.Data == null)
                            {
                                Console.WriteLine($"User with OzviyatNo '{doc.ozviyat_no}' not found. Skipping.");
                                continue;
                            }

                            var user = userResult.Data;

                            // Find the participant of the user
                            var userParticipant = await participantRepository.FindOneAsync(x => x.UserId == user.UserId);
                            if (userParticipant == null)
                            {
                                Console.WriteLine($"Participant for UserId '{user.UserId}' not found. Skipping.");
                                continue;
                            }

                            // Ensure title length fits within the column limit
                            const int maxTitleLength = 200; // Replace with the actual maximum length of the column
                            var title = doc.title.Length > maxTitleLength ? doc.title.Substring(0, maxTitleLength) : doc.title;

                            // Create a new document instance
                            var newDocument = new DocumentAggregateRoot(
                                title: title,
                                content: string.Empty, // Adjust content as needed
                                senderUserId: userParticipant.ParticipantId,
                                reciverUserId: adminParticipant.ParticipantId,
                                type: DocumentType.Incoming,
                                letterNumber: doc.Id
                            );
                            newDocument.UpdateLetterDate(doc.createdAt);
                            newDocument.SetTrackingCode(doc.trackingCode);
                            newDocument.Publish(userParticipant.ParticipantId);

                            // Add an attachment if the file path is valid
                            if (!string.IsNullOrWhiteSpace(doc.filePath))
                            {
                                var filePath = doc.filePath;
                                var fileName = Path.GetFileName(filePath);
                                var fileType = Path.GetExtension(filePath)?.TrimStart('.').ToUpperInvariant() ?? "UNKNOWN";
                                const long fileSize = 1000; // Replace with actual size if known

                                newDocument.AddAttachment(fileName, fileType, fileSize, filePath);
                            }

                            // Add the document to the repository
                            await documentRepository.AddAsync(newDocument, true);

                            Console.WriteLine($"Added new document with TrackingCode '{doc.trackingCode}'.");
                        }
                        catch (Exception innerEx)
                        {
                            // Log specific error for the document
                            Console.WriteLine($"Error processing document with TrackingCode '{doc.trackingCode}': {innerEx.Message}");
                            // Optionally, continue with the next document
                        }
                    }

                    // Commit all changes for this batch of documents
                    await uow.CommitAsync(stoppingToken);

                    Console.WriteLine("Document synchronization committed successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any high-level exceptions
            Console.WriteLine($"Error during document synchronization: {ex.Message}");
        }

        // Delay before the next synchronization cycle
        await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Adjust the interval as needed
    }
}

    }

    // DTO for mapping
}

public class DocumentDto
{
    public int Id { get; set; }
    public string title { get; set; }
    public long ozviyat_no { get; set; }
    public string filePath { get; set; }
    public string trackingCode { get; set; }
    public DateTime createdAt { get; set; }
}