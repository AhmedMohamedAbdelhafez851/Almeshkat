// TimeTableGroupDetail.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class TimeTableGroupDetail : AuditableEntity
    {
        [Key]
        public int TimTabGroDetId { get; set; }

        public string PeriodName { get; set; } = "";

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool PeriodActive { get; set; }

        public int TimTabGroId { get; set; }
        public TimeTableGroup? TimeTableGroup { get; set; } 

        public ICollection<TimeTable> TimeTables { get; set; } = new List<TimeTable>();
    }
}
