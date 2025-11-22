# ?? Docker + Render.com Deployment Guide

## ?? What's Been Set Up

Your Discord bot is now ready for Docker deployment on Render.com! Here's what was added:

- ? `Dockerfile` - Multi-stage Docker build
- ? `.dockerignore` - Exclude unnecessary files from Docker image
- ? `render.yaml` - Render.com configuration (Background Worker)
- ? `docker-compose.yml` - Local Docker testing
- ? `.env.example` - Environment variable template
- ? Updated `BotConfig.cs` - Supports environment variables

---

## ?? Test Locally with Docker (Optional)

### Prerequisites
- Docker Desktop installed
- Your Discord bot token

### Steps

1. **Create `.env` file** (copy from `.env.example`):
```bash
cp .env.example .env
```

2. **Edit `.env` and add your bot token**:
```env
DISCORD_TOKEN=YOUR_ACTUAL_BOT_TOKEN_HERE
PREFIX=!
```

3. **Build and run with Docker Compose**:
```bash
docker-compose up --build
```

4. **Stop the container**:
```bash
docker-compose down
```

### Alternative: Manual Docker Commands

```bash
# Build the image
docker build -t pilotbot .

# Run the container
docker run -e DISCORD_TOKEN="YOUR_TOKEN_HERE" pilotbot
```

---

## ?? Deploy to Render.com

### Step 1: Prepare Your GitHub Repository

1. **Commit all Docker files**:
```bash
git add Dockerfile .dockerignore render.yaml docker-compose.yml .env.example
git commit -m "Add Docker support for Render.com deployment"
git push origin main
```

?? **IMPORTANT**: Make sure `appsettings.json` and `.env` are NOT committed (they should be in `.gitignore`)

---

### Step 2: Create Render.com Account

1. Go to [Render.com](https://render.com)
2. Sign up or log in
3. Connect your GitHub account

---

### Step 3: Deploy as Background Worker

#### Option A: Using render.yaml (Recommended)

1. Click **"New +"** ? **"Blueprint"**
2. Connect your GitHub repository: `yashkolhatkar09/Discord-Bot`
3. Render will automatically detect `render.yaml`
4. Click **"Apply"**

#### Option B: Manual Setup

1. Click **"New +"** ? **"Background Worker"**
2. Connect your GitHub repository: `yashkolhatkar09/Discord-Bot`
3. Configure:
   - **Name**: `pilotbot-discord`
   - **Environment**: `Docker`
   - **Region**: Choose closest to you
   - **Branch**: `main`
   - **Dockerfile Path**: `./Dockerfile`

---

### Step 4: Add Environment Variables

1. In your Render service dashboard, go to **"Environment"** tab
2. Add environment variable:
   - **Key**: `DISCORD_TOKEN`
   - **Value**: Your actual Discord bot token
3. Add another (optional):
   - **Key**: `PREFIX`
   - **Value**: `!`
4. Click **"Save Changes"**

---

### Step 5: Deploy

1. Render will automatically build and deploy your bot
2. Watch the **"Logs"** tab to see your bot starting up
3. You should see: `?? Bot started successfully!`
4. Your bot will now be online 24/7! ??

---

## ?? Auto-Deploy Setup

Render.com will automatically redeploy when you push to `main` branch:

1. Make code changes locally
2. Commit and push:
```bash
git add .
git commit -m "Update bot features"
git push origin main
```
3. Render automatically rebuilds and redeploys! ?

---

## ?? Monitoring Your Bot

### Check Logs
- Go to your Render service dashboard
- Click **"Logs"** tab
- See real-time console output

### Check Status
- **Running** ?? - Bot is online
- **Building** ?? - Deploying new version
- **Failed** ?? - Check logs for errors

---

## ?? Troubleshooting

### Bot won't start?
1. Check logs for error messages
2. Verify `DISCORD_TOKEN` is set correctly in Render dashboard
3. Make sure your bot token is valid in Discord Developer Portal

### "Could not find file appsettings.json"?
? This is expected! The bot will use environment variables instead.

### Build fails?
1. Make sure `Dockerfile` is in the root directory
2. Check that your code compiles locally: `dotnet build`

### Bot connects but commands don't work?
1. Check Discord Developer Portal ? Bot ? Message Content Intent is enabled
2. Verify bot has correct permissions when invited to server

---

## ?? Render.com Pricing

- **Free Tier**: 
  - 750 hours/month (enough for 24/7 with one service)
  - Your bot will sleep after 15 minutes of inactivity
  - ?? Background workers on free tier will **stop after 90 days** unless you upgrade

- **Paid Tier** ($7/month):
  - Always running
  - No sleep
  - Better for production bots

---

## ?? Security Best Practices

? **DO:**
- Use environment variables for bot token
- Keep `appsettings.json` in `.gitignore`
- Never commit tokens to Git

? **DON'T:**
- Commit your bot token to GitHub
- Share your `.env` file
- Use production token for local testing

---

## ?? Helpful Commands

### View Docker logs locally:
```bash
docker-compose logs -f
```

### Rebuild after code changes:
```bash
docker-compose up --build
```

### Stop and remove containers:
```bash
docker-compose down
```

---

## ?? Next Steps

1. ? Test locally with Docker (optional)
2. ? Push to GitHub
3. ? Deploy to Render.com
4. ? Add bot token as environment variable
5. ? Monitor logs to confirm bot is online
6. ?? Enjoy your 24/7 Discord bot!

---

## ?? Need Help?

- [Render.com Docs](https://render.com/docs)
- [DSharpPlus Docs](https://dsharpplus.github.io/)
- [Docker Docs](https://docs.docker.com/)

---

**Your bot is now production-ready! ??**
