using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Units.Domain.Units;

namespace Nezam.Modular.ESS.Infrastructure.Data.Configurations
{
    public class UnitMemberEntityConfiguration : IEntityTypeConfiguration<UnitMemberEntity>
    {
        public void Configure(EntityTypeBuilder<UnitMemberEntity> builder)
        {
            builder.HasKey(u => u.UnitMemberId);
            // Add other configurations for UnitEntity
        }
    }
}