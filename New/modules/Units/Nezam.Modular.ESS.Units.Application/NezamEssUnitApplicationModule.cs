using Bonyan.Modularity.Abstractions;
// using Nezam.Modular.ESS.Identity.Application.Employers;
// using Nezam.Modular.ESS.Identity.Application.Employers.Jobs;
// using Nezam.Modular.ESS.Identity.Application.Engineers;
// using Nezam.Modular.ESS.Identity.Application.Engineers.Jobs;
// using Nezam.Modular.ESS.Identity.Application.Users;
using Nezam.Modular.ESS.Units.Domain;

namespace Nezam.Modular.ESS.Identity.Application;

public class NezamEssUnitApplicationModule : BonModule
{
    public NezamEssUnitApplicationModule()
    {
        DependOn<NezamEssUnitsDomainModule>();
    }

}