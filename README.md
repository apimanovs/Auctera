# Auctera

> **Auctera** is a modular backend platform for fashion/resale auctions. Sellers publish lots, buyers place real-time bids, and after an auction ends the system creates a payment order.

---

## 🚀 What this project is

Auctera implements a practical C2C/B2C auction flow:

- a seller creates and publishes a lot,
- an auction starts for that lot,
- users place bids in real time,
- the system automatically closes the auction,
- if there is a winner, an `Order` is created with a payment window (MVP: 48 hours),
- unpaid overdue orders are moved to `PaymentExpired` by a background process.

This is the foundation for a marketplace backend with Stripe Connect, second-chance offers, and richer catalog capabilities.

---

## 🧩 Core MVP capabilities

### Auctions
- Create auctions for published lots.
- Start/stop auctions.
- Auto-stop auctions by schedule (background service).
- Anti-sniping logic (auction extension for late bids).

### Bidding
- Place bids with minimum-price and currency validation.
- Mark previous active bid as outbid.
- Real-time notifications (SignalR hub + notifier).

### Lots / Items
- Create and publish lots.
- Catalog filter attributes:
  - `Category` (`Tops/Bottoms/Outerwear/Footwear/Accessories/Other`)
  - `Gender` (`Mens/Womens/Unisex`)
  - `Size`, `Brand`, `Condition`, `Color`
- API for list/details/edit/delete (draft-only edit/delete).

### Orders (post-auction)
- Create `Order` when an auction finishes with a winner.
- Order statuses: `PendingPayment`, `Paid`, `PaymentExpired`, `Cancelled`, `Refunded`.
- API endpoints for “my orders” and order details.
- Automatic expiration based on `PaymentDeadlineUtc`.

### Identity
- Registration/login.
- JWT authentication.
- Basic rate limiting for auth and bid endpoints.

---

## 🏗 Architecture

The project is a **modular monolith** on .NET 8:

- each business domain is its own module (`items`, `auctions`, `bids`, `orders`, `identity`),
- each module follows layered architecture:
  - `Domain`
  - `Application`
  - `Infrastructure`
  - `API`
- a single host (`Auctera.Host`) composes module APIs via `AddApplicationPart`.

### Why this architecture

1. **Fast MVP delivery** without microservice overhead.
2. **Clear bounded contexts** between business domains.
3. **Straightforward evolution path** to extract modules into services when scaling.

---

## 🧠 DDD and event-driven model

Auctera uses:

- aggregates and value objects,
- domain events (`AuctionEnded`, `OrderCreated`, `OrderPaid`, `OrderExpired`, etc.),
- centralized domain event dispatching through MediatR.

Benefits:

- business rules are separated from infrastructure concerns,
- post-auction flows are easy to extend,
- low coupling between modules.

---

## 🛠 Tech stack

- **.NET 8 / ASP.NET Core**
- **Entity Framework Core 8**
- **PostgreSQL (Npgsql provider)**
- **MediatR** (CQRS + notifications)
- **SignalR** (real-time)
- **Swagger/OpenAPI**
- **JWT authentication**
- **Dockerfile** for the host

---

## 📁 Repository structure

```text
/backend
  /host                 -> app entrypoint, middleware, background jobs
  /modules
    /identity           -> auth and users
    /items              -> lots/catalog
    /auctions           -> auctions
    /bids               -> bidding
    /orders             -> post-auction orders and payment lifecycle
  /persistance          -> DbContext, EF configurations, migrations
  /realtime             -> SignalR hub/notifier
  /shared               -> shared abstractions, value objects, time, dispatcher
```

---

## 🔐 Security and reliability (MVP)

- Global exception middleware.
- Rate limiting on sensitive endpoints.
- Domain invariant checks inside aggregates.
- Background jobs for auction auto-stop and order payment expiration.

---

## 💳 Payment direction

The current order model already prepares the platform for marketplace payments:

- `Order` stores payment deadlines,
- Stripe-related fields exist (`StripeCheckoutSessionId`),
- status transitions and events are ready for webhook-driven payment updates.

Recommended target integration: **Stripe Connect** (`destination charges` + `application_fee_amount`).

## 📌 Presentation positioning

**Auctera** is a backend-first auction engine for fashion/resale marketplaces with real-time bidding and a post-auction payment lifecycle.

Strong presentation points:

- transparent modular architecture,
- production-oriented auction + bidding core,
- built-in order lifecycle aligned with marketplace payments,
- clear migration path to Stripe Connect and future scaling.
