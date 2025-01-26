using BL.Specifications;
using Domains.Dtos;

namespace BL.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllAsync(int? pageNumber = null, int? pageSize = null);
        //Task<IEnumerable<DepartmentDto>> GetPagedAsync(int pageNumber, int pageSize); // Added GetPagedAsync
        Task CreateAsync(DepartmentDto departmentDto, string createdBy);
        Task<bool> UpdateAsync(DepartmentDto departmentDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
