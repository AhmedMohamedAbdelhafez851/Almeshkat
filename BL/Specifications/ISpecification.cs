using System;
using System.Linq.Expressions;

namespace BL.Specifications
{
    public interface ISpecification<T>
    {
        // This property will hold the filtering criteria
        Expression<Func<T, bool>> Criteria { get; }
    }
}
