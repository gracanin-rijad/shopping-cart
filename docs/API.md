# API Documentation

## Overview

This document describes the public API endpoints exposed by the ShoppingCart backend (Presentation layer).

## Cart

- GET /api/cart
  - Returns the current cart
- POST /api/cart/items
  - Adds an item to the cart
  - Body: `{ productId: string, quantity: number }`
- PUT /api/cart/items/{itemId}
  - Updates quantity for a cart item
  - Body: `{ quantity: number }`
- DELETE /api/cart/items/{itemId}
  - Removes an item from the cart

## Orders

- POST /api/orders
  - Creates an order from the current cart
  - Body: checkout details (name, address, payment stub)

## Products

- GET /api/products
  - Returns list of available products
- GET /api/products/{id}
  - Returns product details

## Response format

- Successful responses use standard HTTP codes (200/201/204).
- Errors return a JSON payload: `{ "message": "...", "errors": [...] }`.

## Authentication

- Currently the API is unauthenticated (development/demo). Add auth middleware in Production.

## Examples

Refer to the frontend services under `client/src/app/services/` for example usage.
