# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

RUN mkdir -p /source
WORKDIR /source

# Copy the main source project files
COPY ./src/backend/Megarender.AppServices/Megarender.WorkerServices/Megarender.NotificationWorkerService .
RUN dotnet restore

RUN dotnet publish -c debug -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Megarender.NotificationWorkerService.dll"]