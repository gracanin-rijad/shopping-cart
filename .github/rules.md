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
- Never push directly to master
- Start each feature on a new branch created from `master`
- Use one dedicated branch per feature
- Name feature branches using `feature/<number>-<short-name>`
- Example: `feature/1-product-catalog`
- Open a pull request back into `master` after finishing a feature
- Use fix branches named `fix/<short-description>` for isolated bug fixes
- Prefer one commit per completed task within a feature
- Write meaningful commit messages in imperative form
- Commit messages must use the same change types as the PR template
- Recommended format: `<type>: <summary>`
- Allowed types: feat, fix, docs, refactor, test, chore
- Example: `feat: add cart item update flow`

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
