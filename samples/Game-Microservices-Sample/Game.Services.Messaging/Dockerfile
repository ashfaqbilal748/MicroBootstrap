FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./src/Game.Services.Messaging.API/bin/Docker .
EXPOSE 7002
ENV ASPNETCORE_URLS http://*:7002
ENV ASPNETCORE_ENVIRONMENT docker  
ENTRYPOINT dotnet Game.Services.Messaging.API.dll