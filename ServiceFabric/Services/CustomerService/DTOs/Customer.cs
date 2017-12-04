using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.DTOs
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public Nullable<System.DateTime> BirthDate { get; set; }
        [DataMember]
        public string CivilID { get; set; }
        [DataMember]
        public string CountryOfBirthCode { get; set; }
        [DataMember]
        public string CountryOfBirth { get; set; }
        [DataMember]
        public string OriginCountryCode { get; set; }
        [DataMember]
        public string OriginCountry { get; set; }
        [DataMember]
        public string GroupType { get; set; }
        [DataMember]
        public string SalaryRangeCode { get; set; }
        [DataMember]
        public string SalaryRange { get; set; }
        [DataMember]
        public int? IncomeSourceCode { get; set; }
        [DataMember]
        public string IncomeSource { get; set; }
        [DataMember]
        public string EducationCode { get; set; }
        [DataMember]
        public string Education { get; set; }
        [DataMember]
        public string JobRankCode{ get; set; }
        [DataMember]
        public string JobRank { get; set; }
        [DataMember]
        public string WorkplaceCode { get; set; }
        [DataMember]
        public string Workplace { get; set; }
        [DataMember]
        public string FullNameArabic { get; set; }
        [DataMember]
        public string FullNameEnglish { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public Nullable<decimal> SalaryAmount { get; set; }
        [DataMember]
        public string MaritalStatusCode { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string NationalityCode { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public string OccupationCode { get; set; }
        [DataMember]
        public string Occupation { get; set; }
        [DataMember]
        public string PackageCode { get; set; }
        [DataMember]
        public string Package { get; set; }
        [DataMember]
        public string ResidencyStatusCode { get; set; }
        [DataMember]
        public string ResidencyStatus { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string IdentTypeCode { get; set; }
        [DataMember]
        public string IdentType { get; set; }
        [DataMember]
        public string IdentNumber { get; set; }
        [DataMember]
        public string IdIssuePlaceCode { get; set; }
        [DataMember]
        public string IdIssuePlace { get; set; }
        [DataMember]
        public Nullable<System.DateTime> IdentExpiryDate { get; set; }
        [DataMember]
        public string CustTypeCode { get; set; }
        [DataMember]
        public string CustType { get; set; }
        [DataMember]
        public string StatusCode { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string LanguageCode { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public string TitleCode { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string HomePhone { get; set; }
        [DataMember]
        public string MobilePhone { get; set; }
    }
}
