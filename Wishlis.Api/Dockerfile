# syntax=docker/dockerfile:1.4
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only project files first
COPY ["Wishlis.Api/Wishlis.Api.csproj", "Wishlis.Api/"]
COPY ["Wishlis.Infrastructure/Wishlis.Infrastructure.csproj", "Wishlis.Infrastructure/"]
COPY ["Wishlis.Domain/Wishlis.Domain.csproj", "Wishlis.Domain/"]
COPY ["Wishlis.Application/Wishlis.Application.csproj", "Wishlis.Application/"]

# Restore packages with cache mount
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore "Wishlis.Api/Wishlis.Api.csproj"

# Copy everything else
COPY . .
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/Wishlis.Api"

# Build with cache mount
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet build "Wishlis.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet publish "Wishlis.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy appsettings files
COPY ["Wishlis.Api/appsettings.json", "Wishlis.Api/appsettings.Production.json", "./"]
ENTRYPOINT ["dotnet", "Wishlis.Api.dll"]