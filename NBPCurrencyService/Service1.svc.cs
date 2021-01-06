
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NBPCurrencyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {


        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }


        public Currency GiveCurentRate(string currencyName)
        {

            string URL = "http://api.nbp.pl/api/exchangerates/rates/c/" + currencyName.ToLower() + "/?format=json";
            string urlParameters = "?format=json";

            HttpClient client = new HttpClient();
            
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage responseHttp = client.GetAsync(URL).Result;

            var response = responseHttp.Content.ReadAsStringAsync();

            Console.WriteLine("response" + response);

            Currency currency = JsonConvert.DeserializeObject<Currency>(response.Result);

            JObject googleSearch = JObject.Parse(response.Result);

            IList<JToken> results = googleSearch["rates"].Children().ToList();


            IList<Rate> searchResults = new List<Rate>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                Rate rate = result.ToObject<Rate>();
                searchResults.Add(rate);
            }
            currency.list = searchResults.ToList<Rate>();


            return currency;
        }
        public Currency GiveOldRate(string currencyName, string date)
        {


            string URL = "http://api.nbp.pl/api/exchangerates/rates/c/" + currencyName.ToLower() + "/" + date.ToLower() + "/?format=json";
            string urlParameters = "?format=json";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage responseHttp = client.GetAsync(URL).Result;

            var response = responseHttp.Content.ReadAsStringAsync();

            Console.WriteLine("response" + response);

            Currency currency = JsonConvert.DeserializeObject<Currency>(response.Result);

            JObject googleSearch = JObject.Parse(response.Result);

            IList<JToken> results = googleSearch["rates"].Children().ToList();


            IList<Rate> searchResults = new List<Rate>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                Rate rate = result.ToObject<Rate>();
                searchResults.Add(rate);
            }
            currency.list = searchResults.ToList<Rate>();


            return currency;
        }

        public Converted Result(string fromCur, string toCur, string amount)
        {
            Currency from = GiveCurentRate(fromCur);
            Currency to = GiveCurentRate(toCur);
            int amountInt = Int32.Parse(amount);

            double fromRate = from.list.FirstOrDefault().ask;
            double toRate = to.list.FirstOrDefault().ask;
            string effectiveDateStr = to.list.FirstOrDefault().effectiveDate;


            double resultConversion = (fromRate / toRate) * amountInt;
            Math.Round(resultConversion, 3);
            return new Converted
            {
                result = resultConversion,
                effectiveDate = effectiveDateStr
            };
        }
        public Topup CheckTopup(string key, string amount)
        {
            string approveKey = "kjkszpj";
            double approvedAmount = 0.0;
            if (key.ToString() == approveKey)
            {
                approvedAmount = Double.Parse(amount);

                return new Topup
                {
                    topupResult = "Approved",
                    amount = approvedAmount
                };
            }
            else
            {
                approvedAmount = 0.0;
                return new Topup
                {
                    topupResult = "Declined",
                    amount = approvedAmount
                };
            }
        }

        public Converted OldRates(string fromCur, string toCur, string date)
        {
            Currency from = GiveOldRate(fromCur, date);
            Currency to = GiveOldRate(toCur, date);


            double fromRate = from.list.FirstOrDefault().ask;
            double toRate = to.list.FirstOrDefault().ask;
            string effectiveDateStr = to.list.FirstOrDefault().effectiveDate;


            double rate = (fromRate / toRate);
            return new Converted
            {
                result = rate,
                effectiveDate = effectiveDateStr
            };
        }
    }
}
