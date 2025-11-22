using DiscordBot.Commands;
using DiscordBot.Config;
using DiscordBot.Events;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Core
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        private readonly BotConfig _config;

        public Bot(BotConfig config)
        {
            _config = config;

            Client = new DiscordClient(new DiscordConfiguration
            {
                Token = config.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.Guilds |
                         DiscordIntents.GuildVoiceStates |
                         DiscordIntents.GuildMessages |
                         DiscordIntents.MessageContents
            });

            // Setup Commands
            Commands = Client.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new[] { config.Prefix },
                EnableMentionPrefix = true,
                EnableDefaultHelp = true
            });

            RegisterCommands();
            RegisterEvents();
        }

        private void RegisterCommands()
        {
            Commands.RegisterCommands<BasicCommands>();
            // Add more command modules here as you create them
        }

        private void RegisterEvents()
        {
            Client.Ready += ReadyEvent.OnReady;
            Client.MessageCreated += MessageEvent.OnMessageCreated;
            Client.MessageUpdated += MessageEvent.OnMessageUpdated;
        }

        public async Task StartAsync()
        {
            await Client.ConnectAsync();
            Console.WriteLine("🚀 Bot started successfully!");
        }
    }
}