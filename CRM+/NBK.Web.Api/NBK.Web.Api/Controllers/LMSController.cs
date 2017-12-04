using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NBK.Web.Api.Models;

using Newtonsoft.Json;

namespace NBK.Web.Api.Controllers
{
    [RoutePrefix("api/LMS")]
    public class LMSController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ActiveCampaignLeads/{customerNo}")]
        public IHttpActionResult ActiveCampaignLeads(string customerNo)
        {
            List<CampaignLead> leadList = new List<CampaignLead>();
            using (var context = new LMSEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
               
                var query = from a in context.CampaignLeads
                            join b in context.Campaigns on a.CampaignID equals b.CampaignID
                            where a.Closed == 0 && a.CustomerNo == customerNo
                            select new { CampaignLead = a, Campaign = b };

                foreach (var item in query)
                {
                    CampaignLead campaignLead = item.CampaignLead;
                    campaignLead.CampaignName = item.Campaign.CampaignName;
                    leadList.Add(campaignLead);
                }
            }
            return Json(leadList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("LeadMonitor/{userID}")]
        public IHttpActionResult LeadMonitor(string userID)
        {
            List<InstantLead> leadList = new List<InstantLead>();
            using (var context = new LMSEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;

                var query = from a in context.InstantLeads
                            join b in context.InstantLeadTypes on a.TypeID equals b.TypeID
                            where a.OriginatorID == userID
                            select new { InstantLead = a, InstantLeadTypes = b };

                foreach (var item in query)
                {
                    InstantLead lead = item.InstantLead;
                    lead.InstantLeadName = item.InstantLeadTypes.Name;
                    try {
                        if (lead.Closed == 1)
                        {
                            lead.RemainHours = 0;
                            lead.PercentRemain = 0;
                        }
                        else
                        {
                            TimeSpan _ts = DateTime.Now.Subtract((DateTime)lead.CreateDate);
                            lead.RemainHours = (int)item.InstantLeadTypes.SLA - (int)_ts.TotalHours;
                            if ((int)item.InstantLeadTypes.SLA > 0)
                            {
                                lead.PercentRemain = ((double)lead.RemainHours / (int)item.InstantLeadTypes.SLA) * 100;
                                if (lead.PercentRemain > 0)
                                {
                                    lead.PercentRemain = 100 - lead.PercentRemain;
                                }
                               
                            }
                            else
                            {
                                lead.PercentRemain = 0;
                            }
                        }
                    }
                    catch
                    {

                    }
                    leadList.Add(lead);
                }
            }
            return Json(leadList);
        }

    }
}
