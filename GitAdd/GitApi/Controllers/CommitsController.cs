using GitApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitApi.Controllers
{
    public class CommitsController : ApiController
    {
        // GET: api/Commit
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Commit/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Commit
        public IHttpActionResult Post([FromBody]Body value)
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string remote = ConfigurationManager.AppSettings["RemoteName"];
            string branch = ConfigurationManager.AppSettings["RefName"];
            value.LocalFolder = value.LocalFolder.Replace("\\", @"\");
            GitRepositoryManager manager = new GitRepositoryManager(username, password, value.RepoUrl, value.LocalFolder);

            bool result = manager.CommitAllChanges(value.Comment);

            if (result)
            {
                manager.PushCommits(remote, branch);

                return Ok();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Commit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Commit/5
        public void Delete(int id)
        {
        }
    }
}
