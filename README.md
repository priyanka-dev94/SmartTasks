# SmartTasks

SmartTasks is a production-style task management application designed to demonstrate
real-world API design, domain modeling, and frontend–backend integration.

The project focuses on correctness, maintainability, and clear business rules
rather than superficial features.

## Key Features

- Task lifecycle management (Pending, In Progress, Snoozed, Completed, Archived)
- Snooze functionality with due-date–based behavior
- Overdue detection with visual feedback
- Safe destructive actions with confirmation
- Server-side pagination and filtering
- RFC 7807–compliant error handling
- Full-stack React + ASP.NET Core integration

## Architecture

### Backend
- ASP.NET Core Web API
- Clean Architecture (API, Application, Domain, Infrastructure)
- Entity Framework Core with SQL Server
- Centralized exception handling
- Fluent validation with ProblemDetails responses

### Frontend
- React (Vite + TypeScript)
- React Query for server-state management
- Tailwind CSS for styling
- Clear separation of components, hooks, and API clients

## Key Design Decisions

- **Snooze as status + due date**  
  Snoozing postpones urgency without changing ownership or completion state.

- **Archive vs Delete**  
  Archive hides tasks without data loss, while delete is irreversible and confirmed.

- **Server-side pagination**  
  Prevents over-fetching and mirrors real production APIs.

- **ProblemDetails for errors**  
  Ensures consistent, debuggable error responses across API and UI.

- **PUT-style updates**  
  The update API requires full resource state to keep behavior explicit and predictable.

  ## Tech Stack

- ASP.NET Core
- Entity Framework Core
- SQL Server
- React
- TypeScript
- React Query
- Tailwind CSS
