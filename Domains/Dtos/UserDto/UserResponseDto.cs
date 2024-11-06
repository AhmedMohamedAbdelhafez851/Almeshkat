using System.ComponentModel.DataAnnotations;

namespace Domains.Dtos.UserDto
{

    public class UserResponseDto
    {
        public string Id { get; set; } = "";// User ID
        public string Email { get; set; } = ""; // User Email
        public string FullName { get; set; } = ""; // User Full Name
        public string Country { get; set; } = "";// User Country
        public string PhoneNumber { get; set; } = ""; // User Phone Number
        public string UserPhoto { get; set; } = ""; // User Photo URL
        public string CreatedBy { get; set; } = ""; // Created By
        public DateTime? CreatedDate { get; set; } // Created Date
        public string UpdatedBy { get; set; } = ""; // Updated By
        public DateTime? UpdatedDate { get; set; } // Updated Date
        public bool? IsDeleted { get; set; } // Is Deleted
        public DateTime? DeletedDate { get; set; } // Deleted Date
        public DateTime? LoginDate { get; set; } // Login Date
        public DateTime? LogoutDate { get; set; } // Logout Date
    }
}
