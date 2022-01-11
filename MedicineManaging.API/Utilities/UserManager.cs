using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedicineManaging.API.Utilities
{
    public static class UserManager
    {
        public static bool IsUserInRole(JwtSecurityToken token, string[] roleNames)
        {
            var role = GetUserRoleFromToken(token);

            if (role != null)
                return roleNames.Contains(role);

            return false;
        }

        public static bool IsUserInRole(JwtSecurityToken token, string roleName)
        {
            var role = GetUserRoleFromToken(token);

            if (role != null)
                return role.Equals(roleName);

            return false;
        }

        private static string? GetUserRoleFromHttpContext(HttpContext context) =>
            context.User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role))?.Value;

        private static string? GetUserRoleFromToken(JwtSecurityToken token) =>
            token.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role))?.Value;
    }
}
