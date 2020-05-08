#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
BUILD=./scripts/dotnet-build-local.sh
PREFIX=Game
SERVICE=$PREFIX.Services
REPOSITORIES=($PREFIX.Api $SERVICE.Messaging $SERVICE.EventProcessor )

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Building a project: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     $BUILD
     cd ..
done