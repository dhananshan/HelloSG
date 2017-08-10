using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using HelloSGBotService.Service;
using HelloSGBotService.Model.LUIS;
using HelloSGBotService.Model.ExternalService;

namespace NDBot.Dialogs
{

    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string _intent { get; set; }

        private IAIService _aiService ;
        private IExternalService _exService;

        public RootDialog() {
            this._aiService = new LUISService();
        }
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            string inputParam = string.Empty;


            LUISResponse luisRes = await this._aiService.GetIntent<LUISResponse>(message.Text);

            switch (luisRes.topScoringIntent.intent) {

                case LUISIntents.Weather:
                    _exService = new WeatherService();

                    inputParam = $"date_time={DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss")}";
                    inputParam = System.Web.HttpUtility.HtmlEncode(inputParam);

                    var weatherRes = await _exService.GetContent<WeatherResponse>(inputParam);

                    await context.PostAsync($"Todays weather is {weatherRes?.items[0]?.general?.forecast}");
                    break;
            }

            context.Wait(MessageReceivedAsync);
        }

    }
}