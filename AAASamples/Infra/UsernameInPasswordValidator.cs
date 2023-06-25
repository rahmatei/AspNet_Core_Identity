using Microsoft.AspNetCore.Identity;

namespace AAASamples.Infra
{
    public class UsernameInPasswordValidator : IPasswordValidator<IdentityUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user, string password)
        {
            if(password.Contains(user.UserName, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code="UserNameInPassword",
                    Description="You Can not Use Your Username in your Password"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
