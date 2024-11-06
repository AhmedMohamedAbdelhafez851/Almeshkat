// StudentsStudyInfo.cs
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class StudentsStudyInfo : AuditableEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }

        public int YearId { get; set; }
        public Year? Year { get; set; }

        public int SubStageId { get; set; }
        public SubStage? SubStage { get; set; }
    }
}
