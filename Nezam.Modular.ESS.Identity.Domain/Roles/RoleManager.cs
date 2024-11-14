using Bonyan.Layer.Domain.Services;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.Roles
{
    public class RoleManager : BonDomainService
    {
        public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
        public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

        // ایجاد یک نقش جدید
        public async Task<bool> CreateAsync(RoleId name, string title)
        {
            try
            {
                if (await RoleRepository.ExistsAsync(x => x.Id.Equals(name)))
                {
                    Logger.LogWarning($"Role with name '{name}' already exists.");
                    return false;
                }

                var role = new RoleEntity(name, title);
                await RoleRepository.AddAsync(role, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error creating role.");
                return false;
            }
        }

        // به‌روزرسانی جزئیات نقش
        public async Task<bool> UpdateAsync(RoleEntity role, string newTitle)
        {
            try
            {
                role.UpdateTitle(newTitle);
                await RoleRepository.UpdateAsync(role, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error updating role.");
                return false;
            }
        }

        // پیدا کردن نقش از طریق نام آن
        public async Task<RoleEntity?> FindByNameAsync(string name)
        {
            try
            {
                return await RoleRepository.FindOneAsync(x => x.Id.Equals(name));
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error finding role by name.");
                return null;
            }
        }

        // حذف نقش و اختیاری حذف از تمام کاربران
        public async Task<bool> DeleteAsync(RoleEntity role, bool removeFromUsers = true)
        {
            try
            {
                if (removeFromUsers)
                {
                    // پیدا کردن کاربران دارای این نقش و حذف نقش از کاربران
                    var usersWithRole = await UserRepository.FindAsync(u => u.Roles.Any(c=>c.RoleId == role.Id));
                    foreach (var user in usersWithRole)
                    {
                        user.RemoveRole(role.Id); 
                    }

                    await UserRepository.UpdateRangeAsync(usersWithRole, true); // Batch update
                }

                await RoleRepository.DeleteAsync(role, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error deleting role.");
                return false;
            }
        }

        // دریافت تمام نقش‌ها و کاربران مرتبط با هر نقش
        public async Task<Dictionary<RoleEntity, List<UserEntity>>> GetRolesWithUsersAsync()
        {
            try
            {
                var rolesWithUsers = new Dictionary<RoleEntity, List<UserEntity>>();
                var roles = await RoleRepository.FindAsync(x=>true);

                foreach (var role in roles)
                {
                    var users = await UserRepository.FindAsync(u => u.Roles.Any(x=>x.RoleId==role.Id));
                    rolesWithUsers[role] = users.ToList();
                }

                return rolesWithUsers;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error retrieving roles with users.");
                return new Dictionary<RoleEntity, List<UserEntity>>();
            }
        }

        // بررسی وجود یک نقش با نام مشخص
        public async Task<bool> RoleExistsAsync(RoleId roleId)
        {
            return await RoleRepository.ExistsAsync(r => r.Id==roleId);
        }
    }
}
