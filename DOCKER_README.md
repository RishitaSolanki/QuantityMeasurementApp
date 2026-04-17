# Docker Deployment Guide

This .NET 10 Web API has been containerized with Docker for easy deployment.

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running
- Docker Compose (included with Docker Desktop)

## Quick Start

### 1. Build and Run with Docker Compose

```bash
# Navigate to the project directory
cd QuantityMeasurementApp

# Build and start all services (API, SQL Server, Redis)
docker-compose up --build

# Or run in detached mode
docker-compose up --build -d
```

### 2. Access the Application

- **API**: http://localhost:8080
- **Swagger UI**: http://localhost:8080/swagger
- **SQL Server**: localhost:1433
- **Redis**: localhost:6379

### 3. Stop the Services

```bash
# Stop all containers
docker-compose down

# Stop and remove volumes (clears database data)
docker-compose down -v
```

## Services

| Service | Description | Port |
|---------|-------------|------|
| api | .NET 10 Web API | 8080, 8081 |
| sqlserver | SQL Server 2022 Developer | 1433 |
| redis | Redis 7 Alpine | 6379 |

## Environment Variables

The following environment variables are configured in `docker-compose.yml`:

| Variable | Description | Default |
|----------|-------------|---------|
| `ConnectionStrings__DefaultConnection` | SQL Server connection string | Server=sqlserver;Database=QuantityMeasurementDB;... |
| `Redis__ConnectionString` | Redis connection string | redis:6379 |
| `Jwt__Issuer` | JWT token issuer | QuantityMeasurementAPI |
| `Jwt__Audience` | JWT token audience | QuantityMeasurementClient |
| `Jwt__Key` | JWT secret key | (must be at least 32 characters) |

## Building Docker Image Only

```bash
# Build the Docker image
docker build -t quantity-measurement-api .

# Run the container (requires external SQL Server and Redis)
docker run -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="YourConnectionString" \
  -e Redis__ConnectionString="your-redis:6379" \
  quantity-measurement-api
```

## Production Deployment

For production deployment, consider:

1. **Azure Container Apps**: Deploy the container to Azure's managed container service
2. **Azure App Service**: Use the Docker container deployment option
3. **AWS ECS/Fargate**: Run containers on AWS
4. **Kubernetes**: Use the Docker image in a K8s cluster

### Production Environment Variables

Update these in your production environment:

- `ASPNETCORE_ENVIRONMENT=Production`
- `ConnectionStrings__DefaultConnection` - Use Azure SQL or AWS RDS
- `Redis__ConnectionString` - Use Azure Cache for Redis or AWS ElastiCache
- `Jwt__Key` - Use a strong, randomly generated secret (min 32 chars)

### Health Check

The container includes a health check endpoint. Ensure your load balancer checks:
- `http://<host>:8080/health`

## Troubleshooting

### SQL Server Connection Issues

```bash
# Check SQL Server container logs
docker logs quantity-measurement-db

# Test connection from API container
docker exec -it quantity-measurement-api /bin/sh
# Then use sqlcmd or similar to test connectivity
```

### Redis Connection Issues

```bash
# Check Redis container logs
docker logs quantity-measurement-redis

# Test Redis from API container
docker exec -it quantity-measurement-api /bin/sh
# Then: redis-cli -h redis ping
```

### View API Logs

```bash
docker logs quantity-measurement-api

# Follow logs in real-time
docker logs -f quantity-measurement-api
```

## Security Notes

- The Dockerfile runs the application as a non-root user (`appuser`)
- Default passwords should be changed for production
- JWT secrets should be strong and rotated regularly
- HTTPS should be enabled in production (configure reverse proxy like Nginx)
