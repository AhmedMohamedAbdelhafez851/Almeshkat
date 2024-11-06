// SubStage.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class SubStage : AuditableEntity
    {
        public int SubStageId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string SubStageName { get; set; } = "";

        public int StageId { get; set; }
        public Stage? Stage { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<StudentsStudyInfo> StudentsStudyInfos { get; set; } = new List<StudentsStudyInfo>();
        public ICollection<StageSubject> StageSubjects { get; set; } = new List<StageSubject>();
        public ICollection<TimeTable> TimeTables { get; set; } = new List<TimeTable>();
    }
}
