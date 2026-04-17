# Quantity Measurement API - Docker & Render Deployment Guide

## Overview
This guide explains how to deploy the Quantity Measurement API using Docker and Render cloud platform.

## Prerequisites
- Docker Desktop installed
- Git repository with the project code
- Render account (https://render.com)
- GitHub account connected to Render

## Local Docker Deployment

### 1. Build and Run with Docker Compose
```bash
# Build and start all services
docker-compose up --build

# Run in background
docker-compose up -d --build

# View logs
docker-compose logs -f

# Stop services
docker-compose down
```

### 2. Services Available
- **Web API**: http://localhost:5000
- **Swagger UI**: http://localhost:5000
- **Health Check**: http://localhost:5000/health
- **SQL Server**: localhost:1434
- **Redis**: localhost:6379

### 3. Environment Variables
The application uses the following environment variables:
- `ConnectionStrings__DefaultConnection`: SQL Server connection string
- `ConnectionStrings__Redis`: Redis connection string
- `JwtSettings__Key`: JWT signing key
- `EncryptionSettings__Key`: Encryption key

## Render Deployment

### 1. Push to GitHub
```bash
git add .
git commit -m "Add Docker and Render deployment configuration"
git push origin main
```

### 2. Connect Render to GitHub
1. Sign in to Render dashboard
2. Click "New +" -> "Web Service"
3. Connect your GitHub repository
4. Select the repository
5. Render will automatically detect the `render.yaml` file

### 3. Automatic Deployment
Render will automatically:
- Build the Docker containers
- Deploy SQL Server, Redis, and Web API services
- Set up environment variables
- Configure health checks
- Assign URLs to each service

### 4. Service URLs
After deployment, you'll get URLs like:
- **Web API**: https://quantitymeasurement-api.onrender.com
- **SQL Server**: Internal service (not publicly accessible)
- **Redis**: Internal service (not publicly accessible)

## Configuration Files

### Docker Compose (`docker-compose.yml`)
- Defines all services (SQL Server, Redis, Web API)
- Sets up networking and volumes
- Configures health checks
- Manages service dependencies

### Render Configuration (`render.yaml`)
- Defines Render services
- Sets up environment variables
- Configures Docker builds
- Manages service connections

### Dockerfiles
- `Dockerfile`: Web API multi-stage build
- `Dockerfile.sqlserver`: SQL Server container
- `Dockerfile.redis`: Redis container with authentication

## Environment-Specific Settings

### Development (`appsettings.json`)
- Local database connections
- Development logging levels
- Short cache expiration

### Production (`appsettings.Production.json`)
- Environment variable placeholders
- Production logging levels
- Longer cache expiration
- Enhanced security settings

## Troubleshooting

### Local Issues
```bash
# Check container status
docker-compose ps

# View specific service logs
docker-compose logs webapi
docker-compose logs sqlserver
docker-compose logs redis

# Rebuild specific service
docker-compose up --build webapi
```

### Render Issues
1. Check Render dashboard for service status
2. Review deployment logs
3. Verify environment variables
4. Check health check endpoints

### Database Issues
- Ensure SQL Server container is healthy before starting Web API
- Check connection strings in environment variables
- Verify database migrations are applied

### Redis Issues
- Ensure Redis container is healthy
- Check Redis connection string
- Verify Redis password configuration

## Security Considerations

1. **Secrets Management**: Use Render's environment variables for sensitive data
2. **Database Security**: SQL Server uses generated passwords in production
3. **Redis Authentication**: Redis uses password authentication
4. **JWT Keys**: Use randomly generated keys for production
5. **HTTPS**: Render automatically provides SSL certificates

## Monitoring

### Health Checks
- Web API: `/health` endpoint
- SQL Server: Built-in health check
- Redis: Ping-based health check

### Logging
- Application logs available in Render dashboard
- Docker logs available locally with `docker-compose logs`

## Scaling

### Horizontal Scaling
- Web API can be scaled horizontally in Render
- Database and Redis services should be scaled vertically

### Performance Optimization
- Redis caching reduces database load
- Health checks ensure service reliability
- Environment-specific configurations optimize performance

## Cost Optimization

### Render Free Tier
- Web Service: Free tier available
- Private Services: Free tier for SQL Server and Redis
- Custom Domains: Available on paid plans

### Resource Management
- Monitor resource usage in Render dashboard
- Adjust service plans based on actual usage
- Use environment variables to optimize configurations
