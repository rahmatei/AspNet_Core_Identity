using Microsoft.AspNetCore.Identity;

namespace AAASamples.Infra
{
    public class CustomUserValidator : IUserValidator<IdentityUser>
    {
        private readonly string organizationEmail = "@nikamooz.com";
        public Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
        {
            if(!user.Email.EndsWith(organizationEmail,StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code="InvalidEmail",
                    Description="You Should Use Your Organizational Email"
                    
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
