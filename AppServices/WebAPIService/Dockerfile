# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

RUN mkdir -p /source
WORKDIR /source

# Copy the main source project files
COPY . .
RUN dotnet restore

WORKDIR /source/AppServices/WebAPIService
RUN dotnet publish -c debug -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5002

ENTRYPOINT ["dotnet", "WebAPIService.dll"]