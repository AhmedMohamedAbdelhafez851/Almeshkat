using BL.Data;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
namespace BL.Services
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> GetByIdAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task CreateAsync(DepartmentDto departmentDto, string createdBy);
        Task<bool> UpdateAsync(DepartmentDto departmentDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return await _context.Departments
                .Include(d => d.Year)
                .Where(d => !d.IsDeleted)
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    YearId = d.YearId,
                    YearName = d.Year != null ? d.Year.YearName : null // Null check for Year
                })
                .ToListAsync();
        }


        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Year)
                .Where(d => !d.IsDeleted)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null) return null!;

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                YearId = department.YearId,
                YearName = department.Year?.YearName,
            };
        }

        public async Task CreateAsync(DepartmentDto departmentDto, string createdBy)
        {
            // Check if the YearId exists in the Years table
            var yearExists = await _context.Years.AnyAsync(y => y.YearId == departmentDto.YearId && !y.IsDeleted);
            if (!yearExists)
            {
                throw new InvalidOperationException("The specified YearId does not exist.");
            }

            // Proceed with creating the Department
            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName,
                YearId = departmentDto.YearId,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now
            };

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> UpdateAsync(DepartmentDto departmentDto, string updatedBy)
        {
            var existingDepartment = await _context.Departments.FindAsync(departmentDto.DepartmentId);
            if (existingDepartment == null) return false;

            existingDepartment.DepartmentName = departmentDto.DepartmentName;
            existingDepartment.YearId = departmentDto.YearId;
            existingDepartment.UpdatedBy = updatedBy;
            existingDepartment.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            department.IsDeleted = true;
            department.DeletedBy = deletedBy;
            department.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
