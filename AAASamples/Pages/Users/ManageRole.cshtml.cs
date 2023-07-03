using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AAASamples.Pages.Users
{
    public class ManageRoleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _role;

        public ManageRoleModel(UserManager<IdentityUser> userManager , RoleManager<IdentityRole> role)
        {
            _userManager = userManager;
            _role = role;
        }
        [BindProperty]
        public List<string> Roles { get; set; }

        [BindProperty]
        public string Id { get; set; }


        public IdentityUser CurrentUser { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public List<string> UserRoles { get; set; }

        public async Task OnGet(string Id)
        {
            CurrentUser = await _userManager.FindByIdAsync(Id);

            UserRoles = (await _userManager.GetRolesAsync(CurrentUser)).ToList();

            AllRoles = _role.Roles.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CurrentUser = await _userManager.FindByIdAsync(Id);
            AllRoles = _role.Roles.ToList();

            foreach (var item in AllRoles)
            {
                if (Roles.Contains(item.Name))
                {
                    if(!(await _userManager.IsInRoleAsync(CurrentUser, item.Name)))
                    {
                        await _userManager.AddToRoleAsync(CurrentUser, item.Name);
                    }
                }
                else
                {
                    if(await _userManager.IsInRoleAsync(CurrentUser, item.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(CurrentUser,item.Name);
                    }
                }
            }
            return RedirectToPage("list");
        }
    }
}
