# BlazorShop

## Architecture

- Domain-driven design (DDD)
- Event Sourcing
- Clean Architecture
- CQRS

<br/>

![image](https://github.com/user-attachments/assets/2e563079-27ba-44e0-b7da-211df5b9d84f)

## Technologies
- .NET 8.0
- C# 12
- Blazor WebAssembly App
- ASP.NET Core Web API
- MongoDB
- Event Store
- AutoMapper
- NLog
- MediatR
- FluentValidation

## Main layers

| Layer | Description |
| ------ | ------ |
| BlazorShop.Client | Blazor application |
| BlazorShop.Server | API |
| BlazorShop.Application | Communication with Domain Layer |
| BlazorShop.Infrastructure | Persistence |
| BlazorShop.Domain | Core business logic |


## How to run the application
1. Download and run Event Store.
2. Create a cloud database on https://www.mongodb.com/ (it is free) and fill in appsettings.js in Shop.Server: <br/>
```MongoDbConnectionString``` <br/>
```MongoDbDatabaseName``` <br/>
3. Launch the app!
