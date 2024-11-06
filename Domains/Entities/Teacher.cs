// Teacher.cs
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Teacher : AuditableEntity
    {
        public int TeacherId { get; set; }

        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }

        public string ZoomLink { get; set; } = "";

        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
        public ICollection<TimeTable> TimeTables { get; set; } = new List<TimeTable>();
    }
}
