using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using HelloSGBotService.Service;
using HelloSGBotService.Model.ExternalService;

namespace HelloSG.Dialogs
{

    [Serializable]
    public class WeatherDialog : IDialog<object>
    {
        private IExternalService _exService;

        public async Task StartAsync(IDialogContext context)
        {

            await context.PostAsync("Please wait contacting Mr.Weather...");

            string inputParam = string.Empty;
            _exService = new WeatherService(false);

            inputParam = $"date_time={DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss")}";
            inputParam = System.Web.HttpUtility.HtmlEncode(inputParam);

            var weatherRes = await _exService.GetContent<WeatherResponse>(inputParam);

            await context.PostAsync($"Todays 24 hours weather forecast is: **{weatherRes?.items[0]?.general?.forecast}**, Please wait we are retrieving town forecast info... ");

            _exService = new WeatherService(true);
            var weatherTown = await _exService.GetContent<WeatherResponse>(inputParam);

            ShowOptions(context, weatherTown);
        }

        private void ShowOptions(IDialogContext context, WeatherResponse response)
        {

            
            PromptDialog.Choice(context, this.OnOptionSelected, response.items[0].forecasts.ToList().Select(x => x.area).ToList(), "Weather of city?", "Not a valid city", 3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case "Bishan":
                    await context.PostAsync("Bishan Weather...");
                    context.Done(1);
                    break;


                    case "Ang Mo Kio":
                    await context.PostAsync("Ang Mo Kio Weather...");
                    context.Done(1);
                    break;
                }
  

            }


        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var ticketNumber = new Random().Next(0, 20000);

            await context.PostAsync($"registered. Once we resolve it; we will get back to you.");

            context.Done(ticketNumber);
        }

    }
}