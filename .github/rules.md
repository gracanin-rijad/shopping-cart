# Project Rules

## General
- Keep functions small and readable
- Prefer simplicity over complexity

## Backend (.NET) - Clean Architecture
- **Domain**: Business rules and entities (no external dependencies)
- **Application**: Use cases, services, DTOs (depends on Domain only)
- **Infrastructure**: Database, repositories, external services (depends on Application + Domain)
- **Presentation**: Controllers, API endpoints (depends on Application)
- Use DTOs, never expose entities directly
- Controllers should be thin (delegate to Application layer)
- Business logic goes into Application layer services

## Frontend (Angular)
- Use standalone components
- Keep services for API calls only
- No business logic inside components

## Naming
- Use consistent naming across frontend and backend

## API
- Always return JSON
- Use proper HTTP status codes

## Git & Collaboration
- Never push directly to main
- Use feature branches named `feature/<short-description>`
- Use fix branches named `fix/<short-description>`
- One feature = one commit
- Write meaningful commit messages in imperative form
- Recommended format: `<type>: <summary>`
- Types: feat, fix, docs, refactor, test, chore

## Pull Requests
- Keep PRs focused and small
- Link PR to issue when applicable
- Require at least one approval before merge
- Ensure all CI checks pass before merge

## Code Quality
- Frontend must pass lint/build checks
- Backend must pass restore/build/test checks
- Add or update tests for behavior changes

## Documentation
- Keep product docs in `docs/`
- Keep process and automation config in `.github/`
- Update docs when API contracts or user flows change

## Security
- Do not commit secrets, tokens, or credentials
- Use environment variables for sensitive values
- Report vulnerabilities privately
