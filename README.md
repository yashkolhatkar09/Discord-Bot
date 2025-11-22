# ?? PilotBot - Discord Bot

A feature-rich Discord bot built with **DSharpPlus** and **.NET 9.0**

[![Deploy to Render](https://render.com/images/deploy-to-render-button.svg)](https://render.com)

## ? Features

- ?? **Ping Command** - Check bot latency
- ?? **Greetings** - Welcome messages
- ?? **Bot Info** - Display bot statistics
- ?? **ASCII Art** - Convert text to ASCII art
- ??? **Word Moderation** - Auto-delete messages with banned words
- ?? **Voice Commands** - Kick users from voice channels
- ?? **Docker Ready** - Containerized for easy deployment

## ?? Quick Deploy to Render.com

### Prerequisites
- Discord Bot Token from [Discord Developer Portal](https://discord.com/developers/applications)
- GitHub account
- Render.com account (free tier available)

### Deploy in 3 Steps

1. **Fork/Clone this repository**
2. **Push to your GitHub**
3. **Deploy on Render.com** - See [DEPLOYMENT.md](DEPLOYMENT.md) for detailed instructions

## ?? Local Development

### Option 1: Run with .NET
```bash
dotnet restore
dotnet build
dotnet run
```

### Option 2: Run with Docker
```bash
# Copy environment template
cp .env.example .env

# Edit .env and add your bot token
# Then run:
docker-compose up --build
```

## ?? Project Structure

```
PilotBot/
??? Commands/           # Bot command modules
??? config/            # Configuration management
??? core/              # Core bot functionality
??? Events/            # Event handlers
??? Models/            # Data models
??? Services/          # Service classes
??? Dockerfile         # Docker configuration
??? render.yaml        # Render.com deployment config
??? appsettings.json   # Local config (not committed)
```

## ?? Configuration

### Local Development
Edit `appsettings.json`:
```json
{
  "BotToken": "YOUR_DISCORD_BOT_TOKEN",
  "Prefix": "!"
}
```

### Production (Render.com)
Set environment variables:
- `DISCORD_TOKEN` - Your bot token
- `PREFIX` - Command prefix (default: `!`)

## ?? Documentation

- [?? Deployment Guide](DEPLOYMENT.md) - Complete deployment instructions
- [?? Fixes Summary](FIXES_SUMMARY.md) - Issues fixed and improvements

## ??? Technologies

- .NET 9.0
- DSharpPlus 4.5.1
- Docker
- Render.com

## ?? Available Commands

| Command | Description |
|---------|-------------|
| `!ping` | Check bot response time |
| `!hello` | Get a greeting |
| `!info` | Display bot information |
| `!ascii <text>` | Convert text to ASCII art |
| `!kickall` | Kick everyone from your voice channel |

## ??? Auto-Moderation

The bot automatically filters messages containing:
- Banned words (configurable)
- Leetspeak variations (j0b, 1nt3rnsh1p, etc.)

## ?? Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## ?? License

This project is open source and available under the MIT License.

## ?? Links

- [Discord Developer Portal](https://discord.com/developers/applications)
- [DSharpPlus Documentation](https://dsharpplus.github.io/)
- [Render.com Documentation](https://render.com/docs)

---

**Made with ?? using .NET 9.0 and DSharpPlus**
