// Department.cs

namespace Domains.Entities
{
    public class Department : AuditableEntity
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = "";

        public int YearId { get; set; }
        public Year? Year { get; set; }  // Nullable

        public ICollection<SubStage> SubStages { get; set; } = new List<SubStage>();

        

       
    }

}
