using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ITimeTableGroupService
    {
        Task<TimeTableGroupDto> GetByIdAsync(int id);
        Task<IEnumerable<TimeTableGroupDto>> GetAllAsync();
        Task CreateAsync(TimeTableGroupDto timeTableGroupDto, string createdBy);
        Task<bool> UpdateAsync(TimeTableGroupDto timeTableGroupDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
