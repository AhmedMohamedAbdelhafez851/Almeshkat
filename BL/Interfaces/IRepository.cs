using BL.Specifications;
using System.Linq.Expressions;

namespace BL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetBySpecificationAsync(
            ISpecification<T> specification,
            Func<IQueryable<T>, IQueryable<T>>? include = null);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(
            int id,
            Func<IQueryable<T>, IQueryable<T>>? include = null);

        Task SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // Task<IEnumerable<T>> GetPagedAsync(ISpecification<T> specification,
        //Func<IQueryable<T>, IQueryable<T>>? include = null,
        //int pageNumber = 1, int pageSize = 10);

        Task<IEnumerable<T>> GetPagedOrAllAsync(
           ISpecification<T> specification,
           Func<IQueryable<T>, IQueryable<T>>? include = null,
           int? pageNumber = null,
           int? pageSize = null);
    }


}

