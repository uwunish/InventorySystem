# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-nanoserver-ltsc2022 AS build
WORKDIR /src

# Copy solution and project files
COPY InventorySystem.sln .
COPY ["InventorySystem.API/InventorySystem.API.csproj", "InventorySystem.API/"]
COPY ["InventorySystem.Application/InventorySystem.Application.csproj", "InventorySystem.Application/"]
COPY ["InventorySystem.Domain/InventorySystem.Domain.csproj", "InventorySystem.Domain/"]
COPY ["InventorySystem.Infrastructure/InventorySystem.Infrastructure.csproj", "InventorySystem.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "./InventorySystem.API/InventorySystem.API.csproj"

# Copy all source code
COPY . .

# Build and publish
WORKDIR /src/InventorySystem.API
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0-nanoserver-ltsc2022 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Render uses PORT environment variable
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "InventorySystem.API.dll"]
