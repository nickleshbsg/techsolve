# TechSolve Consulting — Full-Stack Solution

**Angular 19 + .NET Core 9 · Design 5 (Sapphire Gradient)**

---

## Solution Structure

```
TechSolve.sln
│
├── [Backend]                         ← Solution folder in VS
│   ├── TechSolve.Domain/             Layer 1 — Entities, Enums, Interfaces,
│   │   ├── Constants/                           DTOs, Requests, Responses, Constants
│   │   ├── DTOs/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Interfaces/
│   │   ├── Requests/
│   │   └── Responses/
│   │
│   ├── TechSolve.DataModel/          Layer 2 — EF Core DbContext + configurations
│   │   ├── AppDbContext.cs
│   │   └── Configurations/
│   │
│   ├── TechSolve.Repository/         Layer 3 — Generic + specific repositories
│   │   └── Implementations/
│   │
│   ├── TechSolve.Service/            Layer 4 — Business logic
│   │   ├── Implementations/
│   │   └── Interfaces/
│   │
│   ├── TechSolve.Infrastructure/     Cross-cutting — Email (MailKit), notifications
│   │   └── Email/
│   │
│   └── TechSolve.API/                Layer 5 — REST API, middleware, Swagger
│       ├── Controllers/
│       ├── Extensions/
│       │   ├── ServiceExtensions.cs
│       │   └── SpaExtensions.cs      ← Dev/Prod SPA wiring
│       ├── Middleware/
│       ├── Program.cs
│       ├── appsettings.json
│       ├── appsettings.Development.json
│       └── appsettings.Production.json
│
├── [Frontend]                        ← Solution folder in VS
│   └── TechSolve.UI/                 Angular host project
│       ├── TechSolve.UI.csproj       ← npm build hooks (ng build on publish)
│       └── clientApp/                ← ALL Angular 19 source files
│           ├── angular.json
│           ├── package.json
│           ├── tsconfig.json
│           └── src/
│               ├── app/
│               │   ├── core/         Models, Services
│               │   ├── shared/       Navbar, Footer, WhatsApp btn, Enquiry modal
│               │   └── features/     Home, Services, Blog, About, Contact
│               ├── environments/
│               ├── styles/
│               └── index.html
│
├── rename-project.sh                 ← One-command project rename
├── .gitignore
└── README.md
```

---

## How Dev vs Production Works

### Development
```
Terminal 1:  cd TechSolve.UI/clientApp && npm start
             → Angular dev server on http://localhost:4200 (HMR enabled)

Terminal 2:  cd TechSolve.API && dotnet run
             → API on https://localhost:5001
             → Non-API requests are proxied to :4200 via SpaExtensions
             → Swagger at https://localhost:5001/swagger
```

### Production
```
dotnet publish TechSolve.API -c Release
```
`TechSolve.UI.csproj` automatically runs `ng build --configuration=production`
and copies the Angular dist into `TechSolve.API/wwwroot`.
The API then serves Angular as static files with index.html fallback.
**Single process. No separate Angular server needed.**

---

## Quick Start (Development)

```bash
# 1. Restore Angular dependencies
cd TechSolve.UI/clientApp
npm install

# 2. Start Angular dev server (keep this running)
npm start

# 3. In a new terminal — update connection string, then start API
cd TechSolve.API
dotnet run
# → https://localhost:5001 (API)
# → https://localhost:5001/swagger (Swagger UI)
# → Angular served via proxy from http://localhost:4200
```

---

## Renaming the Project

The solution is fully name-agnostic. When your brand is finalised:

```bash
chmod +x rename-project.sh
./rename-project.sh "YourBrandName"
```

This renames **everything** in one shot:
- All folder names (`TechSolve.API` → `YourBrandName.API`, etc.)
- All file names (`TechSolve.sln` → `YourBrandName.sln`, etc.)
- All file contents (namespaces, using statements, project references)
- All Angular files (selectors use `ts-` prefix — update separately if needed)

After renaming, update the display strings manually:
| File | What to change |
|------|---------------|
| `YourBrandName.Domain/Constants/AppConstants.cs` | `AppName`, `AdminEmail`, `SupportPhone` |
| `YourBrandName.UI/clientApp/src/index.html` | `<title>` tag |
| `appsettings.json` | `EmailSettings.FromName` |

Then rebuild:
```bash
dotnet build YourBrandName.sln
```

---

## N-Layer Architecture

| Layer | Project | Responsibility |
|-------|---------|----------------|
| **Domain** | `TechSolve.Domain` | Entities, Enums, Repository interfaces, DTOs, Requests, Responses, Constants. **No EF dependency.** |
| **DataModel** | `TechSolve.DataModel` | EF Core `AppDbContext`, entity configurations, seed data, migrations |
| **Repository** | `TechSolve.Repository` | `GenericRepository<T>` + specific repos for Enquiry, Blog, Service, WhatsApp |
| **Service** | `TechSolve.Service` | Business logic — EnquiryService, BlogService, ServiceCatalogService, WhatsAppTrackingService |
| **Infrastructure** | `TechSolve.Infrastructure` | Cross-cutting concerns — SMTP email via MailKit |
| **API** | `TechSolve.API` | REST controllers, `ApiResponse<T>` wrapper, exception middleware, Swagger, SPA hosting |
| **UI** | `TechSolve.UI` | Angular 19 host. Dev: proxies to ng serve. Prod: builds dist into API wwwroot. |

---

## API Endpoints

All responses wrapped in `ApiResponse<T>` `{ success, data, message, errors }`.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/enquiry` | Submit new enquiry → returns ticket ID |
| `GET`  | `/api/enquiry/{ticketId}` | Look up enquiry by ticket |
| `GET`  | `/api/enquiry` | Paged enquiry list |
| `GET`  | `/api/services` | All active services |
| `GET`  | `/api/services/{slug}` | Service detail |
| `GET`  | `/api/blog` | Published posts (paged, filterable by category) |
| `GET`  | `/api/blog/{slug}` | Blog post detail |
| `POST` | `/api/whatsapptracking/track` | Track WhatsApp click event |
| `GET`  | `/api/whatsapptracking/analytics` | WhatsApp analytics by date range |

---

## Tech Stack

| Area | Technology |
|------|-----------|
| Frontend | Angular 19, TypeScript, SCSS, standalone components |
| Backend | .NET 9, ASP.NET Core Web API |
| ORM | Entity Framework Core 9, SQL Server |
| Email | MailKit / MimeKit |
| Logging | Serilog (console + rolling file) |
| Docs | Swagger / Swashbuckle |
| Design | Sapphire Gradient — `#1a3ed4`, Outfit font |
