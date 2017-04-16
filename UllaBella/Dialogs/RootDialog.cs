using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using UllaBella.Storage;
namespace UllaBella.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string stored;
        private OfficeInfo ofin = new OfficeInfo();

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            if(activity.Text.Contains("@ullabella"))
            {
                string[] instr = activity.Text.Split(' ');

                if(instr.Length >= 3)
                {
                    if (instr[1].Equals("info"))
                    {
                        string reply = ofin.getInfo(instr[2]);
                        await context.PostAsync($"{reply}");
                    }

                    if(instr[1].Equals("set"))
                    {
                        bool done = ofin.addInfo(instr[2],instr[3]);
                        if (done)
                        {
                            await context.PostAsync($"{instr[3]} has been added to the office information");
                        }
                        else
                        {
                            await context.PostAsync($"This did not work.");
                        }
                    }
                }
            }

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }
    }
}