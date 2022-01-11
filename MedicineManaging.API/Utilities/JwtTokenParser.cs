using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace MedicineManaging.API.Utilities
{
    public class JwtTokenParser
    {
        public static JwtSecurityToken? ParseSecurityToken(HttpContext context)
        {
            context.Request.Headers.TryGetValue("Authorization", out StringValues values);

            if (values.Count <= 0) return null;

            string token = values.First();

            if (token.Contains("Bearer"))
            {
                token = token.Replace("Bearer ", "");
            }

            return TryReadToken(token);
        }

        private static JwtSecurityToken? TryReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jsonToken = handler.ReadToken(token);
                return jsonToken as JwtSecurityToken;
            }
            catch
            {
                return null;
            }
        }
    }
}
