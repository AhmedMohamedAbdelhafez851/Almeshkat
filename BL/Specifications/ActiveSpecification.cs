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

   
}
