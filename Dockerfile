# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore QuantityMeasurementApp/QuantityMeasurementWebAPI/QuantityMeasurementWebAPI.csproj
RUN dotnet publish QuantityMeasurementApp/QuantityMeasurementWebAPI/QuantityMeasurementWebAPI.csproj -c Release -o /app/publish

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 8080

ENTRYPOINT ["dotnet", "QuantityMeasurementWebAPI.dll"]