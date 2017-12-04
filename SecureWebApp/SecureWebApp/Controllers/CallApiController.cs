using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SecureWebApp.Controllers
{
    public class CallApiController : Controller
    {
        // GET: CallApi
        public ActionResult Index()
        {
            return View();
        }

        // GET: CallApi/ClientCredentials
        public async Task<ActionResult> ClientCredentials()
        {
            string token = null;
            var user = User as ClaimsPrincipal;

            Claim tokenClaim = user.FindFirst("access_token");

            // access token was requested as part of the authentication call
            if (tokenClaim != null)
            {
                token = tokenClaim.Value;
            }
            else
            {
                var response = await GetTokenAsync();
                token = response.AccessToken;
            }

            var result = await CallApi(token);

            ViewBag.Json = result;

            return View("ShowApiResult");
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            var client = new TokenClient(
                "http://localhost:5000/connect/token",
                "crm_service",
                "B443D9C2-D068-4542-B148-D58003022CEA");

            return await client.RequestClientCredentialsAsync("customersapi");
        }

        private async Task<string> CallApi(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);

            //var json = await client.GetStringAsync("http://localhost:8323/api/test");
            var json = await client.GetStringAsync("http://localhost:8380/services/customer/get/000000094");

            return json;
        }
    }
}