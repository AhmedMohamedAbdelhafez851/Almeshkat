using Domains.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ITermService
    {
        Task<IEnumerable<TermDto>> GetAllAsync();
        Task<TermDto> GetByIdAsync(int id);
        Task CreateAsync(TermDto TermDto, string createdBy);
        Task<bool> UpdateAsync(TermDto TermDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
