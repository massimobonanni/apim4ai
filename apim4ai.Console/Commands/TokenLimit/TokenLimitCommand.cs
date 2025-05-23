using apim4ai.Console.Binders;
using apim4ai.Console.Utilities;
using Microsoft.Extensions.AI;
using System.CommandLine;

namespace apim4ai.Console.Commands.TokenLimit
{
    internal class TokenLimitCommand : Command
    {
        public TokenLimitCommand() : base("token-limit", "Call an API with token limit policy")
        {
            var chatClientBinder = this.CreateChatClientBinder();

            this.SetHandler(CommandHandler, chatClientBinder);
        }

        private async Task CommandHandler(IChatClient chatClient)
        {
            ConsoleUtility.WriteLine("Token Limit Command", ConsoleColor.Cyan);
            ConsoleUtility.WriteLine("This command demonstrates the token limit policy.", ConsoleColor.Cyan);
            ConsoleUtility.WriteLine();
            while (true)
            {
                ConsoleUtility.Write("You: ", ConsoleColor.Green);
                System.Console.ForegroundColor = ConsoleColor.Green;
                var input = System.Console.ReadLine();
                System.Console.ResetColor();

                // Exit if input is empty
                if (string.IsNullOrWhiteSpace(input))
                    break;

                // Stream response from LLM
                ConsoleUtility.Write("LLM: ", ConsoleColor.Yellow);
                await foreach (var token in chatClient.GetStreamingResponseAsync(input))
                {
                    ConsoleUtility.Write(token.Text, ConsoleColor.Yellow);
                }
                ConsoleUtility.WriteLine(string.Empty, ConsoleColor.Yellow);
            }
        }
    }
}
