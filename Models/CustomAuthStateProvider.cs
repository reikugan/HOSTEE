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
            /*
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var user = httpContext.User;
            return new AuthenticationState(user);
            */
            Console.WriteLine("Getting Auth State...");
            if(_currentUser.Identity.IsAuthenticated)
            {
                Console.WriteLine("Already authenticated");
                return new AuthenticationState(_currentUser);
            }

            var user = await GetCurrentUserAsync();
            if(user == null)
            {
                Console.WriteLine("No authenticated user found");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
                }, "cookie");

            var p = new ClaimsPrincipal(identity);
            _currentUser = p;
            Console.WriteLine($"Authentication Updated: {user.UserName}");
            return new AuthenticationState(p);

        }

        /*
        public async Task<bool> MarkAuthenticatedAsync(string email, string password)
        {
            Console.WriteLine("Signing in");

            var user = await _uManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var res = await _sinManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            if (res.Succeeded)
            {
                Console.WriteLine("Password check success");
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }, "cookie");

                _currentUser = new ClaimsPrincipal(identity);
                NotifyAuthenticationStateChanged(Task.FromResult( new AuthenticationState(_currentUser)));

                Console.WriteLine($"Authentication state updated for {user.UserName}");
                Console.WriteLine($"IsAuthenticated? {_currentUser.Identity?.IsAuthenticated}");

                await Task.Delay(200);

                return true;
            }

            return false;
        }
        */

        public async Task<bool> MarkAuthenticatedAsync(string email, string password)
        {
            Console.WriteLine("Signing in");

            var user = await _uManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var res = await _sinManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            if (res.Succeeded)
            {
                Console.WriteLine("Password check success");

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }, "cookie");

                var principal = new ClaimsPrincipal(identity);
                _currentUser = principal;

                if (_contextAccessor.HttpContext != null)
                {
                    var authProps = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                    };

                    if(!_contextAccessor.HttpContext.Response.HasStarted)
                    {
                        await _contextAccessor.HttpContext.SignInAsync("Identity.Application", principal, authProps);
                        Console.WriteLine("User signed with http context");
                    }
                    else
                    {
                        Console.WriteLine("Responce has already started");
                    }
                }
                else
                {
                    Console.WriteLine("context is null");
                }

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
                Console.WriteLine("Authentication state updated!!!");

                return true;
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
