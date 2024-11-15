using Domains.Dtos;
using Domains.Entities;

namespace BL.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> GetByIdAsync(int id);
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task CreateAsync(StudentDto studentDto, string createdBy);
        Task<bool> UpdateAsync(StudentDto studentDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}