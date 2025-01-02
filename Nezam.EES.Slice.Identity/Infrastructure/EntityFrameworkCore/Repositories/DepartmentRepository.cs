using Nezam.EES.Service.Identity.Domains.Departments;
using Nezam.EES.Service.Identity.Domains.Departments.Repositories;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Roles.Repositories;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Repositories;

public class DepartmentRepository : EntityFrameworkRepository<DepartmentEntity,IIdentityDbContext>, IDepartmentRepository
{



    public DepartmentRepository(IEfCoreDbContextProvider<IIdentityDbContext> contextProvider) : base(contextProvider)
    {
    }
}