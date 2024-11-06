using System.ComponentModel.DataAnnotations;

namespace Domains.Dtos
{
    public class StageDto  
    {
        public int StageId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string StageName { get; set; } = "";
    }
}
