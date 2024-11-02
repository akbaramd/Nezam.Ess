using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.Roles;

public class RoleEntity : Entity<RoleId>
{
    protected RoleEntity()
    {
    }

    public RoleEntity(RoleId id, string name, string title)
    {
        Id = id;
        Name = name;
        Title = title;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public string Name { get; private set; }
    public string Title { get; private set; }

}

public class RoleId : BusinessId<RoleId>
{
}