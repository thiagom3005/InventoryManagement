# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["InventoryManagement.sln", "./"]
COPY ["src/InventoryManagement.API/InventoryManagement.API.csproj", "src/InventoryManagement.API/"]
COPY ["src/InventoryManagement.Application/InventoryManagement.Application.csproj", "src/InventoryManagement.Application/"]
COPY ["src/InventoryManagement.Domain/InventoryManagement.Domain.csproj", "src/InventoryManagement.Domain/"]
COPY ["src/InventoryManagement.Infrastructure/InventoryManagement.Infrastructure.csproj", "src/InventoryManagement.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "InventoryManagement.sln"

# Copy all source files
COPY . .

# Build and publish
WORKDIR "/src/src/InventoryManagement.API"
RUN dotnet publish "InventoryManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Create non-root user
RUN addgroup --gid 1000 appuser && \
    adduser --uid 1000 --gid 1000 --disabled-password --gecos "" appuser

# Copy published app
COPY --from=build /app/publish .

# Change ownership
RUN chown -R appuser:appuser /app

# Switch to non-root user
USER appuser

EXPOSE 8080

ENTRYPOINT ["dotnet", "InventoryManagement.API.dll"]
