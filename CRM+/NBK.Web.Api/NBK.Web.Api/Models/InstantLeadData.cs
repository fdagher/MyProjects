//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NBK.Web.Api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InstantLeadData
    {
        public System.Guid FieldDataID { get; set; }
        public System.Guid LeadID { get; set; }
        public int FieldID { get; set; }
        public string FieldData { get; set; }
    
        public virtual InstantLead InstantLead { get; set; }
    }
}
