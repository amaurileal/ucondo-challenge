# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ucondo-challenge.api/ucondo-challenge.api.csproj", "src/ucondo-challenge.api/"]
COPY ["src/ucondo-challenge.application/ucondo-challenge.application.csproj", "src/ucondo-challenge.application/"]
COPY ["src/ucondo-challenge.business/ucondo-challenge.business.csproj", "src/ucondo-challenge.business/"]
COPY ["src/ucondo-challenge.infrastructure/ucondo-challenge.infrastructure.csproj", "src/ucondo-challenge.infrastructure/"]
RUN dotnet restore "./src/ucondo-challenge.api/ucondo-challenge.api.csproj"
COPY . .
WORKDIR "/src/src/ucondo-challenge.api"
RUN dotnet build "./ucondo-challenge.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ucondo-challenge.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ucondo-challenge.api.dll"]