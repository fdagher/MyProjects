using System;
using System.Collections.Generic;
using System.Linq;

namespace NBK.Web.Api.Models
{
    public class CustomerOld
    {
        public string CustomerNumber { get; set; }
        public Nullable<DateTime> EstablishDate { get; set; }
        public string FullName { get; set; }
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


        public IdentityInfo IdentityInfo { get; set; }
        public BankInfo BankInfo { get; set; }
        public CustTypeInfo CustTypeInfo { get; set; }
        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }
        public ContactInfo ContactInfo { get; set; }

        public EmploymentInfo EmploymentInfo { get; set; }
        public IncomeInfo IncomeInfo { get; set; }

        //Temp field to set image
        public string Image { get; set; }
        public double ServiceResponseTime { get; set; }

    }


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

        public string ResidenceStatusCode { get; set; }
        public string ResidenceStatus { get; set; }

        public string IdentTypeCode { get; set; }
        public string IdentType { get; set; }

        public string IdentNumber { get; set; }
        public Nullable<DateTime> IdentExpiryDate { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string CustTypeCode { get; set; }
        public string CustType { get; set; }
        public string GroupType { get; set; }
    
        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string WorkplaceCode { get; set; }
        public string Workplace { get; set; }

        public string OccupationCode { get; set; }
        public string Occupation { get; set; }

        public string JobRankCode { get; set; }
        public string JobRank { get; set; }
        public string IncomeSourceCode { get; set; }
        public string IncomeSource { get; set; }

        public string SalaryRangeCode { get; set; }
        public string SalaryRange { get; set; }
        public string Salary { get; set; }
        public string Wealth { get; set; }

        //Temp field to set image
        public string Image { get; set; }
        public double ServiceResponseTime { get; set; }

        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }

    }

    public class IdentityInfo
    {
        public string ResidenceStatusCode { get; set; }
        public string ResidenceStatus { get; set; }

        public string IdentTypeCode { get; set; }
        public string IdentType { get; set; }

        public string IdentNumber { get; set; }
        public Nullable<DateTime> IdentExpiryDate { get; set; }

    }

    public class BankInfo
    {
        public Nullable<DateTime> EstablishDate { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }

    }

    public class CustTypeInfo
    {
        public string CustTypeCode { get; set; }
        public string CustType { get; set; }
        public string GroupType { get; set; }
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

    public class ContactInfo
    {
        public string HomePhone { get; set;}
        public string Mobile { get; set; }
        public string Email { get; set; }
    }

    public class EmploymentInfo
    {
        public string WorkplaceCode { get; set; }
        public string Workplace { get; set; }

        public string OccupationCode { get; set; }
        public string Occupation { get; set; }

        public string JobRankCode { get; set; }
        public string JobRank { get; set; }
    }

    public class IncomeInfo
    {
        public string IncomeSourceCode { get; set; }
        public string IncomeSource { get; set; }

        public string SalaryRangeCode { get; set; }
        public string SalaryRange { get; set; }
        public string Salary { get; set; }
        public string Wealth { get; set; }
    }


    public class Alert
    {
        public string Type { get; set; }
        public string Indicator { get; set; }
    }

 
}