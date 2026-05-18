# Product Requirements Document (PRD)

## Project
Shopping Cart Application (Angular + .NET)

Engineering rules reference: [../.github/rules.md](../.github/rules.md)

## 1. Purpose
Build a simple shopping cart application where users can browse products, manage a cart, and place orders.

## 2. Goals
- Enable users to view products and product details.
- Enable users to add, update, and remove cart items.
- Enable users to complete checkout and create an order.
- Provide a clear API contract between Angular frontend and .NET backend.

## 3. Non-Goals (V1)
- Payment gateway integration
- Discounts and coupons
- Multi-vendor marketplace features
- Advanced analytics

## 4. Users
- Guest shopper (primary)
- Store admin (future phase)

## 5. Core Features (V1)
- Product listing
- Product detail view
- Cart management
- Checkout form
- Order confirmation

## 6. Functional Requirements
- User can list products.
- User can view product details by ID.
- User can add an item to cart.
- User can update item quantity in cart.
- User can remove item from cart.
- User can submit checkout details.
- System can create an order and return confirmation data.

## 7. Non-Functional Requirements
- Core API endpoints should respond in under 500 ms in local dev environments for typical requests.
- Client-side and server-side validation must be implemented for required fields.
- Error responses must be consistent JSON.
- CORS must allow Angular dev server to communicate with .NET API.

## 8. API Scope (V1)
- GET /api/products
- GET /api/products/{id}
- GET /api/cart
- POST /api/cart/items
- PUT /api/cart/items/{itemId}
- DELETE /api/cart/items/{itemId}
- POST /api/orders

## 9. Frontend Scope (V1)
- Pages: Product List, Cart, Checkout, Order Confirmation
- Services: ProductService, CartService, OrderService
- Responsive layout for desktop and mobile

## 10. Backend Scope (V1)
- REST API with ASP.NET Core
- Data models for Product, CartItem, and Order
- Basic persistence (SQLite recommended for V1)
- Validation and structured error handling

## 11. Success Metrics
- User can complete product-to-order flow without blockers.
- All V1 endpoints are implemented and callable from frontend.
- End-to-end local demo works reliably.

## 12. Milestones
1. Scaffold Angular and .NET projects
2. Implement Product APIs and Product List page
3. Implement Cart APIs and Cart page
4. Implement Checkout flow and Order endpoint
5. Add tests, polish UX, and finalize documentation

## 13. Risks
- Scope creep in V1
- API/frontend contract drift
- Incomplete validation causing runtime errors

## 14. Open Questions
- Guest checkout only, or basic user accounts in V1?
- SQLite only for V1, or optional SQL Server from start?
- Should stock be validated during add-to-cart or checkout?
