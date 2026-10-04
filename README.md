# Carrivo Backend

Backend REST API for Carrivo, a mentorship and educational platform. Built with ASP.NET Core 8, Entity Framework Core and SQL Server, with JWT-based authentication.

## Overview

This repository contains the server-side application for Carrivo. It exposes a documented REST API consumed by the web client and handles authentication, user management and data persistence.

## Architecture

The solution is split into four projects with a one-way dependency flow.

| Project | Responsibility |
|---|---|
| `Carrivo` | ASP.NET Core Web API: controllers, middleware, Swagger and dependency injection setup |
| `Carrivo.Application` | Application services and business logic, including token handling |
| `Carrivo.Core` | Domain entities and Identity models. Has no dependencies on other projects |
| `Carrivo.Infrastructure` | Entity Framework Core data access and database configuration |

Repositories and services are registered through dependency injection extension methods in the API project.

## Tech Stack

- .NET 8, ASP.NET Core Web API
- Entity Framework Core 8 with SQL Server
- ASP.NET Core Identity
- JWT Bearer authentication
- Swagger / OpenAPI (Swashbuckle) with XML documentation
- SMTP for email delivery

## Features

- JWT authentication with short-lived access tokens (15 minutes by default) and refresh tokens (7 days by default), both configurable
- User management built on ASP.NET Core Identity
- SQL Server persistence through Entity Framework Core
- Interactive API documentation with bearer token support
- Configurable CORS policy for the frontend client
- SMTP email settings for transactional emails

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (local instance or container)

### Setup

1. Clone the repository and switch to the development branch.

```bash
   git clone https://github.com/Carrivo-Mentorship/Carrivo-Backend.git
   cd Carrivo-Backend
   git checkout dev
```

2. Create `Carrivo/appsettings.Development.Local.json`. The application loads this file automatically when present. Do not commit it.

```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=Carrivo;Trusted_Connection=True;TrustServerCertificate=True"
     },
     "JwtSettings": {
       "SecretKey": "<a long random secret, at least 32 characters>"
     },
     "EmailSettings": {
       "Username": "<smtp username>",
       "Password": "<smtp password or app password>"
     }
   }
```

3. Apply the database migrations.

```bash
   dotnet ef database update --project Carrivo.Infrastructure --startup-project Carrivo
```

4. Run the API.

```bash
   dotnet run --project Carrivo
```

5. Open Swagger UI at `https://localhost:<port>/swagger`. The port is printed in the console on startup.

## Configuration

| Section | Purpose |
|---|---|
| `JwtSettings` | Secret key, issuer, audience and token lifetimes (`AccessTokenExpirationMinutes`, `RefreshTokenExpirationDays`) |
| `CorsSettings` | Allowed frontend origins. Defaults include `http://localhost:5173` and `http://localhost:5174` |
| `EmailSettings` | SMTP server, port and credentials |

Keep secrets out of source control. Use `appsettings.Development.Local.json`, .NET user secrets or environment variables.

## API Documentation

Swagger UI is available at `/swagger`. To call protected endpoints, obtain a token from the authentication endpoints, click **Authorize**, and enter `Bearer <token>`.
