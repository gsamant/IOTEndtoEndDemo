using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SendEvents
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public DateTime EventProcessedUtcTime { get; set; }
        [DataMember]
        public int PartitionId { get; set; }
        [DataMember]
        public DateTime EventEnqueuedUtcTime { get; set; }
        [DataMember]
        public DateTime TimeStamp1 { get; set; }
        [DataMember]
        public string  Temperature { get; set; }
        [DataMember]
        public string Pressure { get; set; }
        [DataMember]
        public string DeviceId { get; set; }
}
}