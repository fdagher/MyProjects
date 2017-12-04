using System.Web.Http;
using Owin;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using IdentityServer3.AccessTokenValidation;

namespace CustomerProfileService
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            appBuilder.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:5000/",
                ValidationMode = ValidationMode.ValidationEndpoint,
                RequiredScopes = new[] { "customersapi" }
            });

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            // Swagger
            SwaggerConfig.Register(config);

            // Enable Json
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            // Map Routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "services/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // require authorization for all controllers
            config.Filters.Add(new AuthorizeAttribute());

            // Use Web API
            appBuilder.UseWebApi(config);
        }
    }
}
