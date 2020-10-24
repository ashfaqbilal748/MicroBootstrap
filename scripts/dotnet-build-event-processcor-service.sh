#!/bin/bash
 MYGET_ENV=""
 case "$TRAVIS_BRANCH" in
   "develop")
     MYGET_ENV="dev"
     ;;
 esac
cd ./samples/Game-Microservices-Sample/Game.Services.EventProcessor/src/Game.Services.EventProcessor.API
dotnet build -p:VersionPrefix=1.0.0 -p:VersionSuffix=MYGET_ENV  -p:SourceRevisionId=$TRAVIS_JOB_ID -p:RepositoryType=git -p:RepositoryBranch=$TRAVIS_BRANCH -c Release --source https://api.nuget.org/v3/index.json --source https://www.myget.org/F/micro-bootstrap$MYGET_ENV/api/v3/index.json
