using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Units.Domain.Member;

namespace Nezam.Modular.ESS.Infrastructure.Data.Configurations
{
    public class MemberEntityConfiguration : IEntityTypeConfiguration<MemberEntity>
    {
        public void Configure(EntityTypeBuilder<MemberEntity> builder)
        {
            builder.HasKey(u => u.MemberId);
            // Add other configurations for UnitEntity
        }
    }
}