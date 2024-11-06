using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class School : AuditableEntity
    {
        public int SchoolId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string SchoolName { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLength)]
        public string SchoolLogoImagePath { get; set; } ="";

        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string SchoolLogoName { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Phone(ErrorMessage = ErrorMessages.InvalidPhoneNumber)]
        public string SchoolPhone { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLength)]
        public string SchoolAddress { get; set; } = "";

        [Url(ErrorMessage = ErrorMessages.InvalidUrl)]
        public string SchoolFacebookLink { get; set; } = "";

        [Url(ErrorMessage = ErrorMessages.InvalidUrl)]
        public string SchoolYoutubeLink { get; set; } = "";
    }
}
