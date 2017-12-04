using System.Runtime.Serialization;

namespace CustomerProfileService.DTOs
{
    [DataContract]
    public class CustomerProfile
    {
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public string TotalCashLimit { get; set; }
        [DataMember]
        public string TotalNonCashLimit { get; set; }
        [DataMember]
        public string TotalEarnedAssets { get; set; }
        [DataMember]
        public string TotalFreeFunds { get; set; }
        [DataMember]
        public string TotalCashLiability { get; set; }
        [DataMember]
        public string TotalIndirectLiability { get; set; }
        [DataMember]
        public string TotalNonCashLiability { get; set; }
        [DataMember]
        public string TotalCollateral { get; set; }
        [DataMember]
        public string TotalNBKFunds { get; set; }
    }
}