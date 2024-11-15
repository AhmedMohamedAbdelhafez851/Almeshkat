using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IStageService
    {
        Task<StageDto> GetByIdAsync(int id);
        Task<IEnumerable<StageDto>> GetAllAsync();
        Task CreateAsync(StageDto StageDto, string createdBy);
        Task<bool> UpdateAsync(StageDto StageDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
