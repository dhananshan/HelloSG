using HelloSGBotService.HTTPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGBotService.LUISService
{
    public class LUISService : ILUISService
    {
        private IHTTPService _httpService;

        public LUISService(IHTTPService httpService) {
            this._httpService = httpService;
        }

        public string TestGet()
        {
            return this._httpService.GetContent();
        }
    }
}
