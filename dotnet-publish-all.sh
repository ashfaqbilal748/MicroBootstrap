#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
PUBLISH=./scripts/dotnet-publish.sh
PREFIX=Game
SERVICE=$PREFIX.Services
REPOSITORIES=($PREFIX.Api $SERVICE.Messaging $SERVICE.EventProcessor)

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Publishing a project: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     $PUBLISH
     cd ..
done