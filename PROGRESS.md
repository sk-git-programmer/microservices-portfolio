# PROGRESS — microservices-portfolio

Last updated: 2026-08-04

## Overall status
Repository created on GitHub. Planning stage completed. No code written yet.

## Done
- [x] GitHub repo created: microservices-portfolio (public, README, .gitignore
      Visual Studio template, MIT License, Topics added)
- [x] Full work plan defined (7 phases, 26 steps — see "Phase plan" below)

## In progress
- [ ] Step 1: solution structure — Orders.Api / Orders.Domain /
      Orders.Infrastructure / Orders.Tests

## Next step
Solution skeleton (Phase 1, Step 1):
- MicroservicesPortfolio.sln
- src/Orders/Orders.Api (Web API) — goal: GET /health → "OK"
- src/Orders/Orders.Domain (Class Library)
- src/Orders/Orders.Infrastructure (Class Library)
- src/Orders/Orders.Tests (xUnit)

## Architecture decisions (summary)
- RabbitMQ.Client used directly (no MassTransit) — deliberate choice, to
  understand the underlying mechanics before reaching for an abstraction
- Database-per-service (PostgreSQL) — each microservice owns its own database
- Simplified Clean Architecture: Domain (pure business logic, no EF/ASP.NET
  dependency) / Infrastructure (EF Core, data access) / Api (HTTP layer)
- CI/CD added only after the code works locally (not from the start)

## Phase plan (full, for reference)
1. **Orders Service** — skeleton, Domain, EF Core+Postgres, CRUD API,
   integration tests, Serilog, Dockerfile
2. **Inventory Service** — same structure, stock reservation logic
3. **Async communication** — RabbitMQ in Docker Compose, Orders publishes
   `OrderCreated`, Inventory consumes it, out-of-stock handling, Correlation ID
4. **Notifications Service** — listens to events, mock notification sending
5. **Bringing it together** — single `docker compose up`, health checks,
   end-to-end manual test
6. **CI/CD and code quality** — GitHub Actions (build/test/docker),
   dotnet format, branch protection
7. **Documentation** — Mermaid diagram, ADRs, final README

## Open questions
None at the moment.
