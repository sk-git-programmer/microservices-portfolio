# PROGRESS — microservices-portfolio

Last updated: 2026-08-04

## Overall status

Orders Service in progress. Solution skeleton, health endpoint, and EF Core
with SQL Server provider set up. No domain entities or database migrations yet.

## Done

- [x] GitHub repo created: microservices-portfolio (public, README, .gitignore
      Visual Studio template, MIT License, Topics added)
- [x] Full work plan defined (7 phases, 26 steps — see "Phase plan" below)
- [x] Solution structure: MicroservicesPortfolio.sln with Orders.Api,
      Orders.Domain, Orders.Infrastructure, Orders.Tests (Clean Architecture,
      project references wired up)
- [x] GET /health endpoint in Orders.Api
- [x] EF Core configured with SQL Server provider (Microsoft.EntityFrameworkCore.SqlServer
      8.0.11, explicit version pinned for net8.0 compatibility)
- [x] OrdersDbContext skeleton (no entities yet), registered in DI
- [x] Connection string split: placeholder in appsettings.json, local
      Windows Authentication connection in appsettings.Development.json
- [x] Branch protection on main: pull request required before merging

## In progress

- [ ] Step 1: Orders Service — domain entity, first migration, CRUD API

## Next step

Domain entity (Phase 1, Step 4):

- Order entity (and OrderItem) in Orders.Domain
- First EF Core migration
- Verify table creation against local SQL Server instance

## Architecture decisions (summary)

- RabbitMQ.Client used directly (no MassTransit) — deliberate choice, to
  understand the underlying mechanics before reaching for an abstraction
- Database-per-service (SQL Server) — each microservice owns its own database
- Simplified Clean Architecture: Domain (pure business logic, no EF/ASP.NET
  dependency) / Infrastructure (EF Core, data access) / Api (HTTP layer)
- CI/CD added only after the code works locally (not from the start)
- Connection strings kept out of appsettings.json (placeholder only);
  environment-specific values live in appsettings.Development.json for now,
  revisited when moving to Docker (Phase 5)

## Phase plan (full, for reference)

1. **Orders Service** — skeleton, Domain, EF Core+SQL Server, CRUD API,
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
