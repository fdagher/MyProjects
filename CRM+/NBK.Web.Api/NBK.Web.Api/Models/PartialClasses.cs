using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBK.Web.Api.Models
{
    public partial class CampaignLead
    {
        public string CampaignName { get; set; }
    }

    public partial class InstantLead
    {
        public string InstantLeadName { get; set; }
        public  int RemainHours { get; set; }
        public virtual double PercentRemain { get; set; }
       
    }
}