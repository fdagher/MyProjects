using ECTSRepository;
using Contracts;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System;
using System.Linq;

namespace ECTSService.Controllers
{
    [ServiceRequestActionFilter]
    public class CodesController : ApiController
    {
        // GET api/codes 
        public IEnumerable<string> Get()
        {
            return new string[] { "" };
        }

        // GET api/codes/5 
        public string Get(int id)
        {
            return "";
        }

        // POST api/codes 
        [ResponseType(typeof(IEnumerable<Code>))]
        public IHttpActionResult Post([FromBody]IEnumerable<Code> input)
        {
            
            IEnumerable<Entity> _inputs = this.TransformInput(input);

            IEnumerable<Entity> _outputs = Repository.GetCodeRecords(_inputs);

            IEnumerable<Code> _final = this.TransformOutput(_outputs);

            return Content(HttpStatusCode.Created, _final);
        }

        private IEnumerable<Entity> TransformInput(IEnumerable<Code> input)
        {
            return (from i in input
                    select new Entity { BusinessTerm = i.BusinessTerm, CodeValue = i.CodeValue, PropertyName = i.PropertyName });
        }

        private IEnumerable<Code> TransformOutput(IEnumerable<Entity> outputs)
        {
            return (from o in outputs
                    select new Code
                    {
                        BusinessTerm = o.BusinessTerm,
                        CodeValue = o.CodeValue, 
                        CodeDescription = o.CodeDescription,
                        PropertyName = o.PropertyName
                    });
        }

        // PUT api/codes/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/codes/5 
        public void Delete(int id)
        {
        }
    }
}
