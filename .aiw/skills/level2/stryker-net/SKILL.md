---
name: stryker-net
description: Use when configuring or running Stryker.NET mutation testing or raising the mutation score.
level: 2
kind: technology
version: dotnet-stryker 5.1.0
sources:
  - https://stryker-mutator.io/docs/stryker-net/configuration/ — Stryker Mutator
  - model-knowledge
fetched: 2026-11-24
---

# Stryker.NET

- Installed as a local tool: `.config/dotnet-tools.json` → `dotnet tool restore`, then `dotnet stryker`.
- Config file `stryker-config.json` (root `stryker-config` section) committed to version control. Run from the test project folder, or pass `--config-file`. `project` = project-under-test file name (no path) when the test project has several references; `solution` optional for .NET (Core).
- Thresholds: `high` 95, `low` 90, `break` 90 — run fails below break. CI runs it on every PR.
- Mutation level `Standard` (raise to `Advanced` if useful). Reporters: `html`, `progress`, `cleartext`, `markdown` for CI summary.
- Surviving mutants mean missing assertions: add a test that kills them; only ignore (`// Stryker disable once <mutator> : reason`) equivalent mutants, always with a reason.
- Don't mutate generated code or polyfills (`mutate` excludes).
- Use `--since main` locally for faster incremental runs.
