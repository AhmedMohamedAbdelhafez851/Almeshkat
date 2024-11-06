using BL.Data;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ISubStageService
    {
        Task<IEnumerable<SubStageDto>> GetAllAsync();
        Task<SubStageDto> GetByIdAsync(int id);
        Task CreateAsync(SubStageDto SubStageDto, string createdBy);
        Task<bool> UpdateAsync(SubStageDto SubStageDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }

    public class SubStageService : ISubStageService
    {
        private readonly ApplicationDbContext _context;

        public SubStageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubStageDto>> GetAllAsync()
        {
            return await _context.SubStages
                .Include(d => d.Stage)
                .Where(d => !d.IsDeleted)
                .Select(d => new SubStageDto
                {
                    SubStageId = d.SubStageId,
                    SubStageName = d.SubStageName,
                    StageId = d.StageId,
                    StageName = d.Stage == null ? null : d.Stage.StageName,
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.Department == null ? null : d.Department.DepartmentName
                })
                .ToListAsync();
        }

        public async Task<SubStageDto> GetByIdAsync(int id)
        {
            var SubStage = await _context.SubStages
                .Include(d => d.Stage)
                .FirstOrDefaultAsync(d => d.SubStageId == id);

            if (SubStage == null) return null!;

            return new SubStageDto
            {
                SubStageId = SubStage.SubStageId,
                SubStageName = SubStage.SubStageName,
                StageId = SubStage.StageId,
                StageName = SubStage?.Stage?.StageName,
                DepartmentId = SubStage!.Department!.DepartmentId ,
                DepartmentName = SubStage.Department.DepartmentName 
            };

        }

        public async Task CreateAsync(SubStageDto SubStageDto, string createdBy)
        {
            var SubStage = new SubStage
            {
                SubStageId = SubStageDto.SubStageId,
                SubStageName = SubStageDto.SubStageName,
                StageId = SubStageDto.StageId,
                DepartmentId = SubStageDto.DepartmentId,
                CreatedBy = createdBy
            };

            await _context.SubStages.AddAsync(SubStage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(SubStageDto SubStageDto, string updatedBy)
        {
            var existingSubStage = await _context.SubStages.FindAsync(SubStageDto.SubStageId);
            if (existingSubStage == null) return false;

            existingSubStage.SubStageName = SubStageDto.SubStageName;
            existingSubStage.UpdatedBy = updatedBy;
            existingSubStage.UpdatedAt = DateTime.Now;
            existingSubStage.StageId = SubStageDto.StageId;
            existingSubStage.DepartmentId  = SubStageDto.DepartmentId;  

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var SubStage = await _context.SubStages.FindAsync(id);
            if (SubStage == null) return false;

            SubStage.IsDeleted = true;
            SubStage.DeletedBy = deletedBy;
            SubStage.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }


    }
}
