# Angular Guidelines

## Project structure

- Keep components small and focused; prefer presentational vs container separation.
- Use strongly-typed interfaces under `client/src/app/models`.
- Services live in `client/src/app/services` and use `HttpClient` for API calls.

## Styling

- Use SCSS and keep variables in `client/src/styles.scss` or a dedicated variables file.

## State

- Keep component-local state where possible; lift shared state into services with Observables.

## Best practices

- Prefer explicit typing, avoid `any`.
- Use Angular `HttpInterceptor` for error handling and adding headers.
- Centralize API base URL in an environment file.
