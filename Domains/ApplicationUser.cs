using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب.")]
        [StringLength(100, ErrorMessage = "لا يمكن أن يتجاوز اسم المستخدم 100 حرف.")]
        [RegularExpression(@"^[\p{L} \.'-]+$", ErrorMessage = "اسم المستخدم يحتوي على أحرف غير صالحة.")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "الدولة مطلوبة.")]
        [StringLength(50, ErrorMessage = "لا يمكن أن تتجاوز الدولة 50 حرفًا.")]
        public string Country { get; set; } = "";

        public string? UserPhoto { get; set; }// = string.Empty; // Default to empty string

        [Phone(ErrorMessage = "رقم الهاتف غير صالح.")]
        public new string PhoneNumber { get; set; } = "";  // Explicitly hide the base property

        // Auditing columns
        [Required(ErrorMessage = "تم تعيين قيمة للمنشئ.")]
        public string? CreatedBy { get; set; } // Ensure non-null
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; }// = string.Empty;
        public DateTime? UpdatedDate { get; set; }

        // Soft delete properties
        public string? DeletedBy { get; set; }// = string.Empty;

        public bool? IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }

        // Login/logout tracking
        public DateTime? LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }

        // Device IP column
        [StringLength(45, ErrorMessage = "عنوان IP لا يمكن أن يتجاوز 45 حرفًا.")]
        public string? DeviceIp { get; set; }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
        }
    }
}
