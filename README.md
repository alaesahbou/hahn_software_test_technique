# Task Management Application

A comprehensive demonstration of Clean Architecture, Domain-Driven Design (DDD), and CQRS principles using .NET 9, TypeScript, and modern development practices.

## 🎯 Project Overview

This application serves as a technical assessment showcasing proficiency in:
- **Clean Architecture** with proper separation of concerns
- **Domain-Driven Design (DDD)** with rich domain models
- **CQRS (Command Query Responsibility Segregation)** pattern
- **Modern .NET 9** development practices
- **TypeScript/React** frontend development
- **Comprehensive testing** strategies
- **Containerized deployment** with Docker

## 🏗️ Architecture Overview

### Backend Architecture (.NET 9)

The solution follows Clean Architecture principles with four distinct layers:

```
TaskManagement.sln
├── src/
│   ├── TaskManagement.Domain/           # Core business logic & entities
│   ├── TaskManagement.Application/      # Use cases, CQRS, validation
│   ├── TaskManagement.Infrastructure/  # Data access, external services
│   └── TaskManagement.API/             # Web API, controllers
└── tests/
    ├── TaskManagement.Domain.Tests/     # Domain logic tests
    └── TaskManagement.Application.Tests/ # Application logic tests
```

#### Layer Responsibilities

- **Domain Layer**: Contains business entities, domain events, and repository interfaces
- **Application Layer**: Implements CQRS pattern with commands/queries, validation, and business logic
- **Infrastructure Layer**: Provides data persistence with EF Core and external service implementations
- **API Layer**: Exposes RESTful endpoints with Swagger documentation

### Frontend Architecture (React + TypeScript)

- **Modern React** with functional components and hooks
- **TypeScript** for type safety and better developer experience
- **Tailwind CSS** for responsive, utility-first styling
- **Clean component structure** with proper separation of concerns

## 🚀 Getting Started

### Prerequisites

- .NET 9 SDK
- Node.js 18+ and npm
- Docker and Docker Compose
- SQL Server (or use Docker container)

### Quick Start with Docker

The fastest way to get started is using Docker Compose:

```bash
# Clone the repository
git clone <your-repo-url>
cd hahn_software_test_technique

# Start all services
docker-compose up -d

# The application will be available at:
# - API: http://localhost:5000
# - Swagger: http://localhost:5000/swagger
# - Frontend: http://localhost:3000
```

### Manual Setup

#### Backend Setup

```bash
# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update --project src/TaskManagement.Infrastructure --startup-project src/TaskManagement.API

# Run the API
dotnet run --project src/TaskManagement.API
```

#### Frontend Setup

```bash
cd frontend

# Install dependencies
npm install

# Start development server
npm start
```

## 🔧 Technical Implementation Details

### CQRS Pattern

The application implements CQRS using MediatR:

- **Commands**: `CreateTaskCommand`, `UpdateTaskCommand`, `StartTaskCommand`, etc.
- **Queries**: `GetAllTasksQuery`, `GetTaskByIdQuery`, `GetTasksByStatusQuery`
- **Handlers**: Separate handlers for each command/query with proper separation of concerns

### Domain Events

Domain events are implemented using MediatR notifications:

- `TaskCreatedEvent`
- `TaskUpdatedEvent`
- `TaskStartedEvent`
- `TaskCompletedEvent`
- `TaskCancelledEvent`

### Validation

Input validation is handled using FluentValidation:

- Comprehensive validation rules for all DTOs
- Custom validation messages
- Integration with ASP.NET Core pipeline

### Data Persistence

- **Entity Framework Core 9** with SQL Server
- **Repository Pattern** for data access abstraction
- **Code-First Migrations** for database schema management
- **Proper entity configuration** with fluent API

## 📚 API Documentation

### Available Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/tasks` | Retrieve all tasks |
| `GET` | `/api/tasks/{id}` | Retrieve task by ID |
| `GET` | `/api/tasks/status/{status}` | Retrieve tasks by status |
| `POST` | `/api/tasks` | Create new task |
| `PUT` | `/api/tasks/{id}` | Update existing task |
| `POST` | `/api/tasks/{id}/start` | Start a pending task |
| `POST` | `/api/tasks/{id}/complete` | Complete an in-progress task |
| `POST` | `/api/tasks/{id}/cancel` | Cancel a task |
| `DELETE` | `/api/tasks/{id}` | Delete a task |

### Task Status Flow

```
Pending → InProgress → Completed
    ↓
  Cancelled
```

### Priority Levels

- **Low**: Basic priority tasks
- **Medium**: Standard priority tasks
- **High**: Important tasks
- **Critical**: Urgent tasks requiring immediate attention

## 🧪 Testing Strategy

### Backend Testing

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/TaskManagement.Domain.Tests/
dotnet test tests/TaskManagement.Application.Tests/
```

#### Test Coverage

- **Domain Tests**: Entity behavior, domain events, business rules
- **Application Tests**: Command/query handlers, validation
- **Unit Tests**: Isolated testing with Moq for mocking

### Frontend Testing

```bash
cd frontend
npm test
```

## 🐳 Docker Configuration

### Services

- **SQL Server**: Microsoft SQL Server 2022 with persistent storage
- **API**: .NET 9 application with hot reload support
- **Network**: Isolated network for service communication

### Environment Variables

- Database connection strings
- SQL Server authentication
- API configuration

## 📦 Dependencies

### Backend Packages

- **MediatR**: CQRS and domain event implementation
- **Entity Framework Core**: ORM and data access
- **FluentValidation**: Input validation
- **Swashbuckle**: Swagger/OpenAPI documentation

### Frontend Packages

- **React**: UI framework
- **TypeScript**: Type-safe JavaScript
- **Tailwind CSS**: Utility-first CSS framework

## 🔍 Code Quality Features

- **Clean Architecture** principles enforced through project structure
- **Dependency Injection** for loose coupling
- **Async/Await** patterns throughout the application
- **Proper error handling** and validation
- **Comprehensive logging** and monitoring
- **Type safety** with TypeScript and C#

## 🚀 Deployment

### Production Considerations

- Environment-specific configuration
- Database connection string management
- Logging and monitoring setup
- Health checks and readiness probes
- Security headers and CORS configuration

### Scaling

- Stateless API design for horizontal scaling
- Database connection pooling
- Caching strategies for frequently accessed data

## 🤝 Contributing

This project demonstrates a technical assessment submission. For production use:

1. Review and enhance security measures
2. Add comprehensive error handling
3. Implement logging and monitoring
4. Add performance optimization
5. Consider adding authentication/authorization

## 📝 License

This project is created for technical assessment purposes.

## 🎯 Assessment Highlights

This implementation demonstrates:

✅ **Clean Architecture** with proper layer separation  
✅ **CQRS Pattern** using MediatR  
✅ **Domain-Driven Design** with rich domain models  
✅ **Modern .NET 9** development practices  
✅ **Comprehensive Testing** strategy  
✅ **Containerized Deployment** with Docker  
✅ **TypeScript/React** frontend with modern UI  
✅ **Entity Framework Core** with migrations  
✅ **Input Validation** using FluentValidation  
✅ **Domain Events** for business process tracking  
✅ **Swagger/OpenAPI** documentation  
✅ **Repository Pattern** for data access  

---

**Built with ❤️ for Hahn Softwareentwicklung GmbH Technical Assessment**
