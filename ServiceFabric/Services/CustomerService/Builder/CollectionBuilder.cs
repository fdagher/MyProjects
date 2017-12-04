using Contracts;
using CustomerService.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CustomerService.Builder
{
    internal class CollectionBuilder
    {
        private static XmlDocument businessTermsDocument;

        private static PropertyInfo[] CustomerProperties { get; set; }

        private static PropertyInfo[] AddressProperties { get; set; }

        static CollectionBuilder()
        {
            businessTermsDocument = new XmlDocument();
            businessTermsDocument.Load("BusinessTermMapping.xml");
        }

        internal static IEnumerable<Code> BuildCustomerCodesRequest(Customer customer)
        {
            IList<Code> _custcodes = new List<Code>();

            if (CustomerProperties == null)
            {
                CustomerProperties = customer.GetType().GetProperties();
            }

            // write property names
            foreach (PropertyInfo propertyInfo in CustomerProperties)
            {
                if (propertyInfo.Name.EndsWith("Code") && propertyInfo.GetValue(customer) != null)
                {
                    _custcodes.Add(
                        new Code
                        {
                            BusinessTerm = businessTermsDocument.SelectSingleNode("/Mappings/Property[@Name='" + propertyInfo.Name + "']").Attributes["BusinessTermName"].InnerText,
                            CodeValue = propertyInfo.GetValue(customer).ToString(),
                            PropertyName = propertyInfo.Name
                        });
                }
            }

            return _custcodes;
        }

        internal static IEnumerable<Code> BuildAddressCodesRequest(CustomerAddress address)
        {
            IList<Code> _addrcodes = new List<Code>();

            if (AddressProperties == null)
            {
                AddressProperties = address.GetType().GetProperties();
            }

            // write property names
            foreach (PropertyInfo propertyInfo in AddressProperties)
            {
                if (propertyInfo.Name.EndsWith("Code") && propertyInfo.GetValue(address) != null)
                {
                    _addrcodes.Add(
                        new Code
                        {
                            BusinessTerm = businessTermsDocument.SelectSingleNode("/Mappings/Property[@Name='" + propertyInfo.Name + "']").Attributes["BusinessTermName"].InnerText,
                            CodeValue = propertyInfo.GetValue(address).ToString(),
                            PropertyName = propertyInfo.Name
                        });
                }
            }

            return _addrcodes;
        }
    }
}
