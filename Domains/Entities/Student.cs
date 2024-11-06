using Domains;
using Domains.Entities;

public class Student : AuditableEntity
{
    public int StudentId { get; set; }

    public string UserId { get; set; } = "";
    public ApplicationUser? User { get; set; }

    public int SubStageId { get; set; }
    public SubStage? SubStage { get; set; }

    public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
}