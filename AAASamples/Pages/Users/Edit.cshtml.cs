using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AAASamples.Pages
{
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _users;

        public EditModel(UserManager<IdentityUser> users)
        {
            _users = users;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public string Id { get; set; }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }

        public async Task OnGet(string id)
        {
            var user = await _users.FindByIdAsync(id);
            Username = user.UserName;
            Email= user.Email;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _users.FindByIdAsync(Id);
                user.UserName = Username;
                user.Email = Email;
                var result =await _users.UpdateAsync(user);
                if (result.Succeeded && !string.IsNullOrEmpty(Password))
                {
                    await _users.RemovePasswordAsync(user);
                    result = await _users.AddPasswordAsync(user, Password);
                }
                if (result.Succeeded)
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
