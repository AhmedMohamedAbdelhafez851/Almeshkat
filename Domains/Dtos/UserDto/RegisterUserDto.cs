using System.ComponentModel.DataAnnotations;
using Domains;

namespace Domains.Dtos.UserDto
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = ErrorMessages.Required)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmail)]
        [StringLength(256, ErrorMessage = ErrorMessages.MaxLength)]
        [RegularExpression(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = ErrorMessages.InvalidEmail)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        [RegularExpression(@"^[\p{L} \.'-]+$",
            ErrorMessage = "الاسم الكامل يحتوي على أحرف غير صالحة.")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(50, ErrorMessage = ErrorMessages.MaxLength)]
        public string Country { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Phone(ErrorMessage = ErrorMessages.InvalidPhoneNumber)]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = ErrorMessages.PasswordLength)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = ErrorMessages.PasswordValidation)]
        public string Password { get; set; } = "";
    }
}
