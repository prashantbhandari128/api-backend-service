# API-Backend-Service : 

Here is a starter template for an ``ASP.NET Core WebAPI`` project that demonstrates the use of ``Entity Framework Core``, integrating the ``Repository Pattern`` and ``Unit of Work Pattern`` for a **RESTful API**.

## Technologies:

- .NET **[8.0]**
- Entity Framework Core **[8.0.1]**
- SQLite

## Patterns: 

- Repository Pattern
- Unit of Work
- Dispose Pattern

## Architecture:

```
[Root]    
  │     
  ├── BackendService/
  │   ├── Controllers/
  │   ├── Database/
  │   ├── Migrations/
  │   ├── Extensions/
  │   ├── Helper/
  │   │   ├── Implementation/
  │   │   ├── Interface/
  │   │   ├── Result/
  │   ├── Middlewares/
  │   ├── Models/
  │       ├── Request/
  │       ├── Response/
  │           ├── Templates/
  │               ├── Operation/
  │               ├── Process/
  │               ├── Query/
  │
  ├── BackendService.DataAccess/
      ├── Context/
      ├── Entities/
      ├── Migrations/
      ├── Repository/
      │   ├── Implementation/
      │   ├── Interface/
      ├── UnitOfWork/
          ├── Implementation/
          ├── Interface/
```

The given directory structure represents a **modular**, **layered** architecture for an ``ASP.NET Core`` application. This architecture is organized to separate concerns and improve **maintainability**, **scalability**, and **testability**. Here’s a breakdown of the main components:

- **BackendService**: Main project containing the **Core Business Logic** and **API** controllers.

- **BackendService.DataAccess**: Separate project dedicated to **Data Access Logic**. It handles database interactions through ``Repository`` & ``Unit of Work``.
