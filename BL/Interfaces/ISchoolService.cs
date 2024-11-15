using Domains.Dtos;


namespace BL.Interfaces
{
    public interface ISchoolService
    {

        Task<SchoolDto> GetByIdAsync(int id);
        Task<IEnumerable<SchoolDto>> GetAllAsync();
        Task<SchoolDto> CreateAsync(SchoolDto schoolDto, string createdBy);
        Task<bool> UpdateAsync(SchoolDto schoolDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }
}

