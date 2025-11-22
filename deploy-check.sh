#!/bin/bash

# Quick deployment checklist script

echo "?? PilotBot Deployment Checklist"
echo "================================"
echo ""

# Check if Docker is installed
if command -v docker &> /dev/null; then
    echo "? Docker is installed"
else
    echo "? Docker is NOT installed - Install Docker Desktop first"
fi

# Check if .env exists
if [ -f .env ]; then
    echo "? .env file exists"
else
    echo "??  .env file not found - Copy .env.example to .env and add your token"
fi

# Check if Dockerfile exists
if [ -f Dockerfile ]; then
    echo "? Dockerfile exists"
else
    echo "? Dockerfile not found"
fi

# Check if render.yaml exists
if [ -f render.yaml ]; then
    echo "? render.yaml exists"
else
    echo "? render.yaml not found"
fi

echo ""
echo "?? Next Steps:"
echo "1. Create .env file with your Discord token"
echo "2. Test locally: docker-compose up --build"
echo "3. Push to GitHub: git push origin main"
echo "4. Deploy on Render.com"
echo ""
echo "?? See DEPLOYMENT.md for full instructions"
