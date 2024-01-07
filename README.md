# Superhero API

This repository contains a simple .NET 6 API with CRUD operations.

## Purpose

To keep track of what I have done and something to refer back to.

## Build and Run

**NOTE:** This project requires [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) or higher to run.

Open your terminal at the root of the repository and run:

`dotnet run --project ./Superhero.Api/Superhero.Api.csproj`

Once you see this in your terminal:

![Terminal Success](https://i.imgur.com/AcyfWpG.png)

You can now access the api here: https://localhost:7073/swagger/index.html

![Swagger UI](https://imgur.com/kkjv5Sq.png)

**Using Docker**

TODO:

## Tech Used

### Security

* ASP.NET Core Identity
* JWT Authentication

### Database
* Entity Framework Core

### Validation
* FluentValidation
* Entity Framework Core Fluent API

### Testing
* xUnit
* FluentAssertion
* FakeItEasy

### API Documentation
* Swagger

### Deployment
* Docker
* Github Actions -- TODO


## Initial Data

Once you run the project, you will notice in the terminal that it also runs the migrations as well as our initial data.

![Terminal EF Core Migrations](https://imgur.com/FkMkwX9.png)


You can now test the application using the default user:

| Username  | Password |
| ------------- | ------------- |
| admin  | Admin123*  |


Remember to authenticate before accessing other endpoint.