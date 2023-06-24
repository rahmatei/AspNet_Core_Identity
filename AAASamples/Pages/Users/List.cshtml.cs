using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AAASamples.Pages.Users
{
    public class ListModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IEnumerable<IdentityUser> Users { get; set; }=Enumerable.Empty<IdentityUser>();

        public ListModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnGet()
        {
            Users = _userManager.Users.ToList();
        }
    }
}