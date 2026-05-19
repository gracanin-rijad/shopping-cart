# Tasks

Governance: Engineering rules and contribution standards are maintained in [../.github/rules.md](../.github/rules.md).

## Feature Workflow
- Start each feature on a new branch created from `master`.
- Use one dedicated branch per feature.
- Name feature branches using `feature/<number>-<short-name>`.
- Example: `feature/1-product-catalog`.
- After finishing a feature, open a pull request back into `master`.

## Backlog

### Feature 0 - Foundation Setup
- [ ] Scaffold Angular project in client/
- [ ] Scaffold .NET Core project in api/
- [ ] Setup clean architecture projects in api (Domain/Application/Infrastructure/Presentation)
- [ ] Configure CORS on backend
- [ ] Setup in-memory database
- [ ] Add global validation and error handling middleware

### Feature 1 - Product Catalog (Hardcoded)
- [ ] Backend: create Product entity and Product DTO
- [ ] Backend: create hardcoded Product list/service in Application layer
- [ ] Frontend: create Product model/interface
- [ ] Frontend: build Product List page using hardcoded products
- [ ] Frontend: build Product Detail page
- [ ] Frontend: add product list/detail styling

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
