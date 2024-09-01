using Microsoft.AspNetCore.Identity;

namespace WebPizzaSite.Data.Entities.Identity
{
    public class RoleEntity : IdentityRole<int>
    {
        public virtual ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}