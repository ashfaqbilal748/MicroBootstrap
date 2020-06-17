#!/bin/bash
#!/bin/bash
 MYGET_ENV=""
 case "$TRAVIS_BRANCH" in
   "develop")
     MYGET_ENV="-dev"
     ;;
 esac

dotnet publish ./src/Game.Services.Messaging.API -c Release -o ./src/Game.Services.Messaging.API/bin/Docker  --source https://api.nuget.org/v3/index.json --source https://www.myget.org/F/micro-bootstrap$MYGET_ENV/api/v3/index.json