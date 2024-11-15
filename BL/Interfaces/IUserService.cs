using Domains;
using Domains.Dtos.UserDto;

namespace BL.Interfaces
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
}
