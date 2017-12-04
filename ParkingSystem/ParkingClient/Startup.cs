using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParkingClient.Startup))]
namespace ParkingClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
