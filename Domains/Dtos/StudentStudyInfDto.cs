using System.ComponentModel.DataAnnotations;

namespace Domains.Dtos
{
    public class StudentStudyInfDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public string UserId { get; set; } = "";

        public string? UserName { get; set; } // Optional for output

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int YearId { get; set; }

        public string? YearName { get; set; } // Optional for output

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int SubStageId { get; set; }

        public string? SubStageName { get; set; } // Optional for output
    }
}
