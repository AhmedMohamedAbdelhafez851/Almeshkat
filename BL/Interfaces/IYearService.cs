using Domains.Dtos;

namespace BL.Interfaces
{
    public interface IYearService
    {
        Task<IEnumerable<YearDto>> GetAllAsync();
        Task<YearDto> GetByIdAsync(int id);
        Task CreateAsync(YearDto yearDto, string createdBy);
        Task<bool> UpdateAsync(YearDto yearDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}
