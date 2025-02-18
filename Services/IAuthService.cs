using HOSTEE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
namespace HOSTEE.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<ApplicationUser?> GetCurrentUserAsync();
    }

    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> sinManager;
        private readonly UserManager<ApplicationUser> uManager;
        private readonly AuthenticationStateProvider authSP;

        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManger, AuthenticationStateProvider asp)
        {
            sinManager = signInManager;
            uManager = userManger;
            authSP = asp;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await uManager.FindByNameAsync(email);
            if (user == null)
            {
                return false;
            }
            var res = await sinManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
            if (res.Succeeded)
            {
                if (authSP is CustomAuthStateProvider customProvider)
                {
                    await customProvider.RefreshAuthenticationState();
                }
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await sinManager.SignOutAsync();
            if (authSP is CustomAuthStateProvider customProvider)
            {
                await customProvider.RefreshAuthenticationState();
            }
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var authState = await authSP.GetAuthenticationStateAsync();
            var user = authState.User;
            if(user.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            return await uManager.GetUserAsync(user);
        }


    }
}
