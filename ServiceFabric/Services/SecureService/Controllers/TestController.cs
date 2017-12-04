using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace SecureService.Controllers
{
    [ServiceRequestActionFilter]
    public class TestController : ApiController
    {
        // GET api/test 
        public IHttpActionResult Get()
        {
            var caller = User as ClaimsPrincipal;

            //var subjectClaim = caller.FindFirst("sub");

            var claims = from c in caller.Claims
                         select new
                         {
                             type = c.Type,
                             value = c.Value
                         };

            return Json(claims);

            //if (subjectClaim != null)
            //{
            //    return Json(new
            //    {
            //        message = "OK user",
            //        client = caller.FindFirst("client_id").Value,
            //        subject = subjectClaim.Value
            //    });
            //}
            //else
            //{
            //    return Json(new
            //    {
            //        message = "OK computer",
            //        client = caller.FindFirst("client_id").Value
            //    });
            //}
        }
    }
}
