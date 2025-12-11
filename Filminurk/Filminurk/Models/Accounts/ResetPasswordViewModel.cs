using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Accounts
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Paroolid ei kattu, palun kirjuta uuesti.")]
        public string ConfirmNewPassword { get; set; }
        public string Token { get; set; }
    }
}
