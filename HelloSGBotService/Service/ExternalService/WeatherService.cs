using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HelloSGBotService.Model.LUIS;

namespace HelloSGBotService.Service
{
    [Serializable]
    public class WeatherService : IExternalService
    {
        private IHTTPService _httpService;
        private string _apiKey;
        public WeatherService() {
            this._httpService = new HTTPService();
            this._apiKey = ConfigurationManager.AppSettings["DatagovsgKey"].ToString();
            this._httpService.ServiceURL = ConfigurationManager.AppSettings["WeatherServiceURL"].ToString();
        }

        public async Task<T> GetContent<T>(string input)
        {
            var header = new List<Tuple<string, string>>();
            var tuple = new Tuple<string, string>("api-key", this._apiKey);
            header.Add(tuple);

            return await this._httpService.Get<T>(input, header);
        }


    }
}
