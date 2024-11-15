using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class TeacherSubjectService : ITeacherSubjectService
    {
        private readonly ApplicationDbContext _context;

        public TeacherSubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherSubjectDto>> GetAllAsync()
        {
            return await _context.TeacherSubjects
                .Include(ts => ts.Teacher)
                .Include(ts => ts.StageSubject).Where(d => !d.IsDeleted)

                .Select(ts => new TeacherSubjectDto
                {
                    Id = ts.Id,
                    TeacherId = ts.TeacherId,
                    StaSubjId = ts.StaSubjId,
                    TeacherName = ts.Teacher!.User!.FullName,
                    SubjectName = ts.StageSubject!.Subject!.SubjectName,
                    // Additional properties if any
                })
                .ToListAsync();
        }

        public async Task<TeacherSubjectDto> GetByIdAsync(int teacherId, int subjectId)
        {
            var teacherSubject = await _context.TeacherSubjects
                .FirstOrDefaultAsync(ts => ts.TeacherId == teacherId && ts.StaSubjId == subjectId);

            if (teacherSubject == null) return null!;

            return new TeacherSubjectDto
            {
                Id = teacherSubject.Id,
                TeacherId = teacherSubject.TeacherId,
                StaSubjId = teacherSubject.StaSubjId,

                // Additional properties if any
            };
        }

        public async Task CreateAsync(TeacherSubjectDto teacherSubjectDto, string createdBy)
        {
            var teacherSubject = new TeacherSubject
            {
                TeacherId = teacherSubjectDto.TeacherId,
                StaSubjId = teacherSubjectDto.StaSubjId,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now,
            };

            await _context.TeacherSubjects.AddAsync(teacherSubject);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TeacherSubjectDto teacherSubjectDto, string updatedBy)
        {
            var teacherSubject = await _context.TeacherSubjects
                .FirstOrDefaultAsync(ts => ts.TeacherId == teacherSubjectDto.TeacherId && ts.StaSubjId == teacherSubjectDto.StaSubjId);

            if (teacherSubject == null)
                return false;

            // Update fields as necessary
            teacherSubject.UpdatedBy = updatedBy;
            teacherSubject.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int teacherId, int subjectId, string deletedBy)
        {
            var teacherSubject = await _context.TeacherSubjects
                .FirstOrDefaultAsync(ts => ts.TeacherId == teacherId && ts.StaSubjId == subjectId);

            if (teacherSubject == null)
                return false;

            teacherSubject.IsDeleted = true;
            teacherSubject.DeletedBy = deletedBy;
            teacherSubject.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
