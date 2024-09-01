using Microsoft.AspNetCore.Identity;

namespace WebPizzaSite.Data.Entities.Identity
{
    public class UserRoleEntity : IdentityUserRole<int>
    {
        public UserEntity? User { get; set; }
        public RoleEntity? Role { get; set; }
    }
}