using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    static class Scopes
    {
        public static List<Scope> Get()
        {
            List<Scope> _scopes = new List<Scope>();

            _scopes.AddRange(StandardScopes.All);

            _scopes.Add(new Scope
                            {
                                Enabled = true,
                                DisplayName = "Customer information API",
                                Name = "customersapi",
                                Description = "Access to the NBK customer information restful services API",
                                Type = ScopeType.Resource
            });

            _scopes.Add(new Scope
            {
                Enabled = true,
                DisplayName = "Portfolio information API",
                Name = "portfolioapi",
                Description = "Access to the NBK portfolio information restful services API",
                Type = ScopeType.Resource
            });

            _scopes.Add(new Scope
                            {
                                Enabled = true,
                                Name = "roles",
                                Type = ScopeType.Identity,
                                Claims = new List<ScopeClaim>
                                {
                                    new ScopeClaim("role")
                                }
                            });

            return _scopes;
        }
    }
}
