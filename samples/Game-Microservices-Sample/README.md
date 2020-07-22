**Introduce the sample**
----------------
In this sample we have a simple microservices project based on our [MicroBootstrap](https://github.com/mehdihadeli/MicroBootstrap/tree/master/src/MicroBootstrap) package which contains 3 micro-services: 
* [Game.APIGateway](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/Game.APIGateway)
* [Game.Services.EventProcessor](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/Game.Services.EventProcessor)
* [Game.Services.Messaging](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/Game.Services.Messaging)
 
 `Game.APIGateway` is our API Gateway of our microservices and it will manage and route our request to appropriate end-points in other microservices. in this API Gateway we use different technologies like [Consul](https://www.consul.io/), [Fabio](https://fabiolb.net/) for service discovery and load-balancing (we can turn them off in our appsettings when we use Kubernetes) and [RabbitMQ](https://www.rabbitmq.com/) for our message broker, [RESTEeas](https://github.com/canton7/RestEase) for Http calls, [Jaeger](https://www.jaegertracing.io/) for distributed tracing, [Seq](https://datalust.co/) and [Serilog](https://serilog.net/) for logging purpose, [Vault](https://www.vaultproject.io/) for security and key management, [Prometheus](https://prometheus.io/) for monitoring and [MongoDB](https://www.mongodb.com/) and [Redis](https://redis.io/) for our storage system. this service will be available in this address [http://localhost:7000](http://localhost:7000).
 
 `Game.Services.EventProcessor` is a service for processing commands that send on the message broker. for example in this sample in our `API Gateway` wen publish an `AddGameEventSource` command to the broker for creating a game event source. `Game Event Processor` service that subscribed to this command (AddGameEventSource) consumes this command from the message broker and will execute its command handler for this command and store this game event source in MongoDB. then this handler in the final step will publish a `GameEventSourceAdded` event to message broker. this service will be available in this address [http://localhost:7001](http://localhost:7001)
 
 `Game.Services.Messaging` is a service that uses SignalR and consumes `GameEventSourceAdded` event from the message broker and writes the event data that is our created game event source to the browser in this address [http://localhost:7002/signalr/index.html](http://localhost:7002/signalr/index.html).
 
 ![Game](https://github.com/mehdihadeli/MicroBootstrap/blob/master/samples/Game-Microservices-Sample/image.jpg?raw=true)
 
 **Scaling Microservices**
 ----------------
 For scaling microservice in this project we have 2 option:
 * Using Consul and Fabio: for scaling our microservices we can use of consul and fabio and use of a customize algorithm for load balancing
 * Using Kubernetes: use of kubernetes for scaling but kubernetes limted to round robin aprouch for load balancing
 
**Thecnologies**
----------------
* .Net Core 3.1
* RabbitMQ
* MongoDB
* Docker
* RESTEeas
* Consul
* Fabio
* Kubernetes
* Docker
* Redis
* Vault
* Jaeger
* OpenTracing
* Prometheus
* DDD
* Clean Architecture
* SignalR
* Seq
* Serilog


**How to start with Docker Compose?**
----------------

Open `samples\Game-Microservices-Sample\deployments\docker-compose` directory and execute bellow command:

```
docker-compose -f infrastructure.yml up -d
```
you can also execute other scripts in [docker-compose](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/deployments/docker-compose) folder like `mongo-rabbit-redis.yml` to run only theses infrastructure on your machine.

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
For setup your local environment for using kubernetes you can use different approuch but I personally perfer to use [K3s](https://k3s.io/) from rancher team, it is awsome like [rancher](https://rancher.com/) for kubernetes management :)        

Open `samples\Game-Microservices-Sample\deployments\k8s` directory, in this directory, there are two folders [infrastructure](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/deployments/k8s/infrastructure) and [micro-services](https://github.com/mehdihadeli/MicroBootstrap/tree/master/samples/Game-Microservices-Sample/deployments/k8s/micro-services). in `infrastructure` folder exits all needed infrastructure for executing our microservices that we use `kubectl apply` for running them. for example for running `mongodb` on our cluster we should use these commands:

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


**Future Works**
----------------
-  [ ] Integration with Service Mesh and Istio

**CI/CD**
----------------
 
 ![CI/CD](https://github.com/mehdihadeli/MicroBootstrap/blob/master/samples/Game-Microservices-Sample/ci-cd.png?raw=true)
