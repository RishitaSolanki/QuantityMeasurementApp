# Use the official .NET 8.0 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["QuantityMeasurementApp.WebApi/QuantityMeasurementWebApi.csproj", "QuantityMeasurementApp.WebApi/"]
COPY ["QuantityMeasurementApp.Model/QuantityMeasurementModelLayer.csproj", "QuantityMeasurementApp.Model/"]
COPY ["QuantityMeasurementApp.BusinessLayer/QuantityMeasurementBusinessLayer.csproj", "QuantityMeasurementApp.BusinessLayer/"]
COPY ["QuantityMeasurementApp.RepositoryLayer/QuantityMeasurementRepositoryLayer.csproj", "QuantityMeasurementApp.RepositoryLayer/"]

RUN dotnet restore "QuantityMeasurementApp.WebApi/QuantityMeasurementWebApi.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/QuantityMeasurementApp.WebApi"
RUN dotnet build "QuantityMeasurementWebApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "QuantityMeasurementWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables for production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Run the application
ENTRYPOINT ["dotnet", "QuantityMeasurementWebApi.dll"]
