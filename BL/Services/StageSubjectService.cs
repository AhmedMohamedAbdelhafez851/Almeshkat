using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class StageSubjectService : IStageSubjectService
    {
        private readonly ApplicationDbContext _context;

        public StageSubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StageSubjectsDto>> GetAllAsync()
        {
            return await _context.StageSubjects
                .Include(s => s.Term)
                .Include(s => s.SubStage)
                .Include(s => s.Subject)
                .Where(s => !s.IsDeleted)
                .Select(s => new StageSubjectsDto
                {
                    StageSubjectId = s.StaSubjId,
                    TermId = s.TermId,
                    TermName = s.Term!.TermName,
                    SubStageId = s.SubStageId,
                    SubStageName = s.SubStage!.SubStageName,
                    SubjectId = s.SubjectId,
                    SubjectName = s.Subject!.SubjectName
                })
                .ToListAsync();
        }

        public async Task<StageSubjectsDto?> GetByIdAsync(int id)
        {
            var stageSubject = await _context.StageSubjects
                .Include(s => s.Term)
                .Include(s => s.SubStage)
                .Include(s => s.Subject)
                .Where(s => !s.IsDeleted)
                .FirstOrDefaultAsync(s => s.StaSubjId == id);

            if (stageSubject == null)
                return null;

            return new StageSubjectsDto
            {
                StageSubjectId = stageSubject.StaSubjId,
                TermId = stageSubject.TermId,
                TermName = stageSubject.Term!.TermName,
                SubStageId = stageSubject.SubStageId,
                SubStageName = stageSubject.SubStage!.SubStageName,
                SubjectId = stageSubject.SubjectId,
                SubjectName = stageSubject.Subject!.SubjectName
            };
        }

        public async Task CreateAsync(StageSubjectsDto stageSubjectsDto, string createdBy)
        {
            var newStageSubject = new StageSubject
            {

                TermId = stageSubjectsDto.TermId,
                SubStageId = stageSubjectsDto.SubStageId,
                SubjectId = stageSubjectsDto.SubjectId,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now
            };

            await _context.StageSubjects.AddAsync(newStageSubject);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(StageSubjectsDto stageSubjectsDto, string updatedBy)
        {
            var existingStageSubject = await _context.StageSubjects.FindAsync(stageSubjectsDto.StageSubjectId);
            if (existingStageSubject == null)
                return false;

            existingStageSubject.TermId = stageSubjectsDto.TermId;
            existingStageSubject.SubStageId = stageSubjectsDto.SubStageId;
            existingStageSubject.SubjectId = stageSubjectsDto.SubjectId;
            existingStageSubject.UpdatedBy = updatedBy;
            existingStageSubject.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var stageSubject = await _context.StageSubjects.FindAsync(id);
            if (stageSubject == null)
                return false;

            stageSubject.IsDeleted = true;
            stageSubject.DeletedBy = deletedBy;
            stageSubject.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
