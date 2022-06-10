using IdentityServer4;
using IdentityServer4.Models;

namespace Sever
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "Role",
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("JusAPI.read"), new ApiScope("JusAPI.write"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("JusAPI")
                {
                    Scopes = new List<string> { "JusAPI.read" , "JusAPI.write" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                //new Client
                //{
                //    ClientId =  "m2m.client",
                //    ClientName = "Client Credentials Client",
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets = { new Secret("ClientSecret1".Sha256())},
                //    AllowedScopes = { "JusAPI.read" , "JusAPI.write" }
                //},
                //new Client
                //{
                //    ClientId =  "interactive",
                //    AllowedGrantTypes = GrantTypes.Code,

                //    RedirectUris = { "http://localhost:4200" },
                //    PostLogoutRedirectUris = { "http://localhost:4200" },
                //    AllowOfflineAccess = true,
                //    AllowedScopes = {"openid", "profile", "JusAPI.read" },
                //    RequirePkce = true,
                //    RequireConsent = false,
                //    AllowPlainTextPkce = false,
                //    RequireClientSecret = false,

                //},

                new Client
                {

                    ClientId = "angular",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes = {
                     IdentityServerConstants.StandardScopes.OpenId,
                      "JusAPI.write"
                    },

                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                }
            };
    }
}
