using Nezam.EES.Slice.Secretariat.Domains.Documents.Repositories;
using Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Repositories;

namespace Nezam.EES.Slice.Secretariat;

public static class IdentitySliceExtensions
{
    public static IServiceCollection AddSecretariatSlice(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddTransient<IDocumentRepository, DocumentRepository>();
        services.AddTransient<IParticipantRepository,ParticipantRepository>();
        return services;
    }
}