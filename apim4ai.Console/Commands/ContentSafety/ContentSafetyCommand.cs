using apim4ai.Console.Utilities;
using Microsoft.Extensions.AI;
using System.CommandLine;

namespace apim4ai.Console.Commands.ContentSafety
{
    internal class ContentSafetyCommand : Command
    {
        public ContentSafetyCommand() : base("content-safety", "Call an API with content safety policy")
        {
            var chatClientBinder = this.CreateChatClientBinder();

            this.SetHandler(CommandHandler, chatClientBinder);
        }

        private async Task CommandHandler(IChatClient chatClient)
        {
            ConsoleUtility.WriteLine("Content Safety Command", ConsoleColor.Cyan);
            ConsoleUtility.WriteLine("This command demonstrates the content safety policy.", ConsoleColor.Cyan);
            ConsoleUtility.WriteLine();

            var chatManagement = new ChatManagement(chatClient);

            await chatManagement.RunChatAsync(
                null,
                onResponse: response =>
                {
                    ConsoleUtility.WriteLine(response.Text, ConsoleColor.Yellow);
                },
                onException: ex =>
                {
                    ConsoleUtility.WriteLine();
                    ConsoleUtility.WriteLine(ex.Message, ConsoleColor.Red);
                });
        }
    }
}
