using System.ComponentModel.DataAnnotations;

namespace Domains.Dtos
{
    public class SubStageDto
    {
        public int SubStageId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string SubStageName { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int StageId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int DepartmentId { get; set; }
        
        public string? StageName { get; set; }

        public string? DepartmentName { get; set; }  
    }
}
