using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HOSTEE.Models
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private IHttpContextAccessor _contextAccessor;

        public CustomAuthStateProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /*
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

            var user = _contextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            /*
            var appUser = await _uManager.GetUserAsync(user);
            if (appUser == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
 

            return new AuthenticationState(user);
   
        }

    

        public async Task<bool> LoginAsync(string email, string password, bool remember = false)
        {
            var user = await _uManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            await _sinManager.SignOutAsync();

            var result = await _sinManager.PasswordSignInAsync(user, password, isPersistent: remember, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var p = await _sinManager.CreateUserPrincipalAsync(user);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(p)));
                return true;
            }
            return false;
        }



        public async Task LogoutAsync()
        {
            await _sinManager.SignOutAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

        */

        //private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = _contextAccessor.HttpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());
            return Task.FromResult(new AuthenticationState(user));
        }

        public void NotifyStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task RefreshAuthenticationState()
        {
            var state = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        internal void SetAuthenticationState(ClaimsPrincipal principal)
        {
            //currentUser = principal;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
