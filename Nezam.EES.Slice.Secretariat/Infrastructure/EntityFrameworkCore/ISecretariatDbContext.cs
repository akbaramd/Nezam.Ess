using Microsoft.EntityFrameworkCore;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Payeh.SharedKernel.EntityFrameworkCore;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;

/// <summary>
/// Interface for Secretariat DbContext to manage domain entities.
/// </summary>
public interface ISecretariatDbContext : IPayehDbContext
{
    DbSet<DocumentAggregateRoot> Documents { get; set; }
    DbSet<DocumentAttachmentEntity> DocumentAttachments { get; set; }
    DbSet<DocumentReferralEntity> DocumentReferrals { get; set; }
    DbSet<Participant> Participants { get; set; }
}