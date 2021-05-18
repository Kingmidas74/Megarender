# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

RUN mkdir -p /source
WORKDIR /source

# Copy the main source project files
COPY . .
WORKDIR /source/backend
RUN dotnet restore

WORKDIR /source/backend/src/Megarender.AppServices/Megarender.WorkerServices/Megarender.NotificationWorkerService
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine
WORKDIR /app
COPY --from=build /app ./

RUN mkdir ~/secrets
COPY ./secrets ~/secrets

ENTRYPOINT ["dotnet", "Megarender.NotificationWorkerService.dll"]