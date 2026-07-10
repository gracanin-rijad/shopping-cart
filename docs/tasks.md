# Tasks

Governance: Engineering rules and contribution standards are maintained in [../.github/rules.md](../.github/rules.md).

## Feature Workflow
Follow the Git and collaboration workflow in [../.github/rules.md](../.github/rules.md).

## Backlog

### Feature 0 - Foundation Setup
- [x] Scaffold Angular project in client/
- [x] Scaffold .NET Core project in api/
- [x] Setup clean architecture projects in api (Domain/Application/Infrastructure/Presentation)
- [x] Configure CORS on backend
- [x] Setup in-memory database
- [x] Add global validation and error handling middleware

### Feature 1 - Product Catalog (Hardcoded)
- [x] Backend: create Product entity and Product DTO
- [x] Backend: create hardcoded Product list/service in Application layer
- [x] Frontend: create Product model/interface
- [x] Frontend: build Product List page using hardcoded products
- [x] Frontend: build Product Detail page
- [x] Frontend: add product list/detail styling

### Feature 2 - Cart Management
- [x] Backend: create CartItem entity and cart repository interface
- [x] Backend: implement Cart repository (in-memory)
- [x] Backend: create Cart use cases/services and Cart DTOs
- [x] Backend: create CartController (GET/POST/PUT/DELETE /api/cart/*)
- [x] Frontend: create Cart model/interface/service
- [x] Frontend: build Cart page
- [x] Frontend: implement add/update/remove cart item flows
- [x] Frontend: display cart totals and empty-state UI styling

### Feature 3 - Checkout and Order Creation
- [x] Backend: create Order entity and order repository interface
- [x] Backend: implement Order repository (in-memory)
- [x] Backend: create Order use cases/services and Order DTOs
- [x] Backend: create OrderController (POST /api/orders)
- [x] Frontend: create OrderService
- [x] Frontend: build Checkout form component
- [x] Frontend: add checkout form validation
- [x] Frontend: submit order and show confirmation page
- [x] Frontend: style checkout and confirmation states

### Feature 4 - Documentation and Quality
- [ ] Write SETUP.md
- [ ] Write API documentation
- [ ] Write Angular guidelines
- [ ] Write .NET guidelines
- [ ] Add unit tests for core services
- [ ] Add integration tests for Cart and Order APIs

---

## In Progress

---

## Done
- Feature 0 - Foundation Setup
- Feature 3 - Checkout and Order Creation
