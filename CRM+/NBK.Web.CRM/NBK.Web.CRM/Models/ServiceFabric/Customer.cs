using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBK.Web.CRM.Models.ServiceFabric
{
    public class Customer
    {
        public string CustomerNumber { get; set; }
        public Nullable<DateTime> EstablishDate { get; set; }
        public string FullNameEnglish { get; set; }
        public string ShortName { get; set; }
        public string TitleCode { get; set; }
        public string Title { get; set; }

        public string StatusCode { get; set; }
        public string Status { get; set; }

        public string PackageCode { get; set; }
        public string Package { get; set; }
        public string LanguageCode { get; set; }
        public string Language { get; set; }
        public string Gender { get; set; }
        public string NationalityCode { get; set; }
        public string Nationality { get; set; }

        public string OriginCountryCode { get; set; }
        public string OriginCountry { get; set; }

        public string CountryOfBirthCode { get; set; }
        public string CountryOfBirth { get; set; }

        public string MaritalStatusCode { get; set; }
        public string MaritalStatus { get; set; }

        public string EducationCode { get; set; }
        public string Education { get; set; }

        public Nullable<DateTime> BirthDate { get; set; }


        public string IdIssuePlaceCode { get; set; }
        public string IdIssuePlace { get; set; }

     
        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }
      

        //Temp field to set image
        public string Image { get; set; }

        //Identity Info

        public string ResidencyStatusCode { get; set; }
        public string ResidencyStatus { get; set; }

        public string IdentTypeCode { get; set; }
        public string IdentType { get; set; }

        public string IdentNumber { get; set; }
        public Nullable<DateTime> IdentExpiryDate { get; set; }

        //Bank Info
        public string BranchCode { get; set; }
        public string BranchName { get; set; }

        //CustType info

        public string CustTypeCode { get; set; }
        public string CustType { get; set; }
        public string GroupType { get; set; }

        //Contact Info

        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        //Employment info

        public string WorkplaceCode { get; set; }
        public string Workplace { get; set; }

        public string OccupationCode { get; set; }
        public string Occupation { get; set; }

        public string JobRankCode { get; set; }
        public string JobRank { get; set; }

        //Income info
        public string IncomeSourceCode { get; set; }
        public string IncomeSource { get; set; }

        public string SalaryRangeCode { get; set; }
        public string SalaryRange { get; set; }
        public string Salary { get; set; }
        public string Wealth { get; set; }

        public double ServiceResponseTime { get; set; }

    }


  


    public class Address
    {
        public string AreaCode { get; set; }
        public string Area { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string BldgPlot { get; set; }
        public string Avenue { get; set; }
        public string UnitNum { get; set; }
        public string FloorNum { get; set; }

        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Addr3 { get; set; }
        public string Addr4 { get; set; }
    }


    public class FinancialProfile
    {
        public Nullable<decimal> TotalCashLimit { get; set; }
        public Nullable<decimal> TotalNonCashLimit { get; set; }
        public Nullable<decimal> TotalEarnedAssets { get; set; }
        public Nullable<decimal> TotalFreeFunds { get; set; }
        public Nullable<decimal> TotalCashLiability { get; set; }
        public Nullable<decimal> TotalIndirectLiability { get; set; }
        public Nullable<decimal> TotalNonCashLiability { get; set; }
        public Nullable<decimal> TotalCollateral { get; set; }
        public Nullable<decimal> TotalNBKFunds { get; set; }
    }


}