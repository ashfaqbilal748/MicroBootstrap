#!/bin/bash
 MYGET_ENV=""
 case "$TRAVIS_BRANCH" in
   "develop")
     MYGET_ENV="-dev"
     ;;
 esac

dotnet publish samples/Game-Microservices-Sample/Game.Services.Messaging/src/Game.Services.Messaging.API -c Release -o ./bin/docker  --no-cache --source https://www.myget.org/F/micro-bootstrap$MYGET_ENV/api/v3/index.json