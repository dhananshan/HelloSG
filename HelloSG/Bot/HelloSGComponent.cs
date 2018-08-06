using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSG.Dialog;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace HelloSG.Bot
{
    public class HelloSGComponent : IBot
    {


        private readonly DialogSet dialogs;

        public HelloSGComponent()
        {
            // compose dialogs
            dialogs = new DialogSet();
            dialogs.Add("mainDialog", MainDialog.Instance);
        }


        /// <summary>
        /// Every Conversation turn for our EchoBot will call this method. In here
        /// the bot checks the Activty type to verify it's a message, bumps the 
        /// turn conversation 'Turn' count, and then echoes the users typing
        /// back to them. 
        /// </summary>
        /// <param name="context">Turn scoped context containing all the data needed
        /// for processing this conversation turn. </param>        
        public async Task OnTurn(ITurnContext context)
        {

            if (context.Activity.Type == ActivityTypes.Message)
            {
                var state = context.GetConversationState<Dictionary<string, object>>();
                var dialogCtx = dialogs.CreateContext(context, state);

                await dialogCtx.Continue();
                if (!context.Responded)
                {
                    await dialogCtx.Begin("mainDialog");
                }
            }

        }
    }    
}
