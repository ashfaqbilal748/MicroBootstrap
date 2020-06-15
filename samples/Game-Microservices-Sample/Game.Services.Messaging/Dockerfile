FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/Game.Services.Messaging.API -c release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS http://*:7002
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet Game.Services.Messaging.API.dll