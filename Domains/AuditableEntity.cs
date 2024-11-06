using System;
using System.ComponentModel.DataAnnotations;

namespace Domains
{

    public abstract class AuditableEntity
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } // Nullable

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        [MaxLength(100)]
        public string? UpdatedBy { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(100)]
        public string? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; } // Nullable
    }

}
