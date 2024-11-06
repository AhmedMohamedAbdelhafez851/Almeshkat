using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entities
{
    public class TimeTableGroup : AuditableEntity
    {
        [Key]
        public int TimTabGroId { get; set; }

        public string TimTabGroName { get; set; } = "";

        public int TermId { get; set; }
        public Term? Term { get; set; }

        public ICollection<TimeTableGroupDetail> TimeTableGroupDetails { get; set; } = new List<TimeTableGroupDetail>();
    }
}
