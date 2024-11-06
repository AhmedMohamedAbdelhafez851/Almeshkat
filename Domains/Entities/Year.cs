using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Year : AuditableEntity
    {
        public int YearId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string YearName { get; set; } = "";

        public bool YearActive { get; set; }

        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Term> Terms { get; set; } = new List<Term>();
        public ICollection<StudentsStudyInfo> StudentsStudyInfos { get; set; } = new List<StudentsStudyInfo>();
    }

}
