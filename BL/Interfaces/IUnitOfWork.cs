using System.Linq.Expressions;
using System.Threading.Tasks;
using BL.Interfaces;
using Domains.Entities;

namespace BL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Year> YearRepository { get; }
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class;


        Task SaveChangesAsync();
    }
}
