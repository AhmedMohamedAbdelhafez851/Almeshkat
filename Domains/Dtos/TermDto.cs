using System.ComponentModel.DataAnnotations;

namespace Domains.Dtos
{
    public class TermDto
    {
        public int TermId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string TermName { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessages.InvalidDate)]
        public DateTime TermStartDate { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [DataType(DataType.Date, ErrorMessage = ErrorMessages.InvalidDate)]
        public DateTime TermEndDate { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int YearId { get; set; }

        public string? YearName { get; set; }
    }
}
