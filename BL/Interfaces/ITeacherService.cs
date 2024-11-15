using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto> GetByIdAsync(int id);
        Task CreateAsync(TeacherDto teacherDto, string createdBy);
        Task<bool> UpdateAsync(TeacherDto teacherDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
