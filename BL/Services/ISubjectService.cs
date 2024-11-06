using BL.Data;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync();
        Task<SubjectDto> GetByIdAsync(int id);
        Task<SubjectDto> CreateAsync(SubjectDto subjectDto , string createdBy);
        Task<bool> UpdateAsync(SubjectDto subjecttDto, string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }

    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext _context;

        public SubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {
            return await _context.Subjects
                .Select(s => new SubjectDto
                {
                    SubjectId = s.SubjectId,
                    SubjectName = s.SubjectName
                })
                .ToListAsync();
        }

        public async Task<SubjectDto> GetByIdAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return null!;

            return new SubjectDto
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName
            };
        }

        public async Task<SubjectDto> CreateAsync(SubjectDto subjectDto, string createdBy)
        {
            var subject = new Subject
            {
                SubjectName = subjectDto.SubjectName , 
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now
            };

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            subjectDto.SubjectId = subject.SubjectId; // Get the generated ID
            return subjectDto;
        }

        public async Task<bool> UpdateAsync(SubjectDto SubjectDto, string updatedBy)
        {
            var existingSubject = await _context.Subjects.FindAsync(SubjectDto.SubjectId);
            if (existingSubject == null) return false;

            existingSubject.SubjectName = SubjectDto.SubjectName;
            existingSubject.UpdatedBy = updatedBy;
            existingSubject.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var department = await _context.Subjects.FindAsync(id);
            if (department == null) return false;

            department.IsDeleted = true;
            department.DeletedBy = deletedBy;
            department.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
