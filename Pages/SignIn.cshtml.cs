using HOSTEE.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HOSTEE.Pages
{
    public class SignInModel : PageModel
    {
        private readonly SignInManager<User> sinManager;
        private readonly UserManager<User> uManager;
        private readonly ILogger<LoginModel> _logger;

        public SignInModel(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<LoginModel> logger)
        {
            sinManager = signInManager;
            uManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }


        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //Console.WriteLine("OnPostAsync reached!");
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState error: {error.ErrorMessage}");
                }
            }
            
            if (ModelState.IsValid)
            {
                var result = await sinManager.PasswordSignInAsync(Input.Email, Input.Password, true, lockoutOnFailure: false);
                //Console.WriteLine("ModelState is Valid, user is not null");

                
                if (result.Succeeded)
                {
                    var user = await uManager.FindByEmailAsync(Input.Email);
                    var claims = await uManager.GetClaimsAsync(user);

                    var existingClaims = await uManager.GetClaimsAsync(user);
                    var oldclaims = existingClaims.Where(c => c.Type == "Name" || c.Type == "Birthday").ToList();
                    if (oldclaims.Any())
                    {
                        await uManager.RemoveClaimsAsync(user, oldclaims);
                    }

                    var newClaims = new List<Claim>
                        {
                            new Claim("Name", user.Name ?? ""),
                            new Claim("Birthday", user.DateOfBirth.ToString())
                        };

                    await uManager.AddClaimsAsync(user, newClaims);

                    await sinManager.RefreshSignInAsync(user);

                    _logger.LogInformation("User logged in.");
                    Console.WriteLine($"ON POST ASYNC: signIn success. Url: {returnUrl}.");
                    return LocalRedirect(returnUrl + "?forceRefresh=true");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    Console.WriteLine($"ON POST ASYNC: SignIn failed! Url: {returnUrl}.");
                    return Page();
                }
            }

            return Page();
        }
    }
}