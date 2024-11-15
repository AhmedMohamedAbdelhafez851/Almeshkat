using Domains.Entities;

namespace BL.Specifications
{
    public class ActiveSpecification : BaseSpecification<Department>
    {
        public ActiveSpecification()
            : base(d => !d.IsDeleted)
        {
        }
    }

    //public class DepartmentByNameSpecification : BaseSpecification<Department>
    //{
    //    public DepartmentByNameSpecification(string departmentName)
    //        : base(d => d.DepartmentName == departmentName && !d.IsDeleted)
    //    {
    //    }
    //}

    //public class DepartmentByIdSpecification : BaseSpecification<Department>
    //{
    //    public DepartmentByIdSpecification(int departmentId)
    //        : base(d => d.DepartmentId == departmentId && !d.IsDeleted)
    //    {
    //    }
    //}
}
