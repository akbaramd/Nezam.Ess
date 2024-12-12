using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class EngineerRepository : EntityFrameworkRepository<EngineerEntity, AppDbContext>, IEngineerRepository
{
    public EngineerRepository(AppDbContext context) : base(context)
    {
    }
}