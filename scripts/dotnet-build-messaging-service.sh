#!/bin/bash
 MYGET_ENV=""
 case "$TRAVIS_BRANCH" in
   "develop")
     MYGET_ENV="-dev"
     ;;
 esac
cd ../samples/Game-Microservices-Sample/Game.Services.Messaging/src/Game.Services.Messaging.API
dotnet build -c Release --no-cache --source https://www.myget.org/F/micro-bootstrap/api/v3/index.json