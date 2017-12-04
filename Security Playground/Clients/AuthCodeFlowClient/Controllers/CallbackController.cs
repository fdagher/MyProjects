using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthCodeFlowClient.Controllers
{
    public class CallbackController : Controller
    {
        public async Task<ActionResult> Index([FromQuery]string code, [FromQuery]string state, [FromQuery]string error)
        {
            ViewBag.Code = code ?? "none";

            //var tempState = await GetTempStateAsync();

            //if (state.Equals(tempState.Item1, StringComparison.Ordinal))
            //{
            //    ViewBag.State = state + " (valid)";
            //}
            //else
            //{
            //    ViewBag.State = state + " (invalid)";
            //}
            ViewBag.State = state + " (valid)";

            ViewBag.Error = error ?? "none";

            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> GetToken([FromQuery]string code)
        {
            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:5001").Result;

            var client = new TokenClient(
                disco.TokenEndpoint,
                "code.client",
                "secret");

            //var tempState = await GetTempStateAsync();
            //Request.GetOwinContext().Authentication.SignOut("TempState");

            var response = await client.RequestAuthorizationCodeAsync(code, "http://localhost:5760/callback");

            //await ValidateResponseAndSignInAsync(response, tempState.Item2);

            if (!string.IsNullOrEmpty(response.IdentityToken))
            {
                ViewBag.IdentityTokenParsed = ParseJwt(response.IdentityToken);
            }
            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                ViewBag.AccessTokenParsed = ParseJwt(response.AccessToken);
            }

            return View("Token", response);
        }

        private string ParseJwt(string token)
        {
            if (!token.Contains("."))
            {
                return token;
            }

            var parts = token.Split('.');
            var part = Encoding.UTF8.GetString(Base64Url.Decode(parts[1]));

            var jwt = JObject.Parse(part);
            return jwt.ToString();
        }
    }
}
