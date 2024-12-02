[![.NET](https://github.com/aimenux/ServiceBusEmulatorDemo/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/aimenux/ServiceBusEmulatorDemo/actions/workflows/ci.yml)

# ServiceBusEmulatorDemo
```
Playing with service bus emulator
```

> In this repo, i m using [service bus emulator](https://hub.docker.com/r/microsoft/azure-messaging-servicebus-emulator) in order to consume and publish messages.
>
> :one: `Example01` : use worker template with queues
>
> :two: `Example02` : use worker template with topics/subscriptions
>
> To run the demo, type the following commands :
> - `docker compose -f .\config\docker-compose.yaml up -d`
> - `dotnet run --project .\src\Example01` 
> - `dotnet run --project .\src\Example02` 
>

**`Tools`** : net 8.0, servicebus-emulator, docker
