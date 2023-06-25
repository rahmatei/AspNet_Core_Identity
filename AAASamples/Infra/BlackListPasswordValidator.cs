using Microsoft.AspNetCore.Identity;

namespace AAASamples.Infra
{
    public class BlackListPasswordValidator<TUser> : IPasswordValidator<TUser>
        where TUser : IdentityUser
    {

        private readonly List<string> _invalidPasswordList = new List<string>
        {
            "password",
            "qwerty",

        };
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            if (_invalidPasswordList.Any(c => string.Equals(c, password, StringComparison.OrdinalIgnoreCase))){
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code="BlackListPassword",
                    Description="You Can Not Use Password In BlackList"

                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
