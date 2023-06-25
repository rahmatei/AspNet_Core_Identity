using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AAASamples.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _users;

        public CreateModel(UserManager<IdentityUser> users)
        {
            _users = users;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new()
                {
                    UserName = Username,
                    Email = Email
                };

                IdentityResult result = await _users.CreateAsync(user, Password);

                if(result.Succeeded)
                {
                    return RedirectToPage("List");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return Page();
        }
        
    }
}
