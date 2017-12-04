using REPO = Repository;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using CustomerService.DTOs;
using System;
using Microsoft.ServiceFabric.Services.Client;
using System.Fabric;
using System.Threading.Tasks;
using CustomerService.Communication;
using Microsoft.ServiceFabric.Services.Communication.Client;
using System.Net.Http;
using Contracts;
using CustomerService.Builder;
using System.Security.Claims;

namespace CustomerService.Controllers
{
    [ServiceRequestActionFilter]
    public class CustomerController : ApiController
    {
        private static readonly Uri serviceUri;

        private static readonly FabricClient fabricClient;

        private static readonly HttpCommunicationClientFactory communicationFactory;

        internal REPO.ECIFEntities context = new REPO.ECIFEntities();

        static CustomerController()
        {
            serviceUri = new Uri(FabricRuntime.GetActivationContext().ApplicationName + "/ECTSService");
            fabricClient = new FabricClient();
            communicationFactory = new HttpCommunicationClientFactory(new ServicePartitionResolver(() => fabricClient));
        }

        // GET services/customer/get/5 
        public async Task<Customer> Get(string id)
        {
            REPO.Customer _cust = (from c in context.Customers
                                   where c.CustomerNumber == id
                                   select c).FirstOrDefault();

            Customer _final = await EnrichCustomer(_cust);

            return _final;
        }

        // GET services/customer/all/5 
        [HttpGet]
        public async Task<IEnumerable<Customer>> All(string id)
        {
            IEnumerable<REPO.Customer> _custs = (from c in context.Customers
                    where c.CivilID == id
                    select c);
            List<Customer> _custsdtos = new List<Customer>();

            foreach(REPO.Customer c in _custs)
            {
                _custsdtos.Add(await EnrichCustomer(c));
            }

            return _custsdtos;
        }

        // GET services/customer/all/5 
        [HttpGet]
        public async Task<CustomerAddress> Address(string id)
        {
            REPO.Customer _cust = (from c in context.Customers
                                                 where c.CustomerNumber == id
                                                 select c).FirstOrDefault();

            IEnumerable<REPO.Address> _addresses = _cust.Addresses.Where(a => a.AddressTypeCode == "01" || a.AddressTypeCode == "02");

            CustomerAddress _custsaddr = new CustomerAddress();

            _custsaddr.CustomerNumber = _cust.CustomerNumber;

            foreach (REPO.Address a in _addresses)
            {
                this.TransformAddress(a, _custsaddr);
            }

            ServicePartitionClient<HttpCommunicationClient> partitionClient
                        = new ServicePartitionClient<HttpCommunicationClient>(communicationFactory, serviceUri, new ServicePartitionKey());

            await partitionClient.InvokeWithRetryAsync(
                async (client) =>
                {
                    IEnumerable<Code> _request = CollectionBuilder.BuildAddressCodesRequest(_custsaddr);
                    
                    HttpResponseMessage response = await client.HttpClient.PostAsJsonAsync(new Uri(client.Url, "Services/Codes"), _request);

                    IEnumerable<Code> _codes = await response.Content.ReadAsAsync<IEnumerable<Code>>();

                    _custsaddr = this.AddEnterpriseCodesToAddress(_custsaddr, _codes);
                });

            return _custsaddr;
        }

        #region Customer-related Methods
        private async Task<Customer> EnrichCustomer(REPO.Customer cust)
        {
            Customer _final = this.TransformCustomer(cust);

            _final.HomePhone = cust.Phones.Where(p => p.PhoneTypeID == "01").FirstOrDefault() != null ? cust.Phones.Where(p => p.PhoneTypeID == "01").FirstOrDefault().PhoneNumber : null;
            _final.MobilePhone = cust.Phones.Where(p => p.PhoneTypeID == "04").FirstOrDefault() != null ? cust.Phones.Where(p => p.PhoneTypeID == "04").FirstOrDefault().PhoneNumber : null;

            string _accessToken = (User as ClaimsPrincipal).FindFirst("token").Value;

            ServicePartitionClient<HttpCommunicationClient> partitionClient
                        = new ServicePartitionClient<HttpCommunicationClient>(communicationFactory, serviceUri, new ServicePartitionKey());

            await partitionClient.InvokeWithRetryAsync(
                async (client) =>
                {
                    IEnumerable<Code> _request = CollectionBuilder.BuildCustomerCodesRequest(_final);

                    client.HttpClient.SetBearerToken(_accessToken);

                    HttpResponseMessage response = await client.HttpClient.PostAsJsonAsync(new Uri(client.Url, "Services/Codes"), _request);

                    IEnumerable<Code> _codes = await response.Content.ReadAsAsync<IEnumerable<Code>>();

                    _final = this.AddEnterpriseCodesToCustomer(_final, _codes);
                });

            return _final;
        }

        private Customer TransformCustomer(REPO.Customer c)
        {
            return new Customer
            {
                BirthDate = c.BirthDate,
                CivilID = c.CivilID,
                CountryOfBirthCode = c.CountryOfBirth,
                OriginCountryCode = c.CountryOriginCode,
                GroupType = c.CustomerGroup,
                CustomerNumber = c.CustomerNumber,
                StatusCode = c.CustomerStatus,
                CustTypeCode = c.NBKCustomerType,
                EducationCode = c.EducationCode,
                FullNameArabic = c.FullNameArabic,
                FullNameEnglish = c.FullNameEnglish,
                Gender = c.GenderCode,
                IdentExpiryDate = c.IDExpiryDate,
                IdentNumber = c.IDNumber,
                IdentTypeCode = c.IDTypeCode,
                IdIssuePlaceCode = c.IDNationalityCode,
                IncomeSourceCode = c.CustomerIncomeTypeID,
                JobRankCode = c.Role_PositionCode,
                LanguageCode = c.LanguageCode,
                MaritalStatusCode = c.MaritalStatusCode,
                //NationalityCode = c.NationalityCode,
                NationalityCode = c.CountryOriginCode,
                OccupationCode = c.OccupationCode,
                PackageCode = c.PackageTypeCode,
                ResidencyStatusCode = c.ResidencyStatusCode,
                SalaryAmount = c.SalaryAmount,
                SalaryRangeCode = c.SalaryRangeCode,
                ShortName = c.ShortName,
                Title = c.Title,
                WorkplaceCode = c.EmployerCode,
                Email = c.Email
            };
        }

