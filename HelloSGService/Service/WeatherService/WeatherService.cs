using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HelloSG.Common.Config;
using HelloSGService.HTTP;

namespace HelloSG.Service.Service.ExternalService
{
    public class WeatherService : IWeatherService
    {
        private string _apiKey;
        IHttpService _httpService;

        public WeatherService(IHttpService httpService)
        {
            _httpService = httpService;
            this._apiKey = AppSettings<BotSetting>.Config.DatagovsgKey;

            this._httpService.ServiceURL = AppSettings<BotSetting>.Config.WeatherServiceURL;
        }

        public async Task<T> GetContent<T>(string input)
        {
            var header = new List<Tuple<string, string>>();
            var tuple = new Tuple<string, string>("api-key", this._apiKey);
            header.Add(tuple);

            return await this._httpService.GetAsync<T>(input, header);
        }


    }
}
