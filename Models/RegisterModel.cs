using System.ComponentModel.DataAnnotations;

namespace HOSTEE.Models
{
    public class RegisterModel
    {

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not much!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public RegisterModel()
        {

            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }
}
