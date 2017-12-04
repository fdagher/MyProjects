using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class Code
    {
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        public string BusinessTerm { get; set; }
        [DataMember]
        public string CodeValue { get; set; }
        [DataMember]
        public string CodeDescription { get; set; }
    }
}
