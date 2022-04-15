using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using UserManaging.API.Exceptions.User;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;

namespace UserManaging.API.Utilities
{
    public class HttpUserHelper
    {
        public static Task<UserDTO> GetCurrentUserAsync(IUserService userService, HttpContext context)
        {
            try
            {
                return userService.FindByEmailAsync(GetCurrentUserEmail(context), CancellationToken.None);
            }
            catch (Exception ex)
            {
                throw new UserNotFoundException(ex.Message);
            }
        }

        private static string GetCurrentUserEmail(HttpContext context)
        {
            var securityToken = ParseSecurityToken(context);

            string userEmail = string.Empty;

            try
            {
                userEmail = securityToken is not null ?
                    securityToken.Claims.First(claim => claim.Type.Equals("sub")).Value
                    : context.User.Claims.FirstOrDefault(claim => claim.Type.Equals("sub"))?.Value;
            }
            catch (Exception e)
            {
                throw new UserNotFoundException(e.Message);
            }
            return userEmail;
        }

        public static JwtSecurityToken? ParseSecurityToken(HttpContext context)
        {
            if (context is null) return null;

            context.Request.Headers.TryGetValue("Authorization", out StringValues values);

            var token = values.FirstOrDefault();

            if (IsTokenValid(token))
            {
                if (token.Contains("Bearer"))
                {
                    return GetJwtToken(ref token);
                }
            }

            return null;
        }

        private static JwtSecurityToken? GetJwtToken(ref string token)
        {
            token = token.Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);

            return jsonToken as JwtSecurityToken;
        }

        private static bool IsTokenValid(string? token) =>
            !string.IsNullOrWhiteSpace(token) && token.Length > 20;
    }
}
