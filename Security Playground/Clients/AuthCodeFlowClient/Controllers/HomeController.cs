using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;

namespace AuthCodeFlowClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string scopes)
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");

            //SetTempState(state, nonce);

            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:5001").Result;

            var request = new AuthorizeRequest(disco.AuthorizeEndpoint);

            var url = request.CreateAuthorizeUrl(
                clientId: "code.client",
                responseType: "code",
                scope: scopes,
                redirectUri: "http://localhost:5760/callback",
                state: state,
                nonce: nonce);

            return Redirect(url);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
