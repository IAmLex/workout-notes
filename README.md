# Workout Notes

## Containers

### Services

``` BASH
docker build -t workout-service .
docker run -d -p 8080:80 --name workout-service workout-service
```

``` BASH
docker build -t UserService .
docker run -d -p 8081:80 --name workout-service UserService
```

### RabbitMQ

``` BASH
# rabbitmq:3-management
docker run --name rabbitmq -d -p 5672:5672 -p 5673:5673 -p 15672:15672 rabbitmq:3-management
```

### MySQL

``` BASH
# mysql:latest@user
docker run --name user -p 3306:3306 -p 33060:33060 -e MYSQL_ROOT_PASSWORD=Welkom32! -d mysql:latest
```

``` BASH
# mysql:latest@workout
docker run --name workout -p 3307:3306 -p 33061:33060 -e MYSQL_ROOT_PASSWORD=Welkom32! -d mysql:latest
```
## Research

### Worker service

[Tutorial](https://codeburst.io/get-started-with-rabbitmq-2-consume-messages-using-hosted-service-e7e6a20b15a6)
[docs.microsoft](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=visual-studio)

## Packages

### MySQL

[Connect to MySQL database.](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-example.html)

Packages:

- MySql.EntityFrameworkCore

### RabbitMQ

[RabbitMQ config](https://code-maze.com/aspnetcore-rabbitmq/)

Packages:

- RabbitMQ.Client
- Newtonsoft.Json

### Worker Service

[Worker service](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=netcore-cli)
[Tutorial](https://www.c-sharpcorner.com/article/how-to-call-background-service-from-net-core-web-api/)

- Microsoft.Extensions.Hosting

## TODO

- Add docker
- Add kubernetes
- Add unit tests
