# Setup

## Prerequisites

- .NET SDK 8+ installed
- Node.js 18+ and npm (or pnpm/yarn)
- Optional: Angular CLI if developing locally (`npm i -g @angular/cli`)

## Backend (API)

1. Open a terminal and go to the API project folder:

```bash
cd api/src/ShoppingCart.Presentation
```

2. Restore and run the API:

```bash
dotnet restore
dotnet run --project ShoppingCart.Presentation.csproj
```

The API runs by default on the configured port (see `appsettings.json`).

## Frontend (Client)

1. Open a terminal and go to the client folder:

```bash
cd client
```

2. Install dependencies and start the dev server:

```bash
npm install
npm start
```

By default the Angular app runs on `http://localhost:4200`.

## Running Tests (backend)

- If a test project exists, run:

```bash
cd api/tests/ShoppingCart.Application.Tests
dotnet test/t
```

## Notes

- Use environment-specific `appsettings.*.json` files for configuration overrides.
- If ports conflict, update `appsettings.json` or the Angular proxy configuration.
