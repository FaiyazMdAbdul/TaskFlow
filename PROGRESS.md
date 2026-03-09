# TaskFlow API — Build Progress Tracker

> Last updated: 2026-03-04

---

## Overall Progress

```
Week 1 — Core API + Auth + EF Core     [ █░░░░░░░░░ ]  15%
Week 2 — Notification Worker + Tests   [ ░░░░░░░░░░ ]   0%
Week 3 — Deploy + Interview Prep       [ ░░░░░░░░░░ ]   0%
```

---

## Project Structure

| Project | Status | Notes |
|---|---|---|
| `TaskFlow.API` | ✅ Scaffolded | Boilerplate only (WeatherForecast template) |
| `TaskFlow.Application` | ✅ Scaffolded | Empty, refs Domain + Contracts |
| `TaskFlow.Domain` | ✅ Scaffolded | Empty |
| `TaskFlow.Infrastructure` | ✅ Scaffolded | Empty, refs Application + Domain |
| `TaskFlow.Contracts` | ✅ Scaffolded | Empty |
| `TaskFlow.NotificationWorker` | ✅ Scaffolded | Basic Worker.cs only |
| `TaskFlow.UnitTests` | ❌ Not created | |
| `TaskFlow.IntegrationTests` | ❌ Not created | |

---

## Week 1 — Core API + Auth + EF Core

### Day 1–2: Project Setup

- [ ] Clean up boilerplate (remove WeatherForecast from API)
- [ ] Install NuGet packages (EF Core, SQL Server, MediatR, FluentValidation, Serilog, Swagger)
- [ ] Define domain entities with Fluent API config
  - [x] `User`
  - [x] `Project`
  - [x] `TaskItem`
  - [x] `Comment`
  - [x] `ActivityLog`
- [x] Define enums (`TaskStatus`, `TaskPriority`, `UserRole`)
- [ ] Define `IRepository<T>` interface in Domain
- [ ] Create `AppDbContext` with Fluent API configurations
- [ ] Create and run initial EF Core migration
- [ ] Docker Compose: API + SQL Server + Redis (3 containers)

### Day 3–4: Authentication

- [ ] JWT token generation service (`TokenService`)
- [ ] Refresh token support
- [ ] `AuthController` — `POST /api/auth/register`
- [ ] `AuthController` — `POST /api/auth/login`
- [ ] `AuthController` — `POST /api/auth/refresh`
- [ ] Role-based authorization (`[Authorize(Roles = "Admin")]`)

### Day 5–6: CRUD — Projects & Tasks

- [ ] Generic `IRepository<T>` + base repository implementation
- [ ] `PagedResult<T>` helper
- [ ] **Projects**
  - [ ] `CreateProjectCommand` + Handler + Validator
  - [ ] `UpdateProjectCommand` + Handler + Validator
  - [ ] `DeleteProjectCommand` + Handler (soft delete)
  - [ ] `GetProjectsQuery` + Handler (paginated)
  - [ ] `GetProjectByIdQuery` + Handler
  - [ ] `GetProjectStatsQuery` + Handler
  - [ ] `ProjectsController` wired to all above
- [ ] **Tasks**
  - [ ] `CreateTaskCommand` + Handler + Validator
  - [ ] `UpdateTaskCommand` + Handler + Validator
  - [ ] `DeleteTaskCommand` + Handler (soft delete)
  - [ ] `UpdateTaskStatusCommand` + Handler
  - [ ] `AssignTaskCommand` + Handler
  - [ ] `GetTasksQuery` + Handler (filter/sort/paginate)
  - [ ] `GetTaskByIdQuery` + Handler
  - [ ] `TasksController` wired to all above
- [ ] **Comments**
  - [ ] `AddCommentCommand` + Handler + Validator
  - [ ] `EditCommentCommand` + Handler
  - [ ] `DeleteCommentCommand` + Handler
  - [ ] `GetCommentsQuery` + Handler
  - [ ] `CommentsController` wired to all above
- [ ] **Users**
  - [ ] `GetCurrentUserQuery` + Handler
  - [ ] `UpdateProfileCommand` + Handler
  - [ ] `GetUsersQuery` + Handler (admin only)
  - [ ] `UsersController` wired to all above

### Day 7: Polish

- [ ] Global exception handling middleware (`ExceptionHandlingMiddleware`)
- [ ] Request logging middleware (`RequestLoggingMiddleware`)
- [ ] Serilog structured logging configured
- [ ] Swagger + JWT auth configured in Swagger UI
- [ ] Push to GitHub with initial README

---

## Week 2 — Notification Worker + Tests

