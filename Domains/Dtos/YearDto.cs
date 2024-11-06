using System.ComponentModel.DataAnnotations;
using Domains;

namespace Domains.Dtos
{
    public class YearDto
    {
        public int YearId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string YearName { get; set; } = "";

        public bool YearActive { get; set; }
    }
}
