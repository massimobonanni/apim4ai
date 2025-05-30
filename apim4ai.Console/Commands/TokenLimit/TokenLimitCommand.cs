﻿using apim4ai.Console.Utilities;
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
