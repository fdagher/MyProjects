using IdentityServer.Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Services;

namespace IdentityServer
{
    class Startup
    {
        public static void Configuration(IAppBuilder app)
        {

            var factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get());

            //factory.ViewService = new Registration<IViewService, NBKViewService>();

            var options = new IdentityServerOptions
            {
                SiteName = "NBK Identity Server",
                SigningCertificate = LoadCertificate(),
                Factory = factory,
                RequireSsl = false //,
                //AuthenticationOptions = new AuthenticationOptions() { EnablePostSignOutAutoRedirect = true }  //allow auto redirect to client after sign out
            };

            app.UseIdentityServer(options);
        }

        private static X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }

        //public static X509Certificate2 LoadCertificateFromStore()
        //{
        //    string thumbPrint = "idsrv3test";
        //    var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        //    store.Open(OpenFlags.ReadOnly);
        //    var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, validOnly: false);
        //    store.Close();
        //    if (certCollection.Count == 0)
        //        throw new Exception("No certificate found containing the specified thumbprint.");

        //    return certCollection[0];
        //}
    }
}



