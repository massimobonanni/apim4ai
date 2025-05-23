using apim4ai.Console.Binders;
using apim4ai.Console.Utilities;
using Microsoft.Extensions.AI;
using System.CommandLine;

namespace apim4ai.Console.Commands.SemanticCache
{
    internal class SemanticCacheCommand : Command
    {
        public SemanticCacheCommand() : base("semantic-cache", "Call an API with semantic cache policy")
        {
            var chatClientBinder = this.CreateChatClientBinder();

            this.SetHandler(CommandHandler, chatClientBinder);
        }

        private async Task CommandHandler(IChatClient chatClient)
        {
            ConsoleUtility.WriteLine("Semantic Cache Command", ConsoleColor.Cyan);
            ConsoleUtility.WriteLine("This command demonstrates the semantic cache policy.", ConsoleColor.Cyan);
            ConsoleUtility.WriteLine();

            var chatManagement = new ChatManagement(chatClient);

            await chatManagement.RunChatAsync(
                onResponse: token => ConsoleUtility.Write(token.Text, ConsoleColor.Yellow),
                onException: ex => ConsoleUtility.WriteLine(ex.Message, ConsoleColor.Red));
        }
    }
}
