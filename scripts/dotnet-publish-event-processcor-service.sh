#!/bin/bash
cd ../samples/Game-Microservices-Sample/Game.Services.EventProcessor/src/Game.Services.EventProcessor.API
dotnet publish  -c Release -o ./bin/Docker --source https://www.myget.org/F/micro-bootstrap/api/v3/index.json