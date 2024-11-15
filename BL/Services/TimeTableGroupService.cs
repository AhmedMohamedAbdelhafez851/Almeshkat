using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class TimeTableGroupService : ITimeTableGroupService
    {
        private readonly ApplicationDbContext _context;

        public TimeTableGroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeTableGroupDto>> GetAllAsync()
        {
            return await _context.TimeTableGroups
                .Include(d => d.Term)
                .Where(d => !d.IsDeleted)
                .Select(d => new TimeTableGroupDto
                {
                    Id = d.TimTabGroId,
                    TimTabGroName = d.TimTabGroName,
                    TermId = d.Term!.TermId,
                    TermName = d.Term.TermName
                })
                .ToListAsync();
        }

        public async Task<TimeTableGroupDto> GetByIdAsync(int id)
        {
            var timeTableGroup = await _context.TimeTableGroups
                .Include(d => d.Term)
                .Where(d => !d.IsDeleted)
                .FirstOrDefaultAsync(d => d.TimTabGroId == id);

            if (timeTableGroup == null) return null!;

            return new TimeTableGroupDto
            {
                Id = timeTableGroup.TimTabGroId,
                TimTabGroName = timeTableGroup.TimTabGroName,
                TermId = timeTableGroup.TermId,
                TermName = timeTableGroup.Term?.TermName,
            };
        }

        public async Task CreateAsync(TimeTableGroupDto timeTableGroupDto, string createdBy)
        {
            // Check if the TermId exists in the Terms table
            var termExists = await _context.Terms.AnyAsync(t => t.TermId == timeTableGroupDto.TermId);
            if (!termExists)
            {
                throw new InvalidOperationException("The specified term id does not exist.");
            }

            // Proceed with creating the TimeTableGroup
            var timeTableGroup = new TimeTableGroup
            {
                TimTabGroName = timeTableGroupDto.TimTabGroName,
                TermId = timeTableGroupDto.TermId,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now
            };

            await _context.TimeTableGroups.AddAsync(timeTableGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TimeTableGroupDto timeTableGroupDto, string updatedBy)
        {
            var existingTimeTableGroup = await _context.TimeTableGroups.FindAsync(timeTableGroupDto.Id);
            if (existingTimeTableGroup == null) return false;

            existingTimeTableGroup.TimTabGroName = timeTableGroupDto.TimTabGroName;
            existingTimeTableGroup.TermId = timeTableGroupDto.TermId;
            existingTimeTableGroup.UpdatedBy = updatedBy;
            existingTimeTableGroup.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var timeTableGroup = await _context.TimeTableGroups.FindAsync(id);
            if (timeTableGroup == null) return false;

            timeTableGroup.IsDeleted = true;
            timeTableGroup.DeletedBy = deletedBy;
            timeTableGroup.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
