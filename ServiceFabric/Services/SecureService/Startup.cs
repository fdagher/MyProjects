using System.Web.Http;
using Owin;
using IdentityServer3.AccessTokenValidation;

namespace SecureService
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Add identity server trust code
            appBuilder.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:5000/",
                ValidationMode = ValidationMode.ValidationEndpoint,
                RequiredScopes = new[] { "customersapi" }
            });

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // require authorization for all controllers
            config.Filters.Add(new AuthorizeAttribute());

            appBuilder.UseWebApi(config);
        }
    }
}
