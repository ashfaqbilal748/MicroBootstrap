#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Game.Services.Messaging.API
dotnet watch run
