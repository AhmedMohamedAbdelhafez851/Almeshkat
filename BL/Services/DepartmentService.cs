using BL.Interfaces;
using BL.Specifications;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var specification = new ActiveSpecification();
            var departments = await _unitOfWork.DepartmentRepository
                .GetBySpecificationAsync(specification, query => query.Include(d => d.Year));

            return departments.Select(MapToDto);
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository
                .GetByIdAsync(id, query => query.Include(d => d.Year));

            return department == null ? null : MapToDto(department);
        }

        public async Task CreateAsync(DepartmentDto departmentDto, string createdBy)
        {

            if (!await YearExistsAsync(departmentDto.YearId))
            {
                throw new ArgumentException("Specified year does not exist or is deleted.");
            }

            if (await IsDuplicateDepartmentNameAsync(departmentDto))
            {
                throw new ArgumentException("Department name already exists.");
            }

            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName,
                YearId = departmentDto.YearId,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.DepartmentRepository.AddAsync(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(DepartmentDto departmentDto, string updatedBy)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(departmentDto.DepartmentId);
            if (department == null)
            {
                return false;
            }

            // Check if Year exists
            if (!await YearExistsAsync(departmentDto.YearId))
            {
                throw new ArgumentException("Specified year does not exist or is deleted.");
            }

            // Check for duplicate Department name
            if (await IsDuplicateDepartmentNameAsync(departmentDto, department))
            {
                throw new ArgumentException("Department name already exists.");
            }

            department.DepartmentName = departmentDto.DepartmentName;
            department.YearId = departmentDto.YearId;
            department.UpdatedBy = updatedBy;
            department.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return false;
            }
            department.IsDeleted = true;
            department.DeletedBy = deletedBy;
            department.DeletedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        // Other methods like UpdateAsync, DeleteAsync with logging...
        private static DepartmentDto MapToDto(Department department) =>
            new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                YearId = department.YearId,
                YearName = department.Year?.YearName ?? string.Empty
            };

        private async Task<bool> YearExistsAsync(int yearId)
        {
            return await _unitOfWork.YearRepository.AnyAsync(y => y.YearId == yearId && !y.IsDeleted);
        }

        private async Task<bool> IsDuplicateDepartmentNameAsync(DepartmentDto departmentDto, Department? existingDepartment = null)
        {
            return await _unitOfWork.DepartmentRepository.AnyAsync(d =>
                d.DepartmentName == departmentDto.DepartmentName &&
                (existingDepartment == null || d.DepartmentId != existingDepartment.DepartmentId) &&
                !d.IsDeleted);
        }

      
    }
}
