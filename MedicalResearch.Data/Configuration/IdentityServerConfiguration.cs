using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4;

namespace UserManaging.Infrastructure.Configuration
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("API", "ServerAPI",
                new[] { JwtClaimTypes.Subject, JwtClaimTypes.Email, JwtClaimTypes.Role })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            yield return new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                ClientSecrets = { new Secret("secret".Sha512()) },
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Address();
            yield return new IdentityResources.Profile();
            yield return new IdentityResources.Email();
        }

        public static IEnumerable<ApiScope> GetScopes()
        {
            yield return new ApiScope("API");
        }
    }
}
