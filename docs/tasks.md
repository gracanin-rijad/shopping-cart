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
- [ ] Backend: create CartItem entity and cart repository interface
- [ ] Backend: implement Cart repository (in-memory)
- [ ] Backend: create Cart use cases/services and Cart DTOs
- [ ] Backend: create CartController (GET/POST/PUT/DELETE /api/cart/*)
- [ ] Frontend: create Cart model/interface/service
- [ ] Frontend: build Cart page
- [ ] Frontend: implement add/update/remove cart item flows
- [ ] Frontend: display cart totals and empty-state UI styling

### Feature 3 - Checkout and Order Creation
- [ ] Backend: create Order entity and order repository interface
- [ ] Backend: implement Order repository (in-memory)
- [ ] Backend: create Order use cases/services and Order DTOs
- [ ] Backend: create OrderController (POST /api/orders)
- [ ] Frontend: create OrderService
- [ ] Frontend: build Checkout form component
- [ ] Frontend: add checkout form validation
- [ ] Frontend: submit order and show confirmation page
- [ ] Frontend: style checkout and confirmation states

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
