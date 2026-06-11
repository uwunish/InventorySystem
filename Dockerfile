# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY ["InventorySystem.API/InventorySystem.API.csproj", "InventorySystem.API/"]
COPY ["InventorySystem.Application/InventorySystem.Application.csproj", "InventorySystem.Application/"]
COPY ["InventorySystem.Domain/InventorySystem.Domain.csproj", "InventorySystem.Domain/"]
COPY ["InventorySystem.Infrastructure/InventorySystem.Infrastructure.csproj", "InventorySystem.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "./InventorySystem.API/InventorySystem.API.csproj"

# Copy source
COPY . .

WORKDIR /src/InventorySystem.API

# Publish
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "InventorySystem.API.dll"]