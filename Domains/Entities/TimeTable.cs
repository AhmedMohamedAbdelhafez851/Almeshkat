using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entities
{
    public class TimeTable : AuditableEntity
    {
        public int TimeTableId { get; set; }

        public int SubStageId { get; set; }
        public SubStage? SubStage { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public string Day { get; set; } = "";

        public int TimTabGroDetId { get; set; }
        public TimeTableGroupDetail? TimeTableGroupDetail { get; set; }
    }
}
