using Duende.IdentityServer.Models;
namespace Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
        };
    
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                RedirectUris= {"http://localhost5002/signin-oidc"},
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:5002/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                AllowedCorsOrigins = { "http://localhost:3000" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            new Client
            {
                ClientId = "js",
                AllowedCorsOrigins = { "https://diydevblog.com" },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://diydevblog.com/redirect" },
                FrontChannelLogoutUri = "https://diydevblog.com/redirect",
                PostLogoutRedirectUris =  { "https://diydevblog.com/redirect" },
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" },
                
            },
            new Client
            {
                ClientId = "js-dev",
                AllowedCorsOrigins = { "http://localhost:3000" },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "http://localhost:3000/redirect" },
                FrontChannelLogoutUri = "http://localhost:3000/redirect",
                PostLogoutRedirectUris =  { "http://localhost:3000/redirect" },
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" },
            },
        };
}
