FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY ./src/Game.Services.Messaging/bin/docker .
ENV ASPNETCORE_URLS http://*:5003
ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 5000
ENTRYPOINT dotnet Game.Services.Messaging.API.dll