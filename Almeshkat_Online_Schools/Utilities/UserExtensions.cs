using System.Security.Claims;

namespace Almeshkat_Online_Schools.Utilities
{
    public static class UserExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";
        }
    }
}
