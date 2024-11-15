using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IStageSubjectService
    {
        Task<StageSubjectsDto?> GetByIdAsync(int id);
        Task<IEnumerable<StageSubjectsDto>> GetAllAsync();
        Task CreateAsync(StageSubjectsDto stageSubjectsDto, string createdBy);
        Task<bool> UpdateAsync(StageSubjectsDto stageSubjectsDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
