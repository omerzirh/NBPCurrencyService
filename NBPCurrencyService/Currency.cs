using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NBPCurrencyService
{
    [DataContract]
    public class Currency
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public List<Rate> list = new List<Rate>();
    }
}