using System.ComponentModel.DataAnnotations;

namespace Conit.WEB.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please, enter email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please, confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please, enter address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please, enter your name")]
        public string Name { get; set; }
    }
}