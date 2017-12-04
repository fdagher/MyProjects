using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "bob",
                    Password = "secret",
                    Subject = "bob",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Role, "Geek"),
                        new Claim(Constants.ClaimTypes.Role, "Foo")
                    }
                },
                new InMemoryUser
                {
                    Username = "alice",
                    Password = "secret",
                    Subject = "alice",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Alice"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Kim")
                    }
                },
                new InMemoryUser
                {
                    Username = "r2622",
                    Password = "sit.622",
                    Subject = "r2622",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Saleh"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Al Shimari"),
                        new Claim(Constants.ClaimTypes.Role, "BM")
                    }
                },
                new InMemoryUser
                {
                    Username = "r301",
                    Password = "sit.301",
                    Subject = "r301",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Fouad"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Rishani"),
                        new Claim(Constants.ClaimTypes.Role, "BM")
                    }
                }
            };
        }
    }
}
