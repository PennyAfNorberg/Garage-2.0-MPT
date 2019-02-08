using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Garage_2._0_MPT
{
    public class DoesNotContainPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var username = await manager.GetUserNameAsync(user);

            if (username == password)
                return IdentityResult.Failed(new IdentityError { Description = "Password Ska inte innehålla inlogg" });
            if (password.Contains("password"))
                return IdentityResult.Failed(new IdentityError { Description = "Password ska inte innhålla password" });

            return IdentityResult.Success;
        }
    }
}
