# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

RUN mkdir -p /source
WORKDIR /source

# Copy the main source project files
COPY ./backend/src/Megarender.AppServices/Megarender.IdentityService .
RUN dotnet restore

RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build /app ./

RUN mkdir ~/secrets
COPY ./secrets ~/secrets

EXPOSE 80

ENTRYPOINT ["dotnet", "Megarender.IdentityService.dll"]