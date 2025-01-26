using BL.Healper;
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

        [Cacheable("Departments_All_{pageNumber}_{pageSize}", 600)] // Cache for 10 minutes
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync(int? pageNumber = null, int? pageSize = null)
        {
            var specification = new ActiveSpecification();
            var departments = await _unitOfWork.DepartmentRepository.GetPagedOrAllAsync(
                specification,
                query => query.Include(d => d.Year).AsNoTracking(),
                pageNumber,
                pageSize
            );

            return departments.Select(MapToDto);
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository
                .GetByIdAsync(id, query => query.AsNoTracking().Include(d => d.Year));

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
            if (department == null) return false;

            if (!await YearExistsAsync(departmentDto.YearId))
            {
                throw new ArgumentException("Specified year does not exist or is deleted.");
            }

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
            if (department == null) return false;

            SoftDeleteHelper.ApplySoftDelete(department, deletedBy);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        //[Cacheable("Departments_Paged_{pageNumber}_{pageSize}", 600)] // Cache for 10 minutes
        //public async Task<IEnumerable<DepartmentDto>> GetPagedAsync(int pageNumber, int pageSize)
        //{
        //    var specification = new ActiveSpecification();
        //    var departments = await _unitOfWork.DepartmentRepository.GetPagedAsync(
        //        specification,
        //        query => query.Include(d => d.Year),
        //        pageNumber,
        //        pageSize
        //    );

        //    return departments.Select(MapToDto);
        //}

        private static DepartmentDto MapToDto(Department department) =>
            new()
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
