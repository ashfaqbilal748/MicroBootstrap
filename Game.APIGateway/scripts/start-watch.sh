#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Game.API
dotnet watch run
