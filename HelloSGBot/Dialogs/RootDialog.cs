using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using HelloSGBotService.Service;
using HelloSGBotService.Model.LUIS;

namespace NDBot.Dialogs
{

    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string _intent { get; set; }

        private ILUISService _luisService ;

        public RootDialog() {
            this._luisService = new LUISService();
        }
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;

            LUISResponse luisRes = await this._luisService.GetIntent(message.Text);

            switch (luisRes.topScoringIntent.intent) {

                case LUISIntents.Weather:
                    await context.PostAsync("Weahther is ...");
                    break;
            }

            context.Wait(MessageReceivedAsync);
        }

    }
}