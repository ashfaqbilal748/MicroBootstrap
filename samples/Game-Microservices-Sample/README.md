**How to start with Docker Compose?**
----------------

Open `samples\Game-Microservices-Sample\deployments\docker-compose` directory and execute:

```
docker-compose -f infrastructure.yml up -d
```

It will start the required infrastructure in the background. Then, you can start the services independently of each other via `dotnet run` or `./scripts/start.sh` command in each microservice or run them all at once using Docker that create and run needed docker images in compose file:

```
docker-compose -f services-local.yml up
```
or using pre-build docker images in docker hub with using this docker compose:

```
docker-compose -f services.yml up
```

**What HTTP requests can be sent to the API?**
----------------

You can find the list of all HTTP requests in [Game.APIGateway.rest](https://github.com/mehdihadeli/MicroBootstrap/blob/master/samples/Game-Microservices-Sample/Game.APIGateway/Game.APIGateway.rest) file placed in the root folder of [Game.APIGateway](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/Game.APIGateway). 
This file is compatible with [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) plugin for [Visual Studio Code](https://code.visualstudio.com). 
