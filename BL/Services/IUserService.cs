using BL;
using Domains;
using Domains.Dtos.UserDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Almeshkat_Online_Schools.Services
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterUserDto model, string currentUserName, string clientIp);
        Task<(string Token, bool Success)> LoginUserAsync(LoginDto model, string clientIp);
        Task<string> UpdateUserAsync(string id, UpdateUserDto model, string currentUserName);
        Task<string> SoftDeleteUserAsync(string id, string currentUserName);
        Task<List<UserResponseDto>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterUserAsync(RegisterUserDto model, string currentUserName, string clientIp)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null) return "البريد الإلكتروني موجود بالفعل.";

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FullName = model.FullName,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                CreatedBy = currentUserName,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = currentUserName,
                UpdatedDate = DateTime.UtcNow,
                DeviceIp = clientIp
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return string.Join(", ", result.Errors.Select(e => e.Description));

            return "تم تسجيل المستخدم بنجاح.";
        }

        public async Task<(string Token, bool Success)> LoginUserAsync(LoginDto model, string clientIp)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = await _tokenService.GenerateToken(user); // Updated to use async GenerateToken
                user.DeviceIp = clientIp;
                user.LoginDate = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                return (token, true);
            }
            return (null!, false);
        }


        public async Task<string> UpdateUserAsync(string id, UpdateUserDto model, string currentUserName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return "User not found.";

            user.FullName = model.FullName;
            user.Country = model.Country;
            user.PhoneNumber = model.PhoneNumber;
            user.UpdatedBy = currentUserName;
            user.UpdatedDate = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return string.Join(", ", result.Errors.Select(e => e.Description));

            return "تم تحديث المستخدم بنجاح.";
        }

        public async Task<string> SoftDeleteUserAsync(string id, string currentUserName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return "User not found.";

            user.MarkAsDeleted();
            user.DeletedBy = currentUserName;
            user.DeletedDate = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return string.Join(", ", result.Errors.Select(e => e.Description));

            return "تم حذف المستخدم بنجاح.";
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Email = u.Email!,
                    FullName = u.FullName,
                    Country = u.Country,
                    PhoneNumber = u.PhoneNumber,
                    UserPhoto = u.UserPhoto!,
                    CreatedBy = u.CreatedBy!,
                    CreatedDate = u.CreatedDate,
                    UpdatedBy = u.UpdatedBy!,
                    UpdatedDate = u.UpdatedDate,
                    IsDeleted = u.IsDeleted,
                    DeletedDate = u.DeletedDate,
                    LoginDate = u.LoginDate,
                    LogoutDate = u.LogoutDate
                })
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(id));
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return user;
        }

    }
}
