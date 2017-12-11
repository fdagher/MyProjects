using GitApi.Models;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitApi.Controllers
{
    public class RepositoriesController : ApiController
    {
        // GET: api/Repository
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Repository/5
        public string Get(int id)
        {
            return "value";
        }

        public IHttpActionResult Post([FromBody]Repo value)
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string remote = ConfigurationManager.AppSettings["RemoteName"];
            string branch = ConfigurationManager.AppSettings["RefName"];
            value.LocalFolder = value.LocalFolder.Replace("\\", @"\");

            try
            {
                Repository.Init(value.LocalFolder, value.RepoUrl);
                Repository.IsValid(value.LocalFolder);
                Repository.Clone(value.RepoUrl, value.LocalFolder);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // DELETE: api/Repository/5
        public void Delete(int id)
        {
        }
    }
}