### Day 8–9: Message Queue + Worker Service

- [ ] Add RabbitMQ to docker-compose (now 4 containers)
- [ ] Define event contracts in `TaskFlow.Contracts`
  - [ ] `TaskStatusChangedEvent`
  - [ ] `TaskAssignedEvent`
  - [ ] `CommentAddedEvent`
  - [ ] `TaskOverdueEvent`
  - [ ] `UserRegisteredEvent`
- [ ] Install MassTransit + MassTransit.RabbitMQ in API + Worker
- [ ] `IEventPublisher` interface in Application layer
- [ ] `EventPublisher` implementation in Infrastructure (MassTransit wrapper)
- [ ] Wire `EventPublisher` into command handlers
- [ ] **Consumers** in NotificationWorker
  - [ ] `TaskStatusChangedConsumer`
  - [ ] `TaskAssignedConsumer`
  - [ ] `CommentAddedConsumer`
  - [ ] `TaskOverdueConsumer`
- [ ] End-to-end test: change task status → RabbitMQ → consumer logs it

### Day 10–11: Production Features

- [ ] `NotificationHub.cs` (SignalR hub in API)
- [ ] `PushNotificationService` in Worker (calls API's SignalR hub via HTTP)
- [ ] `EmailNotificationService` in Worker (mock impl)
- [ ] `SlackNotificationService` in Worker (mock impl)
- [ ] Redis caching on frequently-read endpoints
  - [ ] Project stats
  - [ ] Task list
- [ ] Cache invalidation on writes
- [ ] `ActivityLog` auto-written via EF Core `SaveChanges` override
- [ ] Soft delete with global query filters
- [ ] `OverdueTaskChecker` background service in Worker (runs every 30 min)
- [ ] Rate limiting middleware on API
- [ ] Add Redis to docker-compose (now 5 containers)

### Day 12–13: Testing

- [ ] Create `TaskFlow.UnitTests` project
- [ ] Create `TaskFlow.IntegrationTests` project
- [ ] Unit tests — command handlers (xUnit + Moq)
  - [ ] CreateTask handler
  - [ ] UpdateTaskStatus handler
  - [ ] AssignTask handler
  - [ ] Auth — register / login
- [ ] Unit tests — notification consumers
  - [ ] `TaskStatusChangedConsumer`
  - [ ] `TaskAssignedConsumer`
- [ ] Integration tests — WebApplicationFactory + in-memory DB
  - [ ] Auth flow (register → login → get token)
  - [ ] CRUD operations
  - [ ] Authorization checks (forbidden scenarios)
  - [ ] Validation failures (bad request scenarios)
  - [ ] Event publishing
- [ ] 15–20 tests total passing
- [ ] `docker-compose up` — all 5 containers start cleanly

### Day 14: CI/CD

- [ ] GitHub Actions workflow (`ci.yml`)
  - [ ] Build API project
  - [ ] Build Worker project
  - [ ] Run tests
  - [ ] Build Docker images
- [ ] `Dockerfile.api` (multi-stage build)
- [ ] `Dockerfile.worker` (multi-stage build)
- [ ] Update README with build status badge

---

## Week 3 — Deploy + Interview Prep

### Day 15–16: Azure Deployment

- [ ] Deploy API to Azure App Service (free tier)
- [ ] Azure SQL Database provisioned
- [ ] Environment-specific config (dev vs production appsettings)
- [ ] Health check endpoint (`/health`)
- [ ] Deployed URL working and public

### Day 17–18: README Polish

- [ ] Architecture diagram (Mermaid)
- [ ] API docs with example requests/responses
- [ ] "Technical Decisions" section
- [ ] Performance notes (caching impact)
- [ ] Swagger UI screenshot

### Day 19–21: Interview Prep

- [ ] Study top 30 ASP.NET Core interview questions
- [ ] Can explain middleware pipeline order
- [ ] Can explain DI lifecycle (Transient/Scoped/Singleton + captive dependency gotcha)
- [ ] Can explain EF Core change tracking + AsNoTracking
- [ ] Can explain JWT flow end-to-end
- [ ] Can walk through entire codebase explaining every architectural decision
- [ ] Elevator pitch rehearsed

---

## NuGet Packages Tracker

### TaskFlow.API
| Package | Installed | Version |
|---|---|---|
| Swashbuckle.AspNetCore | ❌ | |
| Serilog.AspNetCore | ❌ | |
| Microsoft.AspNetCore.Authentication.JwtBearer | ❌ | |
| MassTransit.RabbitMQ | ❌ | |
| Microsoft.AspNetCore.SignalR | ❌ | (built-in) |
| AspNetCoreRateLimit | ❌ | |

### TaskFlow.Application
| Package | Installed | Version |
|---|---|---|
| MediatR | ❌ | |
| FluentValidation.DependencyInjectionExtensions | ❌ | |
| AutoMapper | ❌ | |

### TaskFlow.Infrastructure
| Package | Installed | Version |
|---|---|---|
| Microsoft.EntityFrameworkCore.SqlServer | ❌ | |
| Microsoft.EntityFrameworkCore.Tools | ❌ | |
| StackExchange.Redis | ❌ | |
| MassTransit.RabbitMQ | ❌ | |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | ❌ | |

### TaskFlow.NotificationWorker
| Package | Installed | Version |
|---|---|---|
| MassTransit.RabbitMQ | ❌ | |
| Microsoft.AspNetCore.SignalR.Client | ❌ | |
| Serilog.Extensions.Hosting | ❌ | |

### TaskFlow.UnitTests / IntegrationTests
| Package | Installed | Version |
|---|---|---|
| xunit | ❌ | |
| Moq | ❌ | |
| Microsoft.AspNetCore.Mvc.Testing | ❌ | |
| FluentAssertions | ❌ | |

---

## Docker Compose Status

| Container | Configured | Running |
|---|---|---|
| `api` | ❌ | ❌ |
| `worker` | ❌ | ❌ |
| `sqlserver` | ❌ | ❌ |
| `redis` | ❌ | ❌ |
| `rabbitmq` | ❌ | ❌ |

---

## API Endpoints Status

### Auth
| Method | Endpoint | Status |
|---|---|---|
| POST | `/api/auth/register` | ❌ |
| POST | `/api/auth/login` | ❌ |
| POST | `/api/auth/refresh` | ❌ |

### Projects
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/projects` | ❌ |
| POST | `/api/projects` | ❌ |
| GET | `/api/projects/{id}` | ❌ |
| PUT | `/api/projects/{id}` | ❌ |
| DELETE | `/api/projects/{id}` | ❌ |
| GET | `/api/projects/{id}/stats` | ❌ |

### Tasks
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/projects/{projectId}/tasks` | ❌ |
| POST | `/api/projects/{projectId}/tasks` | ❌ |
| GET | `/api/tasks/{id}` | ❌ |
| PUT | `/api/tasks/{id}` | ❌ |
| PATCH | `/api/tasks/{id}/status` | ❌ |
| PATCH | `/api/tasks/{id}/assign` | ❌ |
| DELETE | `/api/tasks/{id}` | ❌ |

### Comments
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/tasks/{taskId}/comments` | ❌ |
| POST | `/api/tasks/{taskId}/comments` | ❌ |
| PUT | `/api/comments/{id}` | ❌ |
| DELETE | `/api/comments/{id}` | ❌ |

### Users
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/users/me` | ❌ |
| PUT | `/api/users/me` | ❌ |
| GET | `/api/users` | ❌ |

---

## Domain Entities Status

| Entity | Defined | EF Config | Migration |
|---|---|---|---|
| `User` | ✅ | ❌ | ❌ |
| `Project` | ✅ | ❌ | ❌ |
| `TaskItem` | ✅ | ❌ | ❌ |
| `Comment` | ✅ | ❌ | ❌ |
| `ActivityLog` | ✅ | ❌ | ❌ |

---

## Event Contracts Status

| Event | Defined | Published | Consumed |
|---|---|---|---|
| `TaskStatusChangedEvent` | ❌ | ❌ | ❌ |
| `TaskAssignedEvent` | ❌ | ❌ | ❌ |
| `CommentAddedEvent` | ❌ | ❌ | ❌ |
| `TaskOverdueEvent` | ❌ | ❌ | ❌ |
| `UserRegisteredEvent` | ❌ | ❌ | ❌ |

---

## Milestones

| Milestone | Target | Status |
|---|---|---|
| Solution scaffolded | Day 1 | ✅ Done |
| First migration runs | Day 2 | ❌ |
| Login returns JWT | Day 4 | ❌ |
| CRUD endpoints working in Swagger | Day 6 | ❌ |
| Task status change flows through RabbitMQ | Day 9 | ❌ |
| SignalR notification received in browser | Day 10 | ❌ |
| All 5 containers up via docker-compose | Day 11 | ❌ |
| 15+ tests passing | Day 13 | ❌ |
| CI pipeline green | Day 14 | ❌ |
| Live Azure URL | Day 16 | ❌ |
| Interview-ready | Day 21 | ❌ |
