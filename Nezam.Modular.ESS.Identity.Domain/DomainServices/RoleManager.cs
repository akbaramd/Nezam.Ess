using Bonyan.Layer.Domain.Services;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.DomainServices;

public class RoleManager : DomainService
{
    public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
    public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();


    public async Task<bool> CreateAsync(string name, string title)
    {
        try
        {
            if (await RoleRepository.ExistsAsync(x=>x.Name.Equals(name)))
            {
                return false;
            }
            
            var res = await RoleRepository.AddAsync(new RoleEntity(RoleId.CreateNew(),name,title),true);
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return false;
        }
    }
    
    
    public async Task<bool> UpdateAsync(RoleEntity entity)
    {
        try
        {
         
             await RoleRepository.UpdateAsync(entity,true);
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return false;
        }
    }
    public async Task<RoleEntity?> FindByNameAsync(string name)
    {
        try
        {
            return await RoleRepository.FindOneAsync(x=>x.Name.Equals(name));
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return null;
        }
    }
    
  

}