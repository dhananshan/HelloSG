using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using HelloSGBotService.LUISService;

namespace NDBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        //private ILUISService _luisService;

        //public RootDialog(ILUISService luisService) {
        //}

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;

            //var val = this._luisService.TestGet();
            if (message.Text == "reset")
            {

            }
          
        }



        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
               
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }


    }
}