using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            return await _context.Students
                .Include(d => d.User)
                .Include(d => d.SubStage)
                .Where(d => !d.IsDeleted)
                .Select(d => new StudentDto
                {
                    StudentId = d.StudentId,
                    UserId = d.UserId,
                    FullName = d.User!.FullName,      // Null check for User
                    Email = d.User.Email,            // Null check for User
                    SubStageId = d.SubStageId,
                    SubStageName = d.SubStage != null ? d.SubStage.SubStageName : null // Null check for SubStage
                })
                .ToListAsync();
        }


        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _context.Students
                .Include(d => d.User)
                .Include(d => d.SubStage)
                .FirstOrDefaultAsync(d => d.StudentId == id);

            if (student == null) return null!;

            return new StudentDto
            {
                StudentId = student.StudentId,
                UserId = student.UserId,
                FullName = student.User!.FullName, // Null check for User
                Email = student.User!.Email,       // Null check for User
                SubStageId = student.SubStageId,
                SubStageName = student.SubStage?.SubStageName // Null check for SubStage
            };
        }


        public async Task CreateAsync(StudentDto studentDto, string createdBy)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == studentDto.UserId);
            if (user == null) throw new ArgumentException("Invalid UserId provided.");

            var student = new Student
            {
                UserId = studentDto.UserId,
                SubStageId = studentDto.SubStageId ?? throw new ArgumentException("SubStageId is required."),
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(StudentDto studentDto, string updatedBy)
        {
            var existingStudent = await _context.Students
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.StudentId == studentDto.StudentId);

            if (existingStudent == null) return false;

            existingStudent.UserId = studentDto.UserId;
            existingStudent.SubStageId = studentDto.SubStageId ?? existingStudent.SubStageId;

            // Null check for FullName before assigning
            if (existingStudent.User != null && studentDto.FullName != null)
            {
                existingStudent.User.FullName = studentDto.FullName;
            }
            else
            {
                // Handle null FullName (log error or provide default value if needed)
                return false; // Or set a default value for FullName if acceptable
            }

            existingStudent.UpdatedBy = updatedBy;
            existingStudent.UpdatedAt = DateTime.UtcNow;

            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            student.IsDeleted = true;
            student.DeletedBy = deletedBy;
            student.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}