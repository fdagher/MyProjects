using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBK.Web.CRM.Models.LMS
{
    public partial class CampaignLead
    {
        public System.Guid LeadID { get; set; }
        public int CampaignID { get; set; }
        public short LeadType { get; set; }
        public short ChannelID { get; set; }
        public string Channel { get; set; }
        public string BranchNo { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public short Status { get; set; }
        public string AdminID { get; set; }
        public string DistributorID { get; set; }
        public string ExecutorID { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<short> Contacted { get; set; }
        public Nullable<short> Closed { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public string TeamName { get; set; }

        public string CampaignName { get; set; }

    }


    public class InstantLead
    {
      
        public System.Guid LeadID { get; set; }
        public int TypeID { get; set; }
        public string Channel { get; set; }
        public string BranchNo { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string AdminID { get; set; }
        public string DistributorID { get; set; }
        public string ExecutorID { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<short> Contacted { get; set; }
        public Nullable<short> Closed { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public string OriginatorID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string LeadSource { get; set; }
        public Nullable<short> Transfered { get; set; }
        public string MappingQuestion { get; set; }
        public string MapValue { get; set; }
        public Nullable<int> ParentTypeID { get; set; }
        public string InstantLeadName { get; set; }
        public int RemainHours { get; set; }
        public virtual double PercentRemain { get; set; }

    }
}