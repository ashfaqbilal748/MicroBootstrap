#!/bin/bash
dotnet publish ./src/Game.Services.EventProcessor.API -c Release --source https://www.myget.org/F/micro-bootstrap/api/v3/index.json  -o ./bin/docker