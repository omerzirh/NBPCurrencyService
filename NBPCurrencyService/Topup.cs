using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NBPCurrencyService
{
    [DataContract]
    public class Topup
    {
        [DataMember]
        public string topupResult { get; set; }

        [DataMember]
        public double amount { get; set; }
    }
}