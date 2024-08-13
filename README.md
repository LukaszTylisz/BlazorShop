# BlazorShop

## Architecture

- Domain-driven design (DDD)
- Event Sourcing
- Clean Architecture
- CQRS
  
<br/>

![image](https://github.com/user-attachments/assets/2e563079-27ba-44e0-b7da-211df5b9d84f)

## Project Structure

![image](https://github.com/user-attachments/assets/7356eec9-04f2-4948-9956-8055dcaee5e7)


## Backend - Swagger

![image](https://github.com/user-attachments/assets/08f1292a-9dd2-4fc3-985b-75ac2ba0284c)

## MongoDb

![image](https://github.com/user-attachments/assets/28fbcb0c-950d-4402-98f7-a14f2fac8b57)
![image](https://github.com/user-attachments/assets/97959fca-24f1-42cb-842b-a4c12e0a8aec)


## Frontend

Created Order with changing status
![image](https://github.com/user-attachments/assets/ea7cb1be-f7d2-4958-85c3-44ffeb390492)
![image](https://github.com/user-attachments/assets/d7527996-0609-4a6c-9aa7-113ebffedb5a)
![image](https://github.com/user-attachments/assets/813e77ba-ef26-433b-8d7d-7db5227ee884)
![image](https://github.com/user-attachments/assets/7f40fbe6-5927-4f74-b909-d5dd8f9c2a74)
![image](https://github.com/user-attachments/assets/44f4e353-84c0-4598-9e26-5d25445e3b0b)

Order Canceled

![image](https://github.com/user-attachments/assets/82a8e0fe-4e21-4b57-97d4-f84ac14a341b)


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
2. Create a cloud database on https://www.mongodb.com/ (it is free) and fill in appsettings.js in BlazorShop.Server: <br/>
```MongoDbConnectionString``` <br/>
```MongoDbDatabaseName``` <br/>
3. Launch the app!
