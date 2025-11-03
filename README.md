# ?? Discord PilotBot

A feature-rich Discord bot built with **DSharpPlus** and **.NET 9.0**

## ?? Project Structure

```
PilotBot/
??? Commands/  # Bot command modules
?   ??? BasicCommands.cs    # Basic commands (ping, hello, info)
??? config/       # Configuration management
?   ??? BotConfig.cs        # Bot configuration loader
??? core/        # Core bot functionality
?   ??? Bot.cs # Main bot class
??? Events/        # Event handlers
?   ??? ReadyEvent.cs       # Bot ready event
?   ??? VoiceStateEvent.cs  # Voice channel join/leave events
?   ??? MessageEvent.cs   # Message creation events
??? Models/        # Data models
?   ??? BotSettings.cs  # Bot settings model (placeholder)
??? Services/    # Service classes
?   ??? LoggingService.cs   # Logging service (placeholder)
??? Program.cs      # Application entry point
??? appsettings.json        # Bot configuration file
??? PilotBot.csproj         # Project file
```

## ? Project Health

**All issues have been fixed:**
- ? Added missing `DSharpPlus.CommandsNext` NuGet package
- ? Fixed `appsettings.json` copy to output directory
- ? Corrected namespace inconsistencies
- ? Fixed type errors in `BotConfig.cs`
- ? Created sample command module (`BasicCommands.cs`)
- ? Removed incorrect using statements
- ? Build successful with no errors

## ?? Setup Instructions

### 1. Prerequisites
- .NET 9.0 SDK installed
- Discord Bot Token from [Discord Developer Portal](https://discord.com/developers/applications)

### 2. Configure Bot Token

Edit `appsettings.json`:
```json
{
  "BotToken": "YOUR_DISCORD_BOT_TOKEN_HERE",
  "Prefix": "!"
}
```

### 3. Install Dependencies
```bash
dotnet restore
```

### 4. Build and Run
```bash
dotnet build
dotnet run
```

## ?? Features

### Commands
- `!ping` - Check bot responsiveness and latency
- `!hello` - Get a greeting from the bot
- `!info` - Display bot information

### Events
- **Ready Event**: Logs when bot comes online
- **Voice State Event**: Welcomes users joining voice channels
- **Message Event**: Tracks bot mentions

## ?? Adding New Commands

1. Create a new class in the `Commands/` folder:

```csharp
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class MyCommands : BaseCommandModule
    {
[Command("mycommand")]
        [Description("Description of my command")]
        public async Task MyCommand(CommandContext ctx)
        {
 await ctx.RespondAsync("Response!");
      }
    }
}
```

2. Register it in `core/Bot.cs`:

```csharp
private void RegisterCommands()
{
    Commands.RegisterCommands<BasicCommands>();
    Commands.RegisterCommands<MyCommands>(); // Add this
}
```

## ?? Dependencies

- **DSharpPlus** (v4.5.1) - Discord API wrapper
- **DSharpPlus.CommandsNext** (v4.5.1) - Command framework

## ?? Security Note

?? **Never commit your `appsettings.json` with your actual bot token to Git!**

Add to `.gitignore`:
```
appsettings.json
```

## ??? Development Tips

1. **Folder Structure**: ? Your structure is excellent for a Discord bot
2. **Separation of Concerns**: Events, Commands, and Core logic are properly separated
3. **Configuration Management**: Robust config loading with multiple fallback paths
4. **Ready for Expansion**: Easy to add new commands, events, and services

## ?? Next Steps

- Implement `LoggingService.cs` for better logging
- Add more commands in the `Commands/` folder
- Implement `BotSettings.cs` for additional configuration
- Add error handling and logging
- Create a `.gitignore` to protect sensitive data

## ?? License

Add your license information here.

## ?? Contributing

Contributions welcome! Please open an issue or pull request.