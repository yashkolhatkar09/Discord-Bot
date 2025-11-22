# ? Render.com Deployment Checklist

Use this checklist to ensure smooth deployment:

## ?? Pre-Deployment

- [ ] Discord bot created in [Discord Developer Portal](https://discord.com/developers/applications)
- [ ] Bot token copied and saved securely
- [ ] **Message Content Intent** enabled in Discord Developer Portal
- [ ] Bot invited to your test server with proper permissions
- [ ] Code tested locally and working

## ?? Docker Setup (Completed)

- [x] Dockerfile created
- [x] .dockerignore created
- [x] render.yaml created
- [x] docker-compose.yml created
- [x] .env.example created
- [x] BotConfig.cs updated to support environment variables

## ?? Local Testing (Optional but Recommended)

- [ ] Create `.env` file from `.env.example`
- [ ] Add your Discord token to `.env`
- [ ] Run `docker-compose up --build`
- [ ] Verify bot connects and responds to commands
- [ ] Test moderation features
- [ ] Stop container with `docker-compose down`

## ?? GitHub Setup

- [ ] All files committed (except appsettings.json and .env)
- [ ] Check `.gitignore` includes sensitive files
- [ ] Push to GitHub: `git push origin main`
- [ ] Verify all Docker files are visible on GitHub

## ?? Render.com Deployment

### Account Setup
- [ ] Create account at [Render.com](https://render.com)
- [ ] Connect your GitHub account
- [ ] Verify repository access

### Service Creation
- [ ] Click "New +" ? "Background Worker" (or use Blueprint with render.yaml)
- [ ] Select your GitHub repository: `yashkolhatkar09/Discord-Bot`
- [ ] Configure service:
  - [ ] Name: `pilotbot-discord`
  - [ ] Environment: `Docker`
  - [ ] Region: Choose closest
  - [ ] Branch: `main`
  - [ ] Dockerfile Path: `./Dockerfile`

### Environment Variables
- [ ] Go to "Environment" tab
- [ ] Add `DISCORD_TOKEN` with your bot token
- [ ] Add `PREFIX` with `!` (optional)
- [ ] Click "Save Changes"

### Deploy
- [ ] Click "Manual Deploy" or wait for auto-deploy
- [ ] Watch "Logs" tab for deployment progress
- [ ] Wait for: "?? Bot started successfully!"
- [ ] Verify bot is online in Discord

## ? Post-Deployment Verification

- [ ] Bot shows as "Online" in Discord
- [ ] Test `!ping` command
- [ ] Test `!hello` command
- [ ] Test `!info` command
- [ ] Test word moderation (send banned word)
- [ ] Check Render.com logs for any errors

## ?? Auto-Deploy Setup

- [ ] Verify "Auto-Deploy" is enabled in Render settings
- [ ] Test by making a small code change
- [ ] Push to GitHub: `git push origin main`
- [ ] Watch Render automatically rebuild and deploy

## ?? If Something Goes Wrong

### Bot won't connect?
- [ ] Check DISCORD_TOKEN in Render environment variables
- [ ] Verify token is valid in Discord Developer Portal
- [ ] Check logs for specific error messages

### Commands don't work?
- [ ] Verify Message Content Intent is enabled
- [ ] Check bot permissions in Discord server
- [ ] Test with correct prefix (default: `!`)

### Build fails?
- [ ] Check Dockerfile is in root directory
- [ ] Verify code compiles locally: `dotnet build`
- [ ] Check Render logs for specific build errors

### Bot goes offline?
- [ ] Free tier: Bot sleeps after 15 min inactivity (expected)
- [ ] Check Render service status
- [ ] Restart service manually if needed

## ?? Cost Considerations

### Free Tier (Current)
- ? 750 hours/month
- ?? Sleeps after 15 minutes inactivity
- ?? Background workers stop after 90 days
- ?? Good for testing and personal use

### Paid Tier ($7/month)
- ? Always running (24/7)
- ? No sleep
- ? No time limits
- ?? Recommended for production bots

## ?? Success!

If all items are checked, your bot is successfully deployed! ??

### What's Next?
- Add more commands in `Commands/` folder
- Enhance moderation features
- Add database integration
- Create custom embeds
- Add music features

---

**Need help?** See [DEPLOYMENT.md](DEPLOYMENT.md) for detailed instructions.
