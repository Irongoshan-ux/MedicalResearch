using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;

namespace UserManaging.Infrastructure.Configuration
{
    public class IdentityServerProfileService : IProfileService
    {
        protected UserManager<User> _userManager;
        protected IUserService _userService;

        public IdentityServerProfileService(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userService.FindEntityByEmailAsync(GetUserEmailByClaims(context.Subject.Claims),
                CancellationToken.None);

            var claims = await GetClaimsAsync(user);

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userService.FindByEmailAsync(GetUserEmailByClaims(context.Subject.Claims),
                CancellationToken.None);

            context.IsActive = user != null;
        }

        private async Task<ICollection<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>();
            IList<string> roles;

            try
            {
                roles = await _userManager.GetRolesAsync(user);
            }
            catch
            {
                return claims;
            }

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private string GetUserEmailByClaims(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                if (claim.Type.Equals("sub"))
                {
                    return claim.Value;
                }
            }

            return string.Empty;
        }
    }
}
