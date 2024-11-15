using Domains.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync();
        Task<SubjectDto> GetByIdAsync(int id);
        Task<SubjectDto> CreateAsync(SubjectDto subjectDto, string createdBy);
        Task<bool> UpdateAsync(SubjectDto subjecttDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
