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
