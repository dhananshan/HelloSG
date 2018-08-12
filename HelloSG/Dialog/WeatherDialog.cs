using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloSG.Dto;
using HelloSG.Service.Service.ExternalService;
using HelloSGService.HTTP;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts.Choices;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSG.Dialog
{
    public class WeatherDialog
    {
        private readonly IHttpService _httpService;
        private readonly IWeatherService _weatherService;
        public WeatherDialog(IServiceProvider serviceProvider) {

            _httpService = serviceProvider.GetService<IHttpService>();
            _weatherService = serviceProvider.GetService<IWeatherService>();
        }

        // The weather waterfall has two steps
        public WaterfallStep[] CreateWeatherWaterfall()
        {
            return new WaterfallStep[] {
                TodaysOverallWeather//,
                //SendWeatherReport
            };
        }

        private async Task TodaysOverallWeather(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {

            await dc.Context.SendActivity($"Please wait contacting Mr.Weather...");

            string inputParam = string.Empty;

            inputParam = $"date_time={DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss")}";
            inputParam = System.Web.HttpUtility.HtmlEncode(inputParam);

            var weatherRes = await _weatherService.GetContent<WeatherResponseDto>(inputParam);

            await dc.Context.SendActivity($"Todays 24 hours weather forecast is: **{weatherRes?.items[0]?.general?.forecast}**"); //, Please wait we are retrieving town forecast info... ");


            // TODO: request town info
            dc.ActiveDialog.State = new Dictionary<string, object>(); // clear the dialog state 
            await dc.End();
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
    

