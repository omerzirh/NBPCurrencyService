using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NBPCurrencyService
{
    [DataContract]
    public class Rate
    {
        [DataMember]
        public string effectiveDate { get; set; }
        [DataMember]
        public double bid { get; set; }
        [DataMember]
        public double ask { get; set; }
    }
}