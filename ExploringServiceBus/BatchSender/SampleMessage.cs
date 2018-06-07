using System.Runtime.Serialization;

namespace BatchSender
{
    [DataContract]
    public class SampleMessage
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}