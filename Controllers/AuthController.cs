using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HOSTEE.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HOSTEE.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> sinManager;
        private readonly UserManager<ApplicationUser> uManager;

        public AuthController(SignInManager<ApplicationUser> signManager, UserManager<ApplicationUser> usManager)
        {
            this.sinManager = signManager;
            this.uManager = usManager;
        }

        [HttpPost("login)")]
        public async Task<IActionResult> Login([FromBody] SignInModel model)
        {
            var user = await uManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return Unauthorized("No user with such email address");
            }

            var res = await sinManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!res.Succeeded)
            {
                return Unauthorized("Invalid password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var identity = new ClaimsIdentity(claims, "Identity.Application");
            var p = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            { 
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            await HttpContext.SignInAsync("Identity.Application", p, authProperties);
            return Ok(new { Message = "Login success" });

        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await sinManager.SignOutAsync();
            return Ok(new { Message = "logout sucess" });
        }
    }
}
