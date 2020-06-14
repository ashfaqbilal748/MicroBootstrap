#!/bin/bash
cd ../samples/Game-Microservices-Sample/Game.Services.Messaging/src/Game.Services.Messaging.API
dotnet publish  -c Release -o ./bin/docker --source https://www.myget.org/F/micro-bootstrap/api/v3/index.json