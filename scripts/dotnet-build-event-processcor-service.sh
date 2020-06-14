#!/bin/bash
 MYGET_ENV=""
 case "$TRAVIS_BRANCH" in
   "develop")
     MYGET_ENV="-dev"
     ;;
 esac
cd ../samples/Game-Microservices-Sample/Game.Services.EventProcessor/src/Game.Services.EventProcessor.API
dotnet build -c Release --no-cache --source https://www.myget.org/F/micro-bootstrap/api/v3/index.json