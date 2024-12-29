using Microsoft.EntityFrameworkCore;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Configurations;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;

/// <summary>
/// Extension methods for applying configurations for the Secretariat slice.
/// </summary>
public static class Extensions
{
    public static void ApplySecretariatConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocumentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentAttachmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentReferralEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ParticipantEntityConfiguration());
    }
}