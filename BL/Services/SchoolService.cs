using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;


namespace BL.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ApplicationDbContext _context;

        public SchoolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SchoolDto> GetByIdAsync(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null || school.IsDeleted)
                return null!;

            return new SchoolDto
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                SchoolLogoImagePath = school.SchoolLogoImagePath,
                SchoolLogoName = school.SchoolLogoName,
                SchoolPhone = school.SchoolPhone,
                SchoolAddress = school.SchoolAddress,
                SchoolFacebookLink = school.SchoolFacebookLink,
                SchoolYoutubeLink = school.SchoolYoutubeLink
            };
        }

        public async Task<IEnumerable<SchoolDto>> GetAllAsync()
        {
            return await _context.Schools
                .Where(s => !s.IsDeleted)
                .Select(s => new SchoolDto
                {
                    SchoolId = s.SchoolId,
                    SchoolName = s.SchoolName,
                    SchoolLogoImagePath = s.SchoolLogoImagePath,
                    SchoolLogoName = s.SchoolLogoName,
                    SchoolPhone = s.SchoolPhone,
                    SchoolAddress = s.SchoolAddress,
                    SchoolFacebookLink = s.SchoolFacebookLink,
                    SchoolYoutubeLink = s.SchoolYoutubeLink
                })
                .ToListAsync();
        }

        public async Task<SchoolDto> CreateAsync(SchoolDto schoolDto, string createdBy)
        {
            var school = new School
            {
                SchoolName = schoolDto.SchoolName,
                SchoolLogoImagePath = schoolDto.SchoolLogoImagePath,
                SchoolLogoName = schoolDto.SchoolLogoName,
                SchoolPhone = schoolDto.SchoolPhone,
                SchoolAddress = schoolDto.SchoolAddress,
                SchoolFacebookLink = schoolDto.SchoolFacebookLink,
                SchoolYoutubeLink = schoolDto.SchoolYoutubeLink,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now // Consider adding CreatedAt
            };

            await _context.Schools.AddAsync(school);
            await _context.SaveChangesAsync();
            schoolDto.SchoolId = school.SchoolId;
            return schoolDto;
        }

        public async Task<bool> UpdateAsync(SchoolDto schoolDto, string updatedBy)
        {
            var school = await _context.Schools.FindAsync(schoolDto.SchoolId);

            // Check if the school exists and is not marked as deleted
            if (school == null || school.IsDeleted)
                return false;

            // Update the school's properties
            school.SchoolName = schoolDto.SchoolName;
            school.SchoolLogoImagePath = schoolDto.SchoolLogoImagePath;
            school.SchoolLogoName = schoolDto.SchoolLogoName;
            school.SchoolPhone = schoolDto.SchoolPhone;
            school.SchoolAddress = schoolDto.SchoolAddress;
            school.SchoolFacebookLink = schoolDto.SchoolFacebookLink;
            school.SchoolYoutubeLink = schoolDto.SchoolYoutubeLink;
            school.UpdatedBy = updatedBy;
            school.UpdatedAt = DateTime.Now;


            // Mark the school as modified
            _context.Schools.Update(school);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return true to indicate the update was successful
            return true;
        }


        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null || school.IsDeleted) return false;

            school.IsDeleted = true;
            school.DeletedAt = DateTime.UtcNow;
            school.DeletedBy = deletedBy; // Replace with actual user ID
            school.UpdatedAt = DateTime.Now;

            _context.Schools.Update(school);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

