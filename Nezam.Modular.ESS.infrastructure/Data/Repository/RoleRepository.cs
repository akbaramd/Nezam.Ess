using Nezam.Modular.ESS.Identity.Domain.Roles;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class RoleRepository : EntityFrameworkRepository<RoleEntity,AppDbContext>, IRoleRepository
{
    public RoleRepository(IUnitOfWorkManager work) : base(work)
    {
    }

    public Task<RoleEntity?> FindRoleByIdAsync(RoleId roleId)
    {
        return FindOneAsync(x=>x.RoleId == roleId);
    }
}