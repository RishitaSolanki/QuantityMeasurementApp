using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementApp.BusinessLayer.Interfaces;
using QuantityMeasurementWebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();//Add services to the container.


builder.Services.AddEndpointsApiExplorer();// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Quantity Measurement API", 
        Version = "v1",
        Description = "REST API for Quantity Measurement operations with caching and database persistence"
    });
    
    
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";// Include XML comments if available
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") // Configure Entity Framework with PostgreSQL
    ?? "Server=localhost,1434;Database=QuantityMeasurementDb;User Id=sa;Password=Glauniversity@123;TrustServerCertificate=true;MultipleActiveResultSets=true";

Console.WriteLine($"Connection string: {connectionString}");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is null or empty");
}

builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("QuantityMeasurementRepositoryLayer")));


// Configure Redis Cache
var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
try
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnectionString;
        options.InstanceName = "QuantityMeasurement_";
    });
    Console.WriteLine("Redis cache configured successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Warning: Failed to configure Redis cache: {ex.Message}. Application will continue without caching.");
    // Add a fallback distributed cache (in-memory) if Redis fails
    builder.Services.AddDistributedMemoryCache();
}

// Register repositories
builder.Services.AddScoped<QuantityMeasurementRepositoryLayer.Interfaces.IQuantityMeasurementRepository, QuantityMeasurementRepositoryLayer.Repositories.QuantityMeasurementRepository>();

// Register services
builder.Services.AddScoped<QuantityMeasurementApp.BusinessLayer.Services.IRedisCacheService, QuantityMeasurementApp.BusinessLayer.Services.RedisCacheService>();

// Register business layer services
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
builder.Services.AddScoped<QuantityMeasurementBusinessLayer.Services.IUserService, QuantityMeasurementBusinessLayer.Services.UserService>();
builder.Services.AddScoped<QuantityMeasurementBusinessLayer.Services.ISecurityService, QuantityMeasurementBusinessLayer.Services.SecurityService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add comprehensive health checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("API is running"));

var app = builder.Build();

// Apply database migrations on startup
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<QuantityMeasurementDbContext>();
        dbContext.Database.Migrate();
        Console.WriteLine("Database migrations applied successfully.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error applying database migrations: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity Measurement API v1");
        c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
    });
}

// Add global exception handling middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Add health check endpoint
app.MapHealthChecks("/health");

// Add a simple root endpoint
app.MapGet("/", () => new
{
    Message = "Quantity Measurement API",
    Version = "v1",
    Endpoints = new
    {
        Swagger = "/swagger",
        Health = "/health",
        Operations = "/api/v1/quantitymeasurement",
        Auth = "/api/v1/auth"
    }
});
app.Run();