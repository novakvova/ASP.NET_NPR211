using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebPizzaSite.Data.Entities.Identity
{
    public class UserEntity : IdentityUser<int>
    {
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(100)]
        public string? Picture { get; set; }
        public virtual ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}
