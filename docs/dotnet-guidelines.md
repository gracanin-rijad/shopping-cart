# .NET Guidelines

## Project layout

- Keep Clean Architecture separation: Domain, Application, Infrastructure, Presentation.
- DTOs live in the Application layer; entities live in Domain.

## Dependency injection

- Use `DependencyInjection` static classes in each project to register services.

## Logging

- Use `Microsoft.Extensions.Logging` and inject `ILogger<T>` into services and controllers.
- Log at appropriate levels: `Information` for high-level events, `Warning` for recoverable issues, `Error` for exceptions.

## Testing

- Add unit tests for Application services and controllers using xUnit.
- Keep repositories abstracted and mock them in tests.

## Configuration

- Store environment overrides in `appsettings.Development.json`.
- Use `IOptions<T>` for typed configuration values.
