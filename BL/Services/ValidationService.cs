using BL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ValidationService
    {
        private readonly ApplicationDbContext _context;

        public ValidationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Generic validation method for common validations
        public async Task ValidateAsync<TDto, TEntity>(
            TDto dto,
            Func<TDto, Task> customValidation = null!,
            Func<TDto, Task<bool>> checkDuplicate = null!,
            string entityNameRequiredMessage = "Entity name is required.",
            string duplicateEntityMessage = "An entity with the same name already exists.",
            int maxNameLength = 100
        )
        {
            // Check if required name field is present and valid
            var nameProperty = typeof(TDto).GetProperty("Name");  // Assuming common "Name" property
            if (nameProperty != null)
            {
                var nameValue = nameProperty.GetValue(dto) as string;
                if (string.IsNullOrWhiteSpace(nameValue))
                    throw new ArgumentException(entityNameRequiredMessage);

                if (nameValue.Length > maxNameLength)
                    throw new ArgumentException($"Name must not exceed {maxNameLength} characters.");
            }

            // Custom validation logic if provided
            if (customValidation != null)
            {
                await customValidation(dto);
            }

            // Check for duplicate entity if needed
            if (checkDuplicate != null)
            {
                var isDuplicate = await checkDuplicate(dto);
                if (isDuplicate)
                    throw new InvalidOperationException(duplicateEntityMessage);
            }
        }

        // Generic method to check for duplicate entries
        public async Task<bool> IsDuplicateAsync<TEntity>(Func<TEntity, bool> filter)
            where TEntity : class
        {
            return await _context.Set<TEntity>().AnyAsync(e => filter(e));
        }

        // Generic foreign key existence check (example for checking Year existence)
        public async Task<bool> ExistsAsync<TEntity>(int id) where TEntity : class
        {
            return await _context.Set<TEntity>().AnyAsync(e => EF.Property<int>(e, "Id") == id && !EF.Property<bool>(e, "IsDeleted"));
        }
    }
}
