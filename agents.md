# Conventions for coding agents

This document captures existing conventions in the Blazor.Extensions solution.

---
## 1. Current Solution Conventions

### 1.1 Layered Project Structure
- `Blazor.Extensions.Domain` – Core domain types, helpers.
- `Blazor.Extensions.Infrastructure` – Data access (EF Core DbContexts, migrations, persistence setup).
- `Blazor.Extensions.Application` – Application-level services (e.g. `FileService`).
- `Blazor.Extensions.Application.Core` – Cross-cutting primitives / helpers (e.g. `PeriodicTask`, command abstractions).
- `Blazor.Extensions.Presentation` – UI helpers / component-level utilities (e.g. Syncfusion integration helpers).
- `Blazor.Extensions.App.Server` – Blazor Server host (composition root, DI wiring, Identity, localization, endpoints).
- `*.Tests` – One test project per production project (`<Project>.Tests` naming), plus Playwright UI tests & BDD (Reqnroll) in Server tests.

### 1.2 Dependency Injection Registration Pattern
- Each layer exposes a `ServiceCollectionExtensions` static class with an `Add<Layer>Services` method.
- Identity services and custom helpers (e.g. `IdentityRedirectManager`) registered directly in `Program.cs`.

### 1.3 Commands Pattern
- Commands derive from a `CommandBase` enabling UI-triggered async operations with cancellation support.
- Conventions (implicit):
  - Provide a user-facing `Label`.
  - Override `OnExecuteAsync(CancellationToken)` for operation body.

### 1.4 Testing Conventions
- Unit/Integration project per layer (`<Layer>.Tests`).
- UI / end‑to‑end: Playwright tests inside `App.Server.Tests` (e.g. `Homepage` test).
- BDD: Reqnroll step definitions (`Steps.cs`) for scenario-driven coverage.
- Test harness spins up real server process (`WebAppRunner`) and real Chromium instance (non‑headless for now).

### 1.5 Security & Identity
- ASP.NET Core Identity integrated with custom user type `ApplicationUser`.
- Multi-factor auth flows implemented (2FA enable, login with 2FA, disable 2FA, recovery codes, etc.).
- `IdentityRedirectManager` centralizes redirect logic and constrains redirects to avoid open redirect issues.

### 1.6 Localization
- Custom `LocalizationHelper.AddLocalizationCookie(HttpContext)` invoked from a root component (`AppComponent`) using a cascading `HttpContext`.
- Culture configuration performed early in `Program.cs` via `SetDefaultAndSupportedCultures`.

### 1.7 UI / Presentation Conventions
- Syncfusion integration wrapped in `SyncfusionHelper.AddBlazorExtension()` + `SyncfusionHelper.Initialize()` at startup.
- Blazor Server interactive components architecture (no WebAssembly head at present).

### 1.8 Naming & File Organization
- Classes generally use PascalCase; internal infrastructure helpers use `internal sealed` where appropriate.
- Redirect / helper classes named verb-noun or responsibility-noun (e.g. `IdentityRedirectManager`).
- Background primitives use functional names: `PeriodicTask` rather than `Scheduler` (suggesting single-purpose minimalism).

### 1.9 Configuration & Options
- Connection strings loaded via `IConfiguration` in Infrastructure registration (`DefaultConnection`).
- No custom `IOptions<T>` classes defined yet for periodic or background work.

### 1.10 Observability (Current Gaps)
- Logging implicitly available through ASP.NET Core DI; explicit custom logging only in Identity-related flows.
- No standardized metric, tracing, or structured event conventions defined yet.

### 1.11 Cancellation & Resilience
- Cancellation tokens passed into async command execution / long running tasks.
- No retry policies, resilience abstractions or Polly integration currently applied.
