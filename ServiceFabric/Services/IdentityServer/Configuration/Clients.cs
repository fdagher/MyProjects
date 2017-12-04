using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    static class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                // no human involved
                new Client
                {
                    ClientName = "CRM+ Client (Service Communication)",
                    ClientId = "crm_service",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,

                    Flow = Flows.ClientCredentials,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("B443D9C2-D068-4542-B148-D58003022CEA".Sha256())
                    },
                    
                    AllowedScopes = new List<string>
                    {
                        "customersapi",
                        "portfolioapi"
                    }
                },
                new Client
                {
                    ClientName = "WOL+ Client (Service Communication)",
                    ClientId = "wol_service",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,

                    Flow = Flows.ClientCredentials,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("73861D23-CE29-42FE-A89C-B0B2C6218A15".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "customersapi",
                        "portfolioapi"
                    }
                },
                // human is involved
                new Client
                {
                    ClientName = "crm on behalf of Carbon Client",
                    ClientId = "carbon",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Reference,

                    Flow = Flows.ResourceOwner,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("E855AB91-0ACE-44AD-BBE6-7D9C0F34513B".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "customersapi"
                    }
                },
                // human is involved
                new Client
                {
                    ClientName = "MVC Client",
                    ClientId = "mvc",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:23138/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:23138/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "customersapi",
                        "portfolioapi"
                    }
                },
                // human is involved
                new Client
                {
                    ClientName = "CRM+ Client",
                    ClientId = "crm",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    Flow = Flows.Implicit,
                    //RequireConsent = false,
                    //RequireSignOutPrompt = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:7000/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:7000/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "customersapi",
                        "portfolioapi"
                    }
                },

                // human is involved
                new Client
                {
                    ClientName = "WOL+ Client",
                    ClientId = "wol",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:6000/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:6000/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "customersapi",
                        "portfolioapi"
                    }
                },
                // human is involved
                new Client
                {
                    ClientName = "Alex Client",
                    ClientId = "alex",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Reference,
                    Flow = Flows.Implicit,
                    RequireConsent = false,
                    //RequireSignOutPrompt = true,
                    RedirectUris = new List<string>
                    {
                        "http://10.149.120.155/crm2/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://10.149.120.155/crm2/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "customersapi"
                    }
                },
                // human is involved
                new Client
                {
                    ClientName = "Alex Client (JWT Token)",
                    ClientId = "alex2",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    Flow = Flows.Implicit,
                    RequireConsent = false,
                    //RequireSignOutPrompt = true,
                    RedirectUris = new List<string>
                    {
                        "http://10.149.120.155/crm2/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://10.149.120.155/crm2/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "customersapi"
                    }
                }
            };
        }
    }
}
