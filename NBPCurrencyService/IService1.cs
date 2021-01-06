using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NBPCurrencyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
          UriTemplate = "/show/{currencyName}",
          BodyStyle = WebMessageBodyStyle.Wrapped,
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        Currency GiveCurentRate(string currencyName);

        [OperationContract]
        [WebInvoke(Method = "GET",
           UriTemplate = "/topup/{key}/{amount}",
           BodyStyle = WebMessageBodyStyle.Wrapped,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        Topup CheckTopup(string key, string amount);

        [OperationContract]
        [WebInvoke(Method = "GET",
           UriTemplate = "/convert/{fromCur}/{toCur}/{amount}",
           BodyStyle = WebMessageBodyStyle.Wrapped,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        Converted Result(string fromCur, string toCur, string amount);


        [OperationContract]
        [WebInvoke(Method = "GET",
           UriTemplate = "/oldrate/{fromCur}/{toCur}/{date}",
           BodyStyle = WebMessageBodyStyle.Wrapped,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        Converted OldRates(string fromCur, string toCur, string date);


    }
}
