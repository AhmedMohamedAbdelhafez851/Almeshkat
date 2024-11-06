using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entities
{
    public class StageSubject : AuditableEntity
    {
        [Key]
        public int StaSubjId { get; set; }

        public int TermId { get; set; }
        public Term? Term { get; set; }   

        public int SubStageId { get; set; }
        public SubStage? SubStage { get; set; } 

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }
}
