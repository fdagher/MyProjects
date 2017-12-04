using System.Web.Http;
using WebActivatorEx;
using CustomerProfileService;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CustomerProfileService
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c => c.SingleApiVersion("v1", "Customer Profile Service")).EnableSwaggerUi();
        }

        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config.EnableSwagger(c => c.SingleApiVersion("v1", "Customer Profile Service")).EnableSwaggerUi();
        }
    }
}
