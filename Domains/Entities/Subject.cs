// Subject.cs
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Subject : AuditableEntity
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = "";

        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
        public ICollection<StageSubject> StageSubjects { get; set; } = new List<StageSubject>();
        public ICollection<TimeTable> TimeTables { get; set; } = new List<TimeTable>();
    }
}
