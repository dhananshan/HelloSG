using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSG.Common.Constant;
using HelloSG.Dialog;
using HelloSG.Dto;
using HelloSGService.HTTP;
using HelloSGService.Service.AI;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.DependencyInjection;

namespace HelloSG.Bot
{
    public class HelloSGComponent : IBot
    {
        private readonly DialogSet _dialogs;
        private readonly IAIService _aiService;
        private readonly IHttpService _httpService;


        public HelloSGComponent(IServiceProvider serviceProvider)
        {
            _httpService = serviceProvider.GetService<IHttpService>();
            _aiService = serviceProvider.GetService<IAIService>();


            _dialogs = new DialogSet();
            _dialogs.Add("weatherDialog", new WeatherDialog(serviceProvider).CreateWeatherWaterfall());


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
                    var dialogArgs = new Dictionary<string, object>();


                    switch (luisRes?.topScoringIntent.intent.ToLower()) {

                        case LUISIntents.Weather:
                            dialogArgs.Add("LuisResult", luisRes);
                            await dialogCtx.Begin("weatherDialog", dialogArgs);
                            break;

                        case LUISIntents.None:
                            await dialogCtx.Context.SendActivity("Sorry, I didn't understand. Can you re-type with more clarity?");
                            await dialogCtx.End();
                            break;

                        case LUISIntents.Greeting:
                            await dialogCtx.Context.SendActivity("Hi, How can i help you ?");
                            await dialogCtx.End();
                            break;

                    }
                }
            }

        }
    }    
}
