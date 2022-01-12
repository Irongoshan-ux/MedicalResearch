using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;

namespace UserManaging.API.Utilities
{
    public class HttpUserHelper
    {
        public static Task<UserDTO> GetCurrentUserAsync(IUserService userService, HttpContext context) =>
            userService.FindByEmailAsync(GetCurrentUserEmail(context), CancellationToken.None);

        private static string GetCurrentUserEmail(HttpContext context)
        {
            var securityToken = ParseSecurityToken(context);
            var userEmail = securityToken is not null?
                securityToken.Claims.First(claim => claim.Type.Equals("sub")).Value
                : context.User.Claims.FirstOrDefault(claim => claim.Type.Equals("sub"))?.Value;

            return userEmail;
        }

        public static JwtSecurityToken? ParseSecurityToken(HttpContext context)
        {
            context.Request.Headers.TryGetValue("Authorization", out StringValues values);

            if (values.Count <= 0) return null;

            if (values.FirstOrDefault().Contains("Bearer"))
            {
                var token = values.FirstOrDefault().Replace("Bearer ", "");

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);

                return jsonToken as JwtSecurityToken;
            }
            else return null;
        }
    }
}
