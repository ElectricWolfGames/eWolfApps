using RestSharp;
using System.Linq;

namespace eWolfCommon.APIRequests
{
    public static class ExchangeRates
    {
        public static float GetUSDtoGBP()
        {
            var client = new RestClient();

            string appId = "8ef35a6312fb4d6cb5156aa405f34a1c";
            var requestC = new RestRequest($"https://openexchangerates.org/api/latest.json?app_id={appId}");
            requestC.AddHeader("accept", "application/json");
            RestResponse reaponsecurr = client.Execute(requestC);

            string content = reaponsecurr.Content;
            string[] parts = content.Split(',');

            string gbp = parts.First(x => x.Contains(@"GBP"": "));
            string[] keyValue = gbp.Split(':');

            return float.Parse(keyValue[1]);
        }
    }
}