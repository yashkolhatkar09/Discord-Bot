# ?? Quick Deploy Reference

## ?? What You Need

1. **Discord Bot Token** - Get from [Discord Developer Portal](https://discord.com/developers/applications)
2. **GitHub Account** - Push your code
3. **Render.com Account** - Free tier is fine

---

## ? Fast Track Deployment (5 Minutes)

### Step 1: Push to GitHub
```bash
git add .
git commit -m "Add Docker support"
git push origin main
```

### Step 2: Deploy on Render.com
1. Go to [render.com](https://render.com)
2. Click **"New +"** ? **"Blueprint"**
3. Connect your repo: `yashkolhatkar09/Discord-Bot`
4. Click **"Apply"**

### Step 3: Add Bot Token
1. Go to your service on Render
2. Click **"Environment"** tab
3. Add variable:
   - Key: `DISCORD_TOKEN`
   - Value: `your_bot_token_here`
4. Click **"Save Changes"**

### Step 4: Wait & Verify
- Watch the "Logs" tab
- Wait for: `?? Bot started successfully!`
- Check Discord - bot should be online! ?

---

## ?? Docker Commands (Local Testing)

```bash
# Test locally
docker-compose up --build

# Stop
docker-compose down

# View logs
docker-compose logs -f
```

---

## ?? Environment Variables

| Variable | Value | Required |
|----------|-------|----------|
| `DISCORD_TOKEN` | Your bot token | ? Yes |
| `PREFIX` | `!` | ? Optional |

---

## ?? Monitor Your Bot

- **Logs**: Render Dashboard ? Your Service ? Logs
- **Status**: Check if "Running" ??
- **Discord**: Bot shows as "Online"

---

## ?? Quick Troubleshooting

| Problem | Solution |
|---------|----------|
| Bot offline | Check `DISCORD_TOKEN` in Render |
| Commands don't work | Enable Message Content Intent |
| Build fails | Run `dotnet build` locally first |
| Can't find token | Check Discord Developer Portal |

---

## ?? Important Notes

?? **Free Tier Limitations:**
- Bot sleeps after 15 min inactivity
- Background workers stop after 90 days
- Upgrade to $7/month for 24/7 uptime

? **Already Configured:**
- Docker support
- Environment variables
- Auto-deploy on git push
- Error handling

---

## ?? That's It!

Your bot is now deployed and running on Render.com! ??

For detailed instructions, see [DEPLOYMENT.md](DEPLOYMENT.md)
