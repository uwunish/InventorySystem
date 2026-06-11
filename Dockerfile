# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY InventorySystem.slnx .
COPY InventorySystem.Domain/InventorySystem.Domain.csproj \
     InventorySystem.Domain/
COPY InventorySystem.Application/InventorySystem.Application.csproj \
     InventorySystem.Application/
COPY InventorySystem.Infrastructure/InventorySystem.Infrastructure.csproj \
     InventorySystem.Infrastructure/
COPY InventorySystem.API/InventorySystem.API.csproj \
     InventorySystem.API/

RUN dotnet restore

COPY . .

WORKDIR /src/InventorySystem.API
RUN dotnet publish -c Release -o /app/publish

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "InventorySystem.API.dll"]