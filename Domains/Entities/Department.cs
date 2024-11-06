// Department.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Department : AuditableEntity
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string DepartmentName { get; set; } = "";

        public int YearId { get; set; }
        public Year? Year { get; set; }  // Nullable

        public ICollection<SubStage> SubStages { get; set; } = new List<SubStage>();
    }

}
