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
    
    public partial class SurveyAnswer_History
    {
        public System.Guid AnswerID { get; set; }
        public System.Guid ContactID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public Nullable<int> AnswerNumber { get; set; }
    }
}
