using System.Linq.Expressions;
using System.Threading.Tasks;
using BL.Data;
using BL.Interfaces;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<Year> YearRepository { get; }


        public UnitOfWork(ApplicationDbContext context, IRepository<Year> yearRepository)
        {
            _context = context;
            DepartmentRepository = new Repository<Department>(context);
            YearRepository = yearRepository;
        }
        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
