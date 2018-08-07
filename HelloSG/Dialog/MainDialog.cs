using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts.Choices;

namespace HelloSG.Dialog
{
    public class MainDialog : DialogContainer
    {
        public MainDialog() : base(Id)
        {
            Dialogs.Add(DialogId, new WaterfallStep[]
              {
                    async (dc, args, next) =>
                    {
                        await dc.Prompt("choicePrompt", $"[MainDialog] I'm banking 🤖{Environment.NewLine}Would you like to check balance or make payment?",
                            new ChoicePromptOptions
                            {
                                Choices = new[] {new Choice {Value = "Check balance"}, new Choice {Value = "Make payment"}}.ToList()
                            });
                    },

                    async (dc, args, next) =>
                    {
                        dc.ActiveDialog.State = new Dictionary<string, object>(); // clear the dialog state 
                        await dc.End();
                    }
      });

            Dialogs.Add("choicePrompt", new ChoicePrompt("en"));


        }



        public static string Id => "mainDialog";

        public static MainDialog Instance { get; } = new MainDialog();
    }
}
    

