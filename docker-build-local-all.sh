#!/bin/bash
BUILD=./scripts/docker-build-local.sh
PREFIX=Game
SERVICE=$PREFIX.Services
REPOSITORIES=($PREFIX.Api $SERVICE.Messaging $SERVICE.EventProcessor )

for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Building a local Docker image: $REPOSITORY
	 echo ========================================================
     cd $REPOSITORY
     $BUILD
     cd ..
done