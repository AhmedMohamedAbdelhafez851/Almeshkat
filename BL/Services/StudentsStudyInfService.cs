using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BL.Services
{
    public class StudentsStudyInfService : IStudentsStudyInfService
    {
        private readonly ApplicationDbContext _context;

        public StudentsStudyInfService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentStudyInfDto>> GetAllAsync()
        {
            return await _context.StudentsStudyInfos
                .Include(s => s.User)
                .Include(s => s.Year)
                .Include(s => s.SubStage)
                .Where(s => !s.IsDeleted)
                .Select(s => new StudentStudyInfDto
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    UserName = s.User!.FullName,
                    YearName = s.Year!.YearName,
                    SubStageName = s.SubStage!.SubStageName
                })
                .ToListAsync();
        }


        public async Task<StudentStudyInfDto> GetByIdAsync(int id)
        {
            var studentsStudyInf = await _context.StudentsStudyInfos
                .Include(s => s.User)
                .Include(s => s.Year)
                .Include(s => s.SubStage)
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (studentsStudyInf == null) return null!;

            return new StudentStudyInfDto
            {
                Id = studentsStudyInf.Id,
                UserId = studentsStudyInf.UserId,
                UserName = studentsStudyInf.User!.FullName,
                SubStageId = studentsStudyInf.SubStageId,
                SubStageName = studentsStudyInf.SubStage!.SubStageName,
                YearId = studentsStudyInf.YearId,
                YearName = studentsStudyInf.Year!.YearName
            };
        }

        public async Task CreateAsync(StudentStudyInfDto studentsStudyInfDto, string createdBy)
        {
            if (!await _context.Years.AnyAsync(y => y.YearId == studentsStudyInfDto.YearId))
                throw new ValidationException("Invalid YearId.");

            if (!await _context.SubStages.AnyAsync(s => s.SubStageId == studentsStudyInfDto.SubStageId))
                throw new ValidationException("Invalid SubStageId.");

            var entity = new StudentsStudyInfo
            {
                UserId = studentsStudyInfDto.UserId,
                YearId = studentsStudyInfDto.YearId,
                SubStageId = studentsStudyInfDto.SubStageId,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now
            };

            _context.StudentsStudyInfos.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(StudentStudyInfDto studentsStudyInfDto, string updatedBy)
        {
            var existingStudentsStudyInf = await _context.StudentsStudyInfos
                .FirstOrDefaultAsync(s => s.Id == studentsStudyInfDto.Id && !s.IsDeleted);

            if (existingStudentsStudyInf == null) return false;

            existingStudentsStudyInf.UserId = studentsStudyInfDto.UserId;
            existingStudentsStudyInf.YearId = studentsStudyInfDto.YearId;
            existingStudentsStudyInf.SubStageId = studentsStudyInfDto.SubStageId;
            existingStudentsStudyInf.UpdatedBy = updatedBy;
            existingStudentsStudyInf.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var studentsStudyInf = await _context.StudentsStudyInfos
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (studentsStudyInf == null) return false;

            studentsStudyInf.IsDeleted = true;
            studentsStudyInf.DeletedBy = deletedBy;
            studentsStudyInf.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
