using Domains.Dtos;

namespace BL.Interfaces
{
    public interface ITeacherSubjectService
    {
        Task<IEnumerable<TeacherSubjectDto>> GetAllAsync();
        Task<TeacherSubjectDto> GetByIdAsync(int teacherId, int subjectId);
        Task CreateAsync(TeacherSubjectDto teacherSubjectDto, string createdBy);
        Task<bool> UpdateAsync(TeacherSubjectDto teacherSubjectDto, string updatedBy);
        Task<bool> DeleteAsync(int teacherId, int subjectId, string deletedBy);
    }
}
