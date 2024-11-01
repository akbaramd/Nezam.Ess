using Bonyan.Modularity;
using Nezam.Modular.ESS.Identity.infrastructure;

namespace Nezam.Modular.ESS.WebApi;


public class NezamEssModule : WebModule
{
    public NezamEssModule()
    {
        DependOn<NezamEssIdentityInfrastructureModule>();
    }    
    
    
}