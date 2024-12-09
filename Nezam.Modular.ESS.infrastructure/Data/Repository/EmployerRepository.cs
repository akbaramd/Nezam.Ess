﻿using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Shared.Employer;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class EmployerRepository : EfCoreBonRepository<EmployerEntity, IdentityDbContext>, IEmployerRepository
{
 
}