# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ../PlanADevOpsChallenge1API/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ../PlanADevOpsChallenge1API/ ./
RUN dotnet publish -c Release -o out

VOLUME //./pipe/docker_engine://./pipe/docker_engine

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PlanADevOpsChallenge1API.dll"]