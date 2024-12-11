using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Units.Domain.Units;

namespace Nezam.Modular.ESS.Infrastructure.Data.Configurations
{
    public class UnitEntityConfiguration : IEntityTypeConfiguration<UnitEntity>
    {
        public void Configure(EntityTypeBuilder<UnitEntity> builder)
        {
            builder.HasKey(u => u.UnitId);
            // Add other configurations for UnitEntity
        }
    }
}