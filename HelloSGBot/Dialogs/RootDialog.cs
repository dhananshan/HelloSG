using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using HelloSGBotService.Service;
using HelloSGBotService.Model.LUIS;
using HelloSGBotService.Model.ExternalService;
using HelloSG.Dialogs;
using System.Threading;

namespace NDBot.Dialogs
{

    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string _intent { get; set; }

        private IAIService _aiService;

        public RootDialog()
        {
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

            LUISResponse luisRes = await this._aiService.GetIntent<LUISResponse>(message.Text);

            switch (luisRes.topScoringIntent.intent.ToLower())
            {

                case LUISIntents.Weather:
                    context.Call(new WeatherDialog(), this.ResumeAfterDialog);
                    break;
                case LUISIntents.None:
                    await NoneIntent(context);
                    break;
                case LUISIntents.Greeting:
                    await GreetingIntent(context);
                    break;
            }

            //context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterDialog(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync($"Thanks for contacting our support team. Your ticket number is.");
            context.Wait(this.MessageReceivedAsync);
        }


        private async Task GreetingIntent(IDialogContext context)
        {
            await context.PostAsync("Hi, How can I help you?");
        }

        private async Task NoneIntent(IDialogContext context)
        {
            await context.PostAsync("Sorry, I didn't understand. Can you re-type with more clarity?");
        }




    }
}