using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // authorization code flow client
                new Client
                {
                    ClientId = "code.client",

                    // interactive user in web server application, use the code for authentication
                    AllowedGrantTypes = GrantTypes.Code,

                     // where to redirect to after login
                    RedirectUris = { "http://localhost:5760/callback" },

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // implicit flow client
                new Client
                {
                    ClientId = "implicit.client",

                    // interactive user in web server application, use the code for authentication
                    AllowedGrantTypes = GrantTypes.Implicit,

                     // where to redirect to after login
                    RedirectUris = { "http://localhost:5761/callback" },

                    // scopes that client has access to
                    AllowedScopes = { "api1" },

                    AllowAccessTokensViaBrowser = true
                },
                // client credentials client
                new Client
                {
                    ClientId = "cc.client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                // OpenID Connect authorization flow client
                new Client
                {
                    ClientId = "code.oidc.client",
                    ClientName = "OpenID Authorization Code Flow Client",
                    AllowedGrantTypes = GrantTypes.Code,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5762/callback" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5762" },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.OfflineAccess.Name,
                        "api1"
                    }
                },
                // OpenID Connect hybrid flow client
                new Client
                {
                    ClientId = "hybrid.oidc.client",
                    ClientName = "OpenID Hybrid Flow Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5763/callback" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5763" },

                    AllowAccessTokensViaBrowser = true,

                    AllowedScopes =
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.Email.Name,
                        StandardScopes.Roles.Name,
                        StandardScopes.OfflineAccess.Name,
                        "api1"
                    }
                },
                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5750/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5750" },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name
                    }
                },
                // OpenID Connect hybrid flow + client configuration client (MVC)
                // In the “Hybrid Flow”, the identity token is transmitted via the browser channel, so the client can validate it 
                // before doing any more work. And if validation is successful, the client opens a back-channel to the token 
                // service to retrieve the access token. In addition we also want the client to allow doing server to server 
                // API calls which are not in the context of a user.
                // We also give the client access to the offline_access scope - this allows requesting refresh tokens 
                // for long lived API access
                new Client
                {
                    ClientId = "mvchybrid",
                    ClientName = "MVC Hybrid Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:5755/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5755" },

                    AllowedScopes =
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.OfflineAccess.Name,
                        StandardScopes.Roles.Name,
                        "api1"
                    }
                }
            };
        }
    }
}
