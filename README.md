[![.NET](https://github.com/kikutano/CleanArchitecture.Template/actions/workflows/dotnet.yml/badge.svg)](https://github.com/kikutano/CleanArchitecture.Template/actions/workflows/dotnet.yml)

# CleanArchitecture.Template
**⚠️ This ReadMe is incomplete! ⚠️**

This is an open source project with the purpose of implement a simple, but complete, Backend Project template using [**Clean Architecture**](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) and [**Domain Driven Design**](https://khalilstemmler.com/articles/domain-driven-design-intro/) principles.
My goal is to improve this source code everytime I learn something new about **DDD** and **Clean Architecture** into my everyday job.
So, please, feel free to submit your idea, tips, criticisms, improvements etc etc.

# What this template do
This is a simple implementation of a Task Manager. You can create Projects, insert tasks inside a Project, update the status of single task, retrive all tasks and project. Etc. etc.

# Architecture
This template organize the code and layers using the [**Clean Architecture**](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) principles and  [**Domain Driven Design**](https://khalilstemmler.com/articles/domain-driven-design-intro/). Above you can find the explanations for every architecture layer.

## Domain
The domain layer ```CleanArchitecture.Domain``` is the **core** of the template and contains all the **Entities**, **Aggregates**, **Value Objects** and the definitions for the **Domain Errors**. The Entities are modeled using the **Rich Domain Model** instead of an [**Anemic Domain Model**](https://martinfowler.com/bliki/AnemicDomainModel.html). So every Entities and Aggregates are able to guarantee the consistency of they own properties.

#### Design Choice about the Domain Layer:
I prefer to use the [**Always Valid Model**](https://enterprisecraftsmanship.com/posts/always-valid-domain-model/) approach instead of [**Not Always Valid Model**](https://enterprisecraftsmanship.com/posts/always-valid-vs-not-always-valid-domain-model/). Another think that I do is to **not separate** the Entities Models from the ORM Models. I know this is not the purest approach, but this allow me to write lesser code and the result seems the same (for now!). Please read more about [**Having the domain model separated from the persistence model**](https://enterprisecraftsmanship.com/posts/having-the-domain-model-separate-from-the-persistence-model/) or [**Three approaches to Domain-Driven Design with Entity Framework Core**](https://www.thereformedprogrammer.net/three-approaches-to-domain-driven-design-with-entity-framework-core).

## Application
The application layer ```CleanArchitecture.Application``` contains all the business logics for this project. We define here the **Interfaces** that will be implemented into the ```CleanArchitecture.Infrastructure``` Layer. This is important because we can define the [**contracts**](https://medium.com/javarevisited/oop-good-practices-coding-to-the-interface-baea84fd60d3#:~:text=Simple%3A%20%E2%80%9CCoding%20to%20interfaces%2C,actual%20class%20with%20the%20implementation.) that are used to implement all the use cases inside the project. In addition, this layer define the **Commands** and the **Responses** that are executed by [**MediatR**](https://github.com/jbogard/MediatR) (an implementation of [**Mediator Pattern**](https://refactoring.guru/design-patterns/mediator)).

This Layer use the [**CQRS Pattern**](https://martinfowler.com/bliki/CQRS.html) using [**MediatR**](https://github.com/jbogard/MediatR) to execute the complex use cases. Furthermore a [**Repository Pattern**](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design) is defined to execute the queries. **Only the Aggregates has a Repository that execute queries**, that because we want allow the access only using the Aggregates. Read the **Resources** section to learn more about the  Aggregates.

#### Design Choice about the Application Layer:
I'm using the Application Layer to Validate the Inputs. [**Here to read more**](https://verraes.net/2015/02/form-command-model-validation/)

## Infrastructure
The ```CleanArchitecture.Infrastructure``` Layer is the place where all the **interfaces** are implemented. In this case we write the implementation for the **Repositories interfaces**, we write the **Database Configurations** using [**Entity Framework**](https://github.com/dotnet/efcore). As I said into Domain chapter I don't separate the Domain Entitites from the Entities on the Database. Please see the [```ProjectConfiguration.cs```](https://github.com/kikutano/CleanArchitecture.Template/blob/main/CleanArchitecture.Infrastructure/Persistence/EntityTypeConfigurations/ProjectConfiguration.cs) to understand how to setup the Entity Framework with Value Objects and Entities.

Another important think is that I'm using an **In Memory Database** just to allow you to compile and use the Api without do any migrations or create any database. Feel free to check the [**Database Configuration**](https://github.com/kikutano/CleanArchitecture.Template/blob/main/CleanArchitecture.Infrastructure/DependencyInjection.cs) and change what you prefer.

## Contracts
The ```CleanArchitecture.Contracts``` define the **response DTOs** and the **requests** to send to the Api. 

## WebApi
The ```CleanArchitecture.WebApi``` is the Layer where all the Api are defined. The Api returns DTOs results mapped using [**Mapster**](https://github.com/MapsterMapper/Mapster). The Api that change something inside the database use the [**CQRS Pattern**](https://martinfowler.com/bliki/CQRS.html) and [**MediatR**](https://github.com/jbogard/MediatR) plugin. For simple read queries I prefer to call directly the **Repository Interfaces**. All Api are documented using [**Swagger**](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-6.0).

## External Dependencies
- [**ErrorOr**](https://github.com/amantinband/error-or)
- [**MediatR**](https://github.com/jbogard/MediatR)
- [**Swagger**](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-6.0)
- [**Mapster**](https://github.com/MapsterMapper/Mapster)

# Automated Tests
I love to write test, I don't to want to test anything using my eyes because I'm lazy. I just want to automate everything I can and lets the computer say to me if something is wrong. I prefer an approach where I write the Functional Test that test the entire use case. I know this is not the purest approach, but if I need to write an Unit Test I do, but in the other case I prefer to write End-To-End Tests. 

## Common.Tests
This layer define all the utils to create Server Snapshot to allow you to test your project in a very easy way. I written an [**ProjectsSnapshotExtension**](https://github.com/kikutano/CleanArchitecture.Template/blob/main/CleanArchitecture.Common.Tests/DatabaseSnapshot/ProjectsSnapshotExtension.cs) that allow you to create Projects inside the database.

So you can create a snapshot in this way:

```csharp
Project project;
MockServerFactory
     .DbSnapshoter
     .AddProject( out project, "new project 1" )
     .AddProject( out project, "new project 2" )
     .AddProject( out project, "new project 3" );
```

[**Here a complete Example**](https://github.com/kikutano/CleanArchitecture.Template/blob/main/CleanArchitecture.Functional.Tests/Projects/GetAll.cs).

## Functional.Tests
For this purpose I written a [**MockServerFactory**](https://github.com/kikutano/CleanArchitecture.Template/blob/main/CleanArchitecture.Functional.Tests/Common/MockServerFactory.cs) that allow you to create an entire Mocked Server to execute the Api Calls directly inside your Test project. 

# 📖 References:
This is a list of online documents that I read to study DDD and Clean Architecture over the time. They are not necessary in C# language. 
##### Introductions:
- [**An Introduction to Domain-Driven Design**](https://khalilstemmler.com/articles/domain-driven-design-intro)

#### Entities:
- [**Understanding Domain Entities**](https://khalilstemmler.com/articles/typescript-domain-driven-design/entities/)
- [**Having the domain model separated from the persistence model**](https://enterprisecraftsmanship.com/posts/having-the-domain-model-separate-from-the-persistence-model/)

#### Value Objects:
- [**Nulls in Value Objects**](https://enterprisecraftsmanship.com/posts/nulls-in-value-objects/)

#### Aggregates:
- [**How to Design & Persist Aggregates**](https://khalilstemmler.com/articles/typescript-domain-driven-design/aggregate-design-persistence/)
- [**What Are Aggregates In Domain-Driven Design?**](https://www.jamesmichaelhickey.com/domain-driven-design-aggregates/)