using Microsoft.AspNetCore.Identity;

namespace WebAppNewsBlog.Data.Entities.Identity
{
    public class UserRoleEntity : IdentityUserRole<int>
    {
        public virtual UserEntity User { get; set; }
        public virtual RoleEntity Role { get; set; }

    }
}
