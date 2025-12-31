SmartTasks UI

This is the frontend application for SmartTasks, built with React and TypeScript.
It consumes the SmartTasks ASP.NET Core Web API.

## Tech Stack

- ASP.NET Core
- Entity Framework Core
- SQL Server
- React
- TypeScript
- React Query
- Tailwind CSS


## Features

- Task list with pagination and filtering
- Create, edit, snooze, archive, unarchive, and delete tasks
- Overdue task highlighting
- Server-side validation error display
- Confirmation for destructive actions
- Clean separation of UI, hooks, and API clients

## Project Structure
src/
├── api/          # API client functions (Axios)
├── components/   # Reusable UI components
├── hooks/        # React Query hooks
├── models/       # TypeScript models & types
├── utils/        # Utility helpers (dates, snooze logic, etc.)
├── pages/        # Page-level components
└── main.tsx

## Prerequisites

Node.js 18+ (recommended)

npm

## Setup & Run
1. Install dependencies
npm install

2. Configure API base URL

Create a .env file in SmartTasks.UI:

VITE_API_BASE_URL=https://localhost:5001/api
(Adjust the URL to match the backend API.)

3. Run the development server
npm run dev
The app will be available at:
http://localhost:5173

## Notes

This UI expects the backend API to be running.

Authentication is intentionally not included in the initial phase.

The application uses React Query for server-state management instead of local/global state.

Backend

The backend API is located in the root solution under:

SmartTasks.API

Refer to the root README for backend architecture and design decisions.

## Future Enhancements

Authentication (Azure AD / Entra ID)

Deployment to Azure Static Web Apps

Environment-based configuration via Azure