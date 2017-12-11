using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitApi.Models
{
    public class Body
    {
        public string RepoUrl { get; set; }

        public string LocalFolder { get; set; }

        public string Comment { get; set; }
    }
}