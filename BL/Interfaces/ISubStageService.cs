using Domains.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ISubStageService
    {
        Task<IEnumerable<SubStageDto>> GetAllAsync();
        Task<SubStageDto> GetByIdAsync(int id);
        Task CreateAsync(SubStageDto SubStageDto, string createdBy);
        Task<bool> UpdateAsync(SubStageDto SubStageDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
