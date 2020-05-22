#!/bin/bash
export ASPNETCORE_ENVIRONMENT=docker
BUILD=./scripts/build.sh
PREFIX=Game
SERVICE=$PREFIX.Services

REPOSITORIES=($PREFIX.APIGateway $SERVICE.Messaging $SERVICE.EventProcessor)

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Building a project: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     $BUILD
     cd ..
done