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

**How to start with Kubernetes?**
----------------
Open `samples\Game-Microservices-Sample\deployments\k8s` directory, in this directory there are two folder [infrastructure](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/deployments/k8s/infrastructure) and [micro-services](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/deployments/k8s/micro-services). in `infrastructure` folder exits all needed infrastructure for executing our microservices that we use `kubectl apply` for running them. for example for running `mongodb` on our cluster we sholud use these commands:

```
kubectl apply -f mongo-persistentvolumeclaim.yaml
kubectl apply -f mongo-deployment.yaml
kubectl apply -f mongo-service.yaml
```
In `micro-services` folder there are our services. for running our services on our cluster we should `kubectl apply` command for example:

```
kubectl apply -f messaging-service-deployment.yaml
kubectl apply -f messaging-service-service.yaml
```

**What HTTP requests can be sent to the API?**
----------------

You can find the list of all HTTP requests in [Game.APIGateway.rest](https://github.com/mehdihadeli/MicroBootstrap/blob/master/samples/Game-Microservices-Sample/Game.APIGateway/Game.APIGateway.rest) file placed in the root folder of [Game.APIGateway](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/Game.APIGateway). 
This file is compatible with [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) plugin for [Visual Studio Code](https://code.visualstudio.com). 
