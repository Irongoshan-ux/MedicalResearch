using System.IdentityModel.Tokens.Jwt;

namespace MedicineManaging.API.Utilities
{
    public class JwtTokenValidator
    {
        public static bool ValidateToken(JwtSecurityToken token)
        {
            var IssuedIsValid = token.Issuer.Equals("API");

            var assignedUserEmail = token.Claims.First(claim => claim.Type.Equals("sub")).Value;
            
            return IssuedIsValid && (assignedUserEmail != null);
        }
    }
}
