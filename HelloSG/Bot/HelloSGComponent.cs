using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSG.Common.Constant;
using HelloSG.Dialog;
using HelloSG.Dto;
using HelloSGService.Service.AI;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace HelloSG.Bot
{
    public class HelloSGComponent : IBot
    {
        private readonly DialogSet _dialogs;
        private readonly IAIService _aiService;
        

        public HelloSGComponent(IAIService aiService)
        {
            _aiService = aiService;
            _dialogs = new DialogSet();

            _dialogs.Add("mainDialog", MainDialog.Instance);
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
                var dialogCtx = _dialogs.CreateContext(context, state);


                await dialogCtx.Continue();
               
                if (!context.Responded)
                {
                    LUISResponse luisRes = await this._aiService.GetIntent<LUISResponse>(context.Activity.Text);

                    switch (luisRes?.topScoringIntent.intent.ToLower()) {

                        case LUISIntents.Weather:
                            //TODO
                            break;
                        case LUISIntents.None:
                            //TODO
                            break;
                        case LUISIntents.Greeting:
                            //TODO
                            break;
                    }

                    await dialogCtx.Begin("mainDialog");
                }
            }

        }
    }    
}
