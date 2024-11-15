using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Specifications;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetBySpecificationAsync(
           ISpecification<T> specification,
           Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            var query = ApplySpecification(specification);

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<T?> GetByIdAsync(int id,Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            var query = _context.Set<T>().Where(e => EF.Property<int>(e, nameof(Department.DepartmentId)) == id);

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        // Implement AnyAsync
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification) =>
            _context.Set<T>().Where(specification.Criteria);

      
    }
}
