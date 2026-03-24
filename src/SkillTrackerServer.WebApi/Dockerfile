# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SkillTrackerServer.WebApi/SkillTrackerServer.WebApi.csproj", "src/SkillTrackerServer.WebApi/"]
COPY ["src/SkillTrackerServer.Infrastructure/SkillTrackerServer.Infrastructure.csproj", "src/SkillTrackerServer.Infrastructure/"]
COPY ["src/SkillTrackerServer.Application/SkillTrackerServer.Application.csproj", "src/SkillTrackerServer.Application/"]
COPY ["src/SkillTrackerServer.Domain/SkillTrackerServer.Domain.csproj", "src/SkillTrackerServer.Domain/"]
COPY ["src/SkillTrackerServer.SharedKernel/SkillTrackerServer.SharedKernel.csproj", "src/SkillTrackerServer.SharedKernel/"]
RUN dotnet restore "./src/SkillTrackerServer.WebApi/SkillTrackerServer.WebApi.csproj"
COPY . .
WORKDIR "/src/src/SkillTrackerServer.WebApi"
RUN dotnet build "./SkillTrackerServer.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SkillTrackerServer.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillTrackerServer.WebApi.dll"]