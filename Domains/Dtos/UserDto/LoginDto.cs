using System.ComponentModel.DataAnnotations;
using Domains;

namespace Domains.Dtos.UserDto
{
    public class LoginDto
    {
        [Required(ErrorMessage = ErrorMessages.Required)]
        [RegularExpression(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = ErrorMessages.InvalidEmail)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = ErrorMessages.PasswordValidation)]
        public string Password { get; set; } = "";
    }
}
