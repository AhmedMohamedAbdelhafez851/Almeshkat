using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entities
{
    public class TeacherSubject : AuditableEntity
    {
        public int Id { get; set; }

        public int StaSubjId { get; set; }
        public StageSubject? StageSubject { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }

}

