---
name: dotnet-library
description: Use when writing or packaging the MetaphonePtBr C# library (multi-targeting, NuGet metadata, analyzers, build settings).
level: 2
kind: technology
version: .NET 10 LTS / C# 14
sources:
  - https://learn.microsoft.com/en-us/dotnet/standard/library-guidance/nuget — Microsoft Learn
  - https://learn.microsoft.com/en-us/dotnet/standard/library-guidance/cross-platform-targeting — Microsoft Learn
  - model-knowledge
fetched: 2026-11-24
---

# .NET library (NuGet) guidance

## Targeting
- Target `netstandard2.0;net10.0`. Microsoft: DO include a modern target (net10.0 LTS); CONSIDER netstandard2.0 for .NET Framework reach; AVOID netstandard1.x and netstandard2.1 when 2.0 + modern target exists.
- Use `#if NET` for modern-only fast paths (`Span<char>`, `string.Create`, `SearchValues<char>`, `stackalloc`). Keep behavior identical across targets; tests prove it.
- PolySharp (PrivateAssets=all) for language polyfills on netstandard2.0. Never add runtime dependencies for polyfills that ship as packages unless unavoidable.
- Mark `IsAotCompatible`/`IsTrimmable` true for the net10.0 target.

## Package metadata (every package on NuGet.org)
PackageId, Version, Title, Description, Authors, PackageTags, PackageIcon (png 128x128+, packed), PackageReadmeFile (packed), PackageProjectUrl, RepositoryUrl, PackageLicenseExpression (`MIT`), PackageReleaseNotes (link to CHANGELOG), Copyright.

## Build quality
- `Directory.Build.props`: Nullable, TreatWarningsAsErrors, AnalysisLevel latest-recommended, EnforceCodeStyleInBuild, GenerateDocumentationFile (public API XML docs mandatory), Deterministic, ContinuousIntegrationBuild in CI.
- SourceLink (Microsoft.SourceLink.GitHub) with embedded PDBs (DotNet.ReproducibleBuilds sets `DebugType=embedded`), so no `.snupkg` is produced; `EmbedUntrackedSources`.
- `EnablePackageValidation=true`; after 3.0.0 ships set `PackageValidationBaselineVersion` to catch accidental breaking changes.
- Central Package Management (`Directory.Packages.props`), `RestorePackagesWithLockFile`, `NuGetAudit` mode all.
- Strong-naming is not required for new libraries on modern .NET; keep the 2.x decision consistent.

## API design
- Minimal public surface; everything else `internal` (`InternalsVisibleTo` for tests/benchmarks).
- Throwing method + `Try` pattern pair. Validate arguments with `ArgumentNullException.ThrowIfNull` (polyfill on netstandard2.0).
- No static mutable state; thread-safe by construction.
- SemVer: breaking change → major. Document in CHANGELOG (Keep a Changelog).

## Commands
`dotnet restore` · `dotnet build -c Release` · `dotnet test` · `dotnet format --verify-no-changes` · `dotnet pack -c Release -o artifacts`

## Avoid
Regex/LINQ in hot paths, culture-sensitive APIs (`ToUpper()` without invariant), public mutable types, nuspec files, committing API keys.
