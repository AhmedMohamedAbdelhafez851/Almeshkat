using BL.Data;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto> GetByIdAsync(int id);
        Task CreateAsync(TeacherDto teacherDto , string createdBy);
        Task<bool> UpdateAsync(TeacherDto teacherDto , string updatedBy);
        Task<bool> DeleteAsync(int id, string deletedBy);
    }

    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;

        public TeacherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            return await _context.Teachers.Where(d => !d.IsDeleted)
                .Select(t => new TeacherDto
                {
                    TeacherId = t.TeacherId,
                    UserId = t.UserId,
                    ZoomLink = t.ZoomLink
                })
                .ToListAsync();
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            var teacher = await _context.Teachers
                .Where(t => t.TeacherId == id).Where(d=>!d.IsDeleted)
                .Select(t => new TeacherDto
                {
                    TeacherId = t.TeacherId,
                    UserId = t.UserId,
                    ZoomLink = t.ZoomLink
                })
                .FirstOrDefaultAsync();

            return teacher!;
        }

        public async Task CreateAsync(TeacherDto teacherDto, string createdBy)
        {
            var teacher = new Teacher
            {
                UserId = teacherDto.UserId,
                ZoomLink = teacherDto.ZoomLink , 
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now    
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TeacherDto teacherDto , string updatedBy)
        {
            var teacher = await _context.Teachers.FindAsync(teacherDto.TeacherId);
            if (teacher == null) return false;
            teacher.UserId =teacherDto.UserId;  
            teacher.ZoomLink = teacherDto.ZoomLink;
            teacher.UpdatedBy = updatedBy;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id  ,string deletedBy)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            teacher.IsDeleted = true;
            teacher.DeletedBy = deletedBy;
            teacher.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
