using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entities
{
    public class TrackUsersProcess
    {
        public int TraPerProid { get; set; } // Primary key
        public string UserId { get; set; } = "";
        public string ProcessName { get; set; } = "";
        public DateTime ProcessDate { get; set; } = DateTime.UtcNow.Date;
        public TimeSpan ProcessTime { get; set; } = DateTime.UtcNow.TimeOfDay;
        public string DeviceIp { get; set; } = "";
        public string TableName { get; set; } = "";
        public int RowNumber { get; set; }
    }
}
