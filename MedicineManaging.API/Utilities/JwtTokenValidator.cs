using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedicineManaging.API.Utilities
{
    public class JwtTokenValidator
    {
        public static bool ValidateToken(JwtSecurityToken token)
        {
            var issuerIsValid = token.Issuer.Equals("API");

            var assignedUserRole = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
            
            return issuerIsValid && assignedUserRole != null;
        }
    }
}
