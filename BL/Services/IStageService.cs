using BL.Data;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IStageService
    {
        Task<StageDto> GetByIdAsync(int id);
        Task<IEnumerable<StageDto>> GetAllAsync();
        Task CreateAsync(StageDto StageDto, string createdBy);
        Task<bool> UpdateAsync(StageDto StageDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }

    public class StageService : IStageService
    {
        private readonly ApplicationDbContext _context;

        public StageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StageDto>> GetAllAsync()
        {
            return await _context.Stages
                .Where(d => !d.IsDeleted)
                .Select(d => new StageDto
                {
                    StageId = d.StageId,
                    StageName = d.StageName,
                   
                })
                .ToListAsync();
        }

        public async Task<StageDto> GetByIdAsync(int id)
        {
            var Stage = await _context.Stages
                 .Where(d => !d.IsDeleted)
                .FirstOrDefaultAsync(d => d.StageId == id);

            if (Stage == null) return null!;

            return new StageDto
            {
                StageId = Stage.StageId,
                StageName = Stage.StageName,
            };
        }

        public async Task CreateAsync(StageDto StageDto, string createdBy)
        {
            var Stage = new Stage
            {
                StageName = StageDto.StageName,
                CreatedBy = createdBy, // Only set CreatedBy
                CreatedAt = DateTime.Now // Consider using UtcNow for consistency
            };

            await _context.Stages.AddAsync(Stage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(StageDto StageDto, string updatedBy)
        {
            var existingStage = await _context.Stages.FindAsync(StageDto.StageId);
            if (existingStage == null) return false;

            existingStage.StageName = StageDto.StageName;
            existingStage.UpdatedBy = updatedBy; // Only set during updates
            existingStage.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var Stage = await _context.Stages.FindAsync(id);
            if (Stage == null) return false;

            Stage.IsDeleted = true;
            Stage.DeletedBy = deletedBy; // Only set during deletion
            Stage.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
