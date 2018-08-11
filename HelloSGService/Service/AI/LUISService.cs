using HelloSG.Common.Config;
using HelloSGService.HTTP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGService.Service.AI
{
    public class LUISService : IAIService
    {
        private IHttpService _httpService;

        public LUISService() {
            _httpService = new HttpService();
            this._httpService.ServiceURL = AppSettings<BotSetting>.Config.LUISServiceURL;
        }


        public async Task<T> GetIntent<T>(string input)
        {
            return await this._httpService.GetAsync<T>(input);
        }
    }
}
