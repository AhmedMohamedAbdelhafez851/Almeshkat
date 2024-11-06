using System.ComponentModel.DataAnnotations;
using Domains;

namespace Domains.Dtos.UserDto
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(50, ErrorMessage = ErrorMessages.MaxLength)]
        public string Country { get; set; } = "";

        [Phone(ErrorMessage = ErrorMessages.InvalidPhoneNumber)]
        public string PhoneNumber { get; set; } = "";
    }
}
