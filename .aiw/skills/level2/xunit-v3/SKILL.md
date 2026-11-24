---
name: xunit-v3
description: Use when writing or running unit tests with xUnit.net v3 in this repo.
level: 2
kind: technology
version: xunit.v3 4.0.3
sources:
  - https://xunit.net/docs/getting-started/v3/getting-started — xUnit.net
  - model-knowledge
fetched: 2026-11-24
---

# xUnit.net v3

- Test projects are executables (`OutputType=Exe`) referencing only `xunit.v3`; `dotnet test` uses Microsoft.Testing.Platform (opt-in via `"test": {"runner": "Microsoft.Testing.Platform"}` in `global.json`; VSTest mode is not supported by MTP on the .NET 10 SDK). Supported: .NET 8+ and .NET Framework 4.7.2+ (Framework officially only on Windows).
- Target `net10.0` always; add `net481` only when building on Windows (condition on `$([MSBuild]::IsOSPlatform('Windows'))`) to exercise the netstandard2.0 asset.
- Prefer `[Theory]` + `[InlineData]` / `TheoryData<T1,T2>` for rule tables; one assertion concept per test; descriptive names `Method_Condition_Expected`.
- Use `Assert.Throws<T>` for exceptions, `Assert.Equal(expected, actual)` order.
- Use `TestContext.Current.CancellationToken` when needed; no shared mutable static state.
- Run: `dotnet test` (or `dotnet run --project tests/...` for the native runner).
- Coverage via `--collect "XPlat Code Coverage"` is optional; mutation score (Stryker) is the real gate.
