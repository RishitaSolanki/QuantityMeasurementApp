# Render Deployment Guide

This .NET 10 Web API is configured for deployment on Render using Docker containers.

## Prerequisites

- [Render account](https://render.com/)
- Git repository with your code (GitHub, GitLab, or Bitbucket)
- Docker support in your repository

## Quick Deployment Steps

### 1. Push to Git Repository

Make sure your code is pushed to a Git repository:

```bash
git add .
git commit -m "Add Render Docker deployment configuration"
git push origin main
```

### 2. Create Render Account

1. Go to [render.com](https://render.com/)
2. Sign up with your Git provider (GitHub/GitLab/Bitbucket)
3. Verify your email

### 3. Deploy to Render

#### Option A: Automatic Deployment (Recommended)

1. Click **"New +"** in Render dashboard
2. Select **"Web Service"**
3. Connect your Git repository
4. Render will automatically detect the `render.yaml` file
5. Review the configuration and click **"Create Web Service"**

#### Option B: Manual Configuration

1. Click **"New +"** → **"Web Service"**
2. Connect your Git repository
3. Configure manually:
   - **Name**: `quantity-measurement-api`
   - **Environment**: `Docker`
   - **Docker Context**: `.`
   - **Dockerfile Path**: `./Dockerfile`
   - **Port**: `8080`
   - **Health Check Path**: `/api/health`

### 4. Database Setup

The `render.yaml` includes a PostgreSQL database that will be automatically created. Render will inject the connection string as an environment variable.

### 5. Redis Configuration (Optional)

Render doesn't provide Redis by default. Options:

1. **Redis Cloud Add-on**: Add Redis from Render marketplace
2. **External Redis**: Use Redis Cloud, Upstash, or AWS ElastiCache
3. **Disable Redis**: Set `Redis__ConnectionString` to empty string

## Configuration Files

### render.yaml
- Defines web service and PostgreSQL database
- Configures environment variables
- Sets health check path
- Auto-deploys on git push

### Dockerfile
- Multi-stage build optimized for production
- Runs as non-root user for security
- Exposes port 8080 for Render

### HealthController.cs
- Provides `/api/health` endpoint for Render health checks
- Returns service status and timestamp

## Environment Variables

Render automatically sets these from `render.yaml`:

| Variable | Description | Source |
|----------|-------------|--------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection | Render Database |
| `Jwt__Key` | JWT secret key | Auto-generated |
| `ASPNETCORE_ENVIRONMENT` | Environment | Set to Production |
| `ASPNETCORE_URLS` | .NET URLs | Set to port 8080 |

## Accessing Your Deployed API

Once deployed:

- **API URL**: `https://your-app-name.onrender.com`
- **Swagger**: `https://your-app-name.onrender.com/swagger`
- **Health Check**: `https://your-app-name.onrender.com/api/health`

## Database Migration

Your API will need database migrations. Options:

1. **Automatic Migrations**: Add migration code to startup
2. **Manual Migrations**: Use Render shell to run migrations:
   ```bash
   dotnet ef database update
   ```

## Monitoring and Logs

- **Logs**: View in Render dashboard under your service
- **Metrics**: Available in Render dashboard
- **Health Checks**: Automated via `/api/health` endpoint

## Troubleshooting

### Build Failures
- Check Dockerfile syntax
- Verify all project files are committed
- Review build logs in Render dashboard

### Runtime Errors
- Check environment variables in Render dashboard
- Review application logs
- Verify database connection

### Database Issues
- Ensure PostgreSQL package is installed (added to .csproj)
- Check connection string format
- Run migrations if needed

### Health Check Failures
- Verify `/api/health` endpoint exists
- Check that port 8080 is exposed
- Review health check logs

## Scaling

Render offers automatic scaling:

1. Go to your service settings
2. Enable **"Auto-scaling"**
3. Set minimum/maximum instances
4. Configure CPU/memory thresholds

## Custom Domain

1. Go to service settings
2. Click **"Custom Domains"**
3. Add your domain name
4. Update DNS records as instructed

## Production Considerations

- **HTTPS**: Automatically provided by Render
- **Environment Variables**: Use Render's secret management
- **Backups**: Enable automatic PostgreSQL backups
- **Monitoring**: Set up alerts for errors and response time

## Cost

- **Free Tier**: Available for hobby projects
- **Starter Plan**: ~$7/month for production use
- **Standard Plan**: ~$25/month for higher performance

## Support

- [Render Documentation](https://render.com/docs)
- [Render Support](https://render.com/support)
- Check community forums for common issues
