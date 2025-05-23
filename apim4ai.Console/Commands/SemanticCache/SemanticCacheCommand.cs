using apim4ai.Console.Utilities;
using Microsoft.Extensions.AI;
using System.CommandLine;
using System.Diagnostics;

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

            var transactionStopWatch = new Stopwatch();

            var chatManagement = new ChatManagement(chatClient);

            await chatManagement.RunChatAsync(
                beforeRequest: input =>
                {
                    transactionStopWatch.Restart();
                },
                onResponse: response =>
                {
                    transactionStopWatch.Stop();
                    ConsoleUtility.WriteLine(response.Text, ConsoleColor.Yellow);
                    ConsoleUtility.WriteLine($"\t\tUsage: InputTokenCount={response.Usage?.InputTokenCount}; OutputTokenCount={response.Usage?.OutputTokenCount}", ConsoleColor.Magenta);
                    ConsoleUtility.WriteLine($"\t\tTransaction Time: {transactionStopWatch.ElapsedMilliseconds} ms", ConsoleColor.Magenta);
                    ConsoleUtility.WriteLine();
                },
                onException: ex =>
                {
                    ConsoleUtility.WriteLine();
                    ConsoleUtility.WriteLine(ex.Message, ConsoleColor.Red);
                }
                );
        }
    }
}