        private Customer AddEnterpriseCodesToCustomer(Customer customer, IEnumerable<Code> codes)
        {
            customer.CountryOfBirth = codes.Where(x => x.PropertyName == "CountryOfBirthCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "CountryOfBirthCode").FirstOrDefault().CodeDescription : null;
            customer.CustType = codes.Where(x => x.PropertyName == "CustTypeCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "CustTypeCode").FirstOrDefault().CodeDescription : null;
            customer.Education = codes.Where(x => x.PropertyName == "EducationCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "EducationCode").FirstOrDefault().CodeDescription : null;
            customer.IdentType  = codes.Where(x => x.PropertyName == "IdentTypeCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "IdentTypeCode").FirstOrDefault().CodeDescription : null;
            customer.IdIssuePlace = codes.Where(x => x.PropertyName == "IdIssuePlaceCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "IdIssuePlaceCode").FirstOrDefault().CodeDescription : null;
            customer.IncomeSource = codes.Where(x => x.PropertyName == "IncomeSourceCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "IncomeSourceCode").FirstOrDefault().CodeDescription : null;
            customer.JobRank = codes.Where(x => x.PropertyName == "JobRankCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "JobRankCode").FirstOrDefault().CodeDescription : null;
            customer.Language = codes.Where(x => x.PropertyName == "LanguageCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "LanguageCode").FirstOrDefault().CodeDescription : null;
            customer.MaritalStatus = codes.Where(x => x.PropertyName == "MaritalStatusCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "MaritalStatusCode").FirstOrDefault().CodeDescription : null;
            customer.Nationality = codes.Where(x => x.PropertyName == "NationalityCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "NationalityCode").FirstOrDefault().CodeDescription : null;
            customer.Occupation = codes.Where(x => x.PropertyName == "OccupationCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "OccupationCode").FirstOrDefault().CodeDescription : null;
            customer.OriginCountry = codes.Where(x => x.PropertyName == "OriginCountryCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "OriginCountryCode").FirstOrDefault().CodeDescription : null;
            customer.Package = codes.Where(x => x.PropertyName == "PackageCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "PackageCode").FirstOrDefault().CodeDescription : null;
            customer.ResidencyStatus = codes.Where(x => x.PropertyName == "ResidencyStatusCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "ResidencyStatusCode").FirstOrDefault().CodeDescription : null;
            customer.SalaryRange = codes.Where(x => x.PropertyName == "SalaryRangeCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "SalaryRangeCode").FirstOrDefault().CodeDescription : null;
            customer.Status = codes.Where(x => x.PropertyName == "StatusCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "StatusCode").FirstOrDefault().CodeDescription : null;
            customer.Title = codes.Where(x => x.PropertyName == "TitleCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "TitleCode").FirstOrDefault().CodeDescription : null;
            customer.Workplace = codes.Where(x => x.PropertyName == "WorkplaceCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "WorkplaceCode").FirstOrDefault().CodeDescription : null;

            return customer;
        }
        #endregion

        #region Address-related Methods
        private void TransformAddress(REPO.Address addr, CustomerAddress custAddr)
        {
            if (addr.AddressTypeCode == "01")
            {
                custAddr.HomeAddr1 = addr.AddressExtendedInfo.AddressLine1;
                custAddr.HomeAddr2 = addr.AddressExtendedInfo.AddressLine2;
                custAddr.HomeAddr3 = addr.AddressExtendedInfo.AddressLine3;
                custAddr.HomeAddr4 = addr.AddressExtendedInfo.AddressLine4;
                custAddr.HomeAreaCode = addr.Area;
                custAddr.HomeAvenue = addr.AvenueNo;
                custAddr.HomeBlock = addr.BlockNumber;
                custAddr.HomeBuildingPlot = addr.BuildingPlot;
                custAddr.HomeFloorNum = addr.FloorNumber;
                custAddr.HomeStreet = addr.Street;
                custAddr.HomeUnitNum = addr.UnitNumber;
            }
            else
            {
                custAddr.WorkAddr1 = addr.AddressExtendedInfo.AddressLine1;
                custAddr.WorkAddr2 = addr.AddressExtendedInfo.AddressLine2;
                custAddr.WorkAddr3 = addr.AddressExtendedInfo.AddressLine3;
                custAddr.WorkAddr4 = addr.AddressExtendedInfo.AddressLine4;
                custAddr.WorkAreaCode = addr.Area;
            }
        }

        private CustomerAddress AddEnterpriseCodesToAddress(CustomerAddress address, IEnumerable<Code> codes)
        {
            address.HomeArea = codes.Where(x => x.PropertyName == "HomeAreaCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "HomeAreaCode").FirstOrDefault().CodeDescription : null;
            address.WorkArea = codes.Where(x => x.PropertyName == "WorkAreaCode").FirstOrDefault() != null ? codes.Where(x => x.PropertyName == "WorkAreaCode").FirstOrDefault().CodeDescription : null;
            
            return address;
        }
        #endregion
    }
}
