using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class TermService : ITermService
    {
        private readonly ApplicationDbContext _context;

        public TermService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TermDto>> GetAllAsync()
        {
            return await _context.Terms
                .Include(d => d.Year)
                .Where(d => !d.IsDeleted)
                .Select(d => new TermDto
                {
                    TermId = d.TermId,
                    TermName = d.TermName,
                    YearId = d.YearId,
                    YearName = d.Year!.YearName,

                    //IsRawCase = !d.IsDeleted // Uncomment if "raw" status is needed
                })
                .ToListAsync();
        }

        public async Task<TermDto> GetByIdAsync(int id)
        {
            var Term = await _context.Terms
                .Include(d => d.Year)
                .FirstOrDefaultAsync(d => d.TermId == id);

            if (Term == null) return null!;

            return new TermDto
            {
                TermId = Term.TermId,
                TermName = Term.TermName,
                YearId = Term.YearId,
                YearName = Term.Year!.YearName,


                //IsRawCase = !Term.IsDeleted // Uncomment if "raw" status is needed
            };
        }

        public async Task CreateAsync(TermDto TermDto, string createdBy)
        {
            var Term = new Term
            {
                TermId = TermDto.TermId,
                TermName = TermDto.TermName,
                YearId = TermDto.YearId,
                CreatedBy = createdBy
            };

            await _context.Terms.AddAsync(Term);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TermDto TermDto, string updatedBy)
        {
            var existingTerm = await _context.Terms.FindAsync(TermDto.TermId);
            if (existingTerm == null) return false;

            existingTerm.TermName = TermDto.TermName;
            existingTerm.UpdatedBy = updatedBy;
            existingTerm.UpdatedAt = DateTime.Now;
            existingTerm.YearId = TermDto.YearId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string deletedBy)
        {
            var Term = await _context.Terms.FindAsync(id);
            if (Term == null) return false;

            Term.IsDeleted = true;
            Term.DeletedBy = deletedBy;
            Term.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }


    }
}
