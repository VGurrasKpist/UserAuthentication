using Microsoft.AspNetCore.Identity;

namespace UserAuthentication.ViewModels
{
    public class UsersIndexViewModel
    {
        public List<UserWithRoles> Users { get; set; } = [];
    }

    public class UserWithRoles
    {
        public IdentityUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}
