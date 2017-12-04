using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer.Configuration
{
    public class Scopes
    {
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                StandardScopes.Roles,

                new Scope
                {
                    Name = "api1",
                    Description = "My API",
                    DisplayName = "My Services Hub API"
                }
            };
        }
    }
}
