using BL.Data;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public interface IYearService
    {
        Task<IEnumerable<YearDto>> GetAllAsync();
        Task<YearDto> GetByIdAsync(int id);
        Task CreateAsync(YearDto yearDto, string createdBy);
        Task<bool> UpdateAsync(YearDto yearDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }

    public class YearService : IYearService
    {
        private readonly ApplicationDbContext _context;

        public YearService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<YearDto>> GetAllAsync()
        {
            return await _context.Years
                .Where(y => !y.IsDeleted)
                .Select(y => new YearDto
                {
                    YearId = y.YearId,
                    YearName = y.YearName,
                    YearActive = y.YearActive
                })
                .ToListAsync();
        }

        public async Task<YearDto> GetByIdAsync(int id)
        {
            var year = await _context.Years.FindAsync(id);
            if (year == null)
                return null!;

            return new YearDto
            {
                YearId = year.YearId,
                YearName = year.YearName,
                YearActive = year.YearActive
            };
        }

        public async Task CreateAsync(YearDto yearDto, string createdBy)
        {
            var year = new Year
            {
                YearId = yearDto.YearId,
                YearName = yearDto.YearName,
                YearActive = yearDto.YearActive,
                CreatedBy = createdBy
            };

            await _context.Years.AddAsync(year);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(YearDto yearDto, string updatedBy)
        {
            var existingYear = await _context.Years.FindAsync(yearDto.YearId);
            if (existingYear == null) return false;

            existingYear.YearName = yearDto.YearName;
            existingYear.YearActive = yearDto.YearActive;
            existingYear.UpdatedBy = updatedBy;
            existingYear.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var year = await _context.Years.FindAsync(id);
            if (year == null) return false;

            year.IsDeleted = true;
            year.DeletedBy = deletedBy;
            year.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
