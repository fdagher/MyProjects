using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web.Helpers;
using System.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using IdentityModel.Client;
using System.Linq;
using System;

namespace SecureWebApp
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
            });            
            
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "http://localhost:5000/",
                ClientId = "mvc",
                //Scope = "openid profile roles",
                Scope = "openid profile roles customersapi",
                RedirectUri = "http://localhost:23138/",
                //ResponseType = "id_token",
                ResponseType = "id_token token",        //call the api on behalf of the user. Get the id token & access token together
                SignInAsAuthenticationType = "Cookies",
                UseTokenLifetime = false,

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = n =>
                    {
                        var id = n.AuthenticationTicket.Identity;

                        // we want to keep first name, last name, subject and roles, comment when access token is requested
                        //var givenName = id.FindFirst("given_name");
                        //var familyName = id.FindFirst("family_name");
                        //var sub = id.FindFirst("sub");
                        //var roles = id.FindAll("role");

                        // create new identity and set name and role claim type
                        var nid = new ClaimsIdentity(
                            id.AuthenticationType,
                            "given_name",
                            "role");

                        // comment when access token is requested
                        //nid.AddClaim(givenName);
                        //nid.AddClaim(familyName);
                        //nid.AddClaim(sub);
                        //nid.AddClaims(roles);

                        // get userinfo data. Add when access token is requested
                        var userInfoClient = new UserInfoClient(n.Options.Authority + "/connect/userinfo");

                        var userInfo = userInfoClient.GetAsync(n.ProtocolMessage.AccessToken);
                        userInfo.Result.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Type, ui.Value)));


                        // add some other app specific claim
                        nid.AddClaim(new Claim("app_specific", "some data"));

                        // add id token
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        // add access token for sample API. Add when access token is requested
                        nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                        // keep track of access token expiration. Add when access token is requested
                        nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            nid,
                            n.AuthenticationTicket.Properties);

                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }

                        return Task.FromResult(0);
                    }
                }
            });

            //AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
        }

    }
}