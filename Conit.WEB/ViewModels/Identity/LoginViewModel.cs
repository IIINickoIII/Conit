using System.ComponentModel.DataAnnotations;

namespace Conit.WEB.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please, enter email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}