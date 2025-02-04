using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HOSTEE.Models
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly UserManager<ApplicationUser> _uManager;
        private readonly SignInManager<ApplicationUser> _sinManager;
        private IHttpContextAccessor _contextAccessor;

        public CustomAuthStateProvider(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> sinManager, IHttpContextAccessor contextAccessor)
        {
            _uManager = userManager;
            _sinManager = sinManager;
            _contextAccessor = contextAccessor;

        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var user = await _uManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
                }, "cookie");

            var p = new ClaimsPrincipal(identity);
            return new AuthenticationState(p);
        }

        public async Task<bool> MarkAuthenticatedAsync(string email, string password)
        {
            var res = await _sinManager.PasswordSignInAsync(email, password, isPersistent: true, lockoutOnFailure: false);
            if (res.Succeeded)
            {
                var user = await _uManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
                    }, "cookie");

                    _currentUser = new ClaimsPrincipal(identity);

                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    return true;
                }
            }
            return false;

        }

        public async Task LogoutAsync()
        {
            await _sinManager.SignOutAsync();
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }
    }
}
