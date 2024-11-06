using BL;
using Domains;
using Domains.Dtos.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Almeshkat_Online_Schools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public UserController(UserManager<ApplicationUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";
        }

        // Helper method to get current user's username from the JWT token
        private string GetCurrentUserName()
        {
            return User?.Identity?.Name ?? "System"; // Defaults to "System" if not authenticated
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null) return BadRequest("البريد الإلكتروني موجود بالفعل.");

            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FullName = model.FullName,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                CreatedBy = GetCurrentUserName(),
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = GetCurrentUserName(),
                UpdatedDate = DateTime.UtcNow,
                DeviceIp = clientIp
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("تم تسجيل المستخدم بنجاح.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = _tokenService.GenerateToken(user);
                user.DeviceIp = HttpContext.Connection.RemoteIpAddress?.ToString();
                user.LoginDate = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found.");

            user.FullName = model.FullName;
            user.Country = model.Country;
            user.PhoneNumber = model.PhoneNumber;
            user.UpdatedBy = GetCurrentUserName();
            user.UpdatedDate = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("تم تحديث المستخدم بنجاح.");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found.");

            user.MarkAsDeleted();
            user.DeletedBy = GetCurrentUserName();
            user.DeletedDate = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("تم حذف المستخدم بنجاح.");
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users
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

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

    }
}
