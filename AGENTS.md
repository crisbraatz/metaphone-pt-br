# AGENTS.md

This project is managed by **AI Workflow**. These instructions apply to every AI agent (Claude Code, Codex, others).

## Start here
1. Read `.aiw/state.json`. If `currentFeature` is not null, a feature is in progress: resume it instead of starting new work (Claude Code: `/aiw:resume`; Codex: `/prompts:aiw-resume`).
2. Read `docs/architecture.md` and `docs/business/overview.md` before planning anything.
3. Load the skills in `.aiw/skills/level2/` (stack) and `.aiw/skills/level3/` (business) relevant to the task.

## Project
- Summary: MetaphonePtBr is an open-source (MIT) .NET library published on NuGet that converts a single Brazilian Portuguese word into a stable phonetic token for fuzzy name/address/brand search. This is a full rewrite as version 3.0.0 (current published: 2.0.14) with intentional breaking changes and no obsolete shims. Phonetic rules start from the legacy README (https://github.com/crisbraatz/MetaphonePtBrOld) and may be corrected. Success: full rule test coverage, automated NuGet publishing, Stryker.NET mutation score >= 90%, benchmarks at least as fast as 2.x, complete docs with a 2.x migration guide.
- Stack: .NET SDK 10.0.404 (LTS, `global.json`), C# 14, targets `netstandard2.0` + `net10.0`, PolySharp 1.17.1, xUnit v3 4.0.3, Stryker.NET 5.1.0, BenchmarkDotNet 0.15.8, SourceLink 11.0.100, DotNet.ReproducibleBuilds 2.0.5, GitHub Actions (NuGet Trusted Publishing).
- Architecture: single-assembly layered library — public API (`Metaphone`) -> `Normalization` -> `Rules` (internal).
- User language: pt-BR (talk to the user and write business docs and Level 3 skills in it; code and technical docs in English).

## Commands
- Install: `dotnet tool restore && dotnet restore`
- Run: `dotnet run -c Release --project benchmarks/MetaphonePtBr.Benchmarks` (library; benchmarks are the runnable entry point)
- Test: `dotnet test` and `dotnet stryker --config-file tests/MetaphonePtBr.Tests/stryker-config.json --solution MetaphonePtBr.slnx` (from the repo root)
- Lint/format: `dotnet format --verify-no-changes`
- Build: `dotnet build -c Release` / `dotnet pack src/MetaphonePtBr -c Release -o artifacts`

## Rules
- Follow the architecture and conventions in `docs/architecture.md`; record new significant decisions as ADRs there.
- Every change ships with tests; format, build, tests and mutation score (>= 90%) must pass before committing. Warnings are errors.
- Public API changes are deliberate: keep the surface minimal, document every public member (XML docs), update README, CHANGELOG and the migration guide.
- Never commit secrets; publishing uses NuGet Trusted Publishing (no API keys in the repo).
- One commit per feature (`feat(<scope>): <summary>`); intermediate state lives in `.aiw/current/`.
- Branch mode is stored in `.aiw/state.json` (`main` or `branch`), and so is the trunk branch (`trunkBranch`). Never commit project work to any other branch.
