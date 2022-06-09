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
                new Client
                {
                    ClientId =  "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256())},
                    AllowedScopes = { "JusAPI.read" , "JusAPI.write" }
                },
                new Client
                {
                    ClientId =  "interactive",
                    ClientSecrets = { new Secret("ClientSecret1".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost5444/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost5444/sigout-oidc",
                    PostLogoutRedirectUris = { "https://localhost5444/sigout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "profile", "JusAPI.read" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false,
                }
            };
    }
}
