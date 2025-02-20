using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HOSTEE.Models;


namespace HOSTEE.Pages
{
    public class SignOutModel : PageModel
    {
        private readonly SignInManager<User> sinManager;

        public SignOutModel(SignInManager<User> signInManager)
        {
            sinManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
            await sinManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}
