using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.DTOs
{
    [DataContract]
    public class CustomerAddress
    {
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public string HomeAreaCode { get; set; }
        [DataMember]
        public string HomeArea { get; set; }
        [DataMember]
        public string HomeBlock { get; set; }
        [DataMember]
        public string HomeStreet { get; set; }
        [DataMember]
        public string HomeBuildingPlot { get; set; }
        [DataMember]
        public string HomeAvenue { get; set; }
        [DataMember]
        public string HomeUnitNum{ get; set; }
        [DataMember]
        public string HomeFloorNum { get; set; }
        [DataMember]
        public string HomeAddr1 { get; set; }
        [DataMember]
        public string HomeAddr2 { get; set; }
        [DataMember]
        public string HomeAddr3 { get; set; }
        [DataMember]
        public string HomeAddr4 { get; set; }
        [DataMember]
        public string WorkAreaCode { get; set; }
        [DataMember]
        public string WorkArea { get; set; }
        [DataMember]
        public string WorkAddr1 { get; set; }
        [DataMember]
        public string WorkAddr2 { get; set; }
        [DataMember]
        public string WorkAddr3 { get; set; }
        [DataMember]
        public string WorkAddr4 { get; set; }

    }
}
