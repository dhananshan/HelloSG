using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloSG.Service.Service.ExternalService;
using HelloSGService.HTTP;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts.Choices;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSG.Dialog
{
    public class WeatherDialog
    {
        IHttpService _httpService;

        public WeatherDialog(IServiceProvider serviceProvider) {

            _httpService = serviceProvider.GetService<IHttpService>();

        }

        // The weather waterfall has two steps
        public WaterfallStep[] CreateWeatherWaterfall()
        {
            return new WaterfallStep[] {
                AskWeatherLocation,
                SendWeatherReport
            };
        }

        private async Task AskWeatherLocation(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            //await _httpService.GetAsync<string>("");

            await dc.Context.SendActivity($"Ask Weather Location .... ");
        }


        private async Task SendWeatherReport(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            //await _httpService.GetAsync<string>("");
            await dc.Context.SendActivity($"SendWeatherReport .... ");
            dc.ActiveDialog.State = new Dictionary<string, object>(); // clear the dialog state 
            await dc.End();

        }
    }
}
    

