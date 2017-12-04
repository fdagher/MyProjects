using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecureWebApp.Startup))]
namespace SecureWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
