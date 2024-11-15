using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BL.Interfaces
{
    public interface IStudentsStudyInfService
    {
        Task<StudentStudyInfDto> GetByIdAsync(int id);
        Task<IEnumerable<StudentStudyInfDto>> GetAllAsync();
        Task CreateAsync(StudentStudyInfDto StudentsStudyInfDto, string createdBy);
        Task<bool> UpdateAsync(StudentStudyInfDto StudentsStudyInfDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
