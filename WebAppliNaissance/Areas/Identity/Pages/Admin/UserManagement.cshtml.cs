using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WebAppliNaissance.Areas.Identity.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UserManagementModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<UserWithRoles> Users { get; set; } = new();

        public class UserWithRoles
        {
            public IdentityUser User { get; set; }
            public IList<string> Roles { get; set; }
        }

        public async Task OnGetAsync()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Users.Add(new UserWithRoles
                {
                    User = user,
                    Roles = roles
                });
            }
        }
    }
}