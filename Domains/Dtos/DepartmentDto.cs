using System.ComponentModel.DataAnnotations;

namespace Domains.Dtos
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        //[MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        //[MinLength(3 , ErrorMessage =ErrorMessages.MinLength)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = ErrorMessages.StringLengthRange)]
        public string DepartmentName { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int YearId { get; set; }
        
        public string? YearName { get; set; }

    }
}
