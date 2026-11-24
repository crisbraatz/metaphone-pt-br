# Architecture

## Stack
| Layer | Technology | Version | Why |
| :--- | :--- | :--- | :--- |
| Language | C# | 14 | Default language version of the .NET 10 SDK |
| SDK | .NET SDK | 10.0.404 (LTS), pinned in `global.json` | Current LTS; reproducible builds |
| Package targets | `netstandard2.0`, `net10.0` | — | Broad reach (incl. .NET Framework) plus optimized modern path (`Span<T>`, `SearchValues`) |
| Polyfills | PolySharp | 1.17.1 | Modern C# language features on `netstandard2.0` (build-time only, no runtime dependency) |
| Unit tests | xUnit v3 on Microsoft.Testing.Platform | 4.0.3 | Current stable xUnit generation; MTP is the `dotnet test` runner on the .NET 10 SDK (`global.json`) |
| Mutation tests | Stryker.NET (local tool) | 5.1.0 | Mutation score gate (break threshold 90%) |
| Benchmarks | BenchmarkDotNet | 0.15.8 | Compare 3.x against the published 2.0.14 package |
| Packaging | Microsoft.SourceLink.GitHub, DotNet.ReproducibleBuilds, SDK Package Validation | 11.0.100 / 2.0.5 / built-in | Debuggable (embedded PDB + SourceLink), deterministic, API-checked packages |
| CI/CD | GitHub Actions | — | CI on push/PR; publish to NuGet.org on `v*` tags (Trusted Publishing) |

## Architecture style
Single-assembly library with three internal layers and a minimal public surface:

1. **Public API** (`Metaphone` static class): `Encode(string)` / `TryEncode(string, out string)` (plus span overloads on `net10.0`). The only public types.
2. **Normalization** (`internal`): `WordNormalizer` validates the input contract (single word, PT-BR letters only) and produces an NFC uppercase word; `AccentFolder` reduces it to A–Z plus Ç (as 2.0.14 does) before the rules run.
3. **Rule engine** (`internal`): left-to-right scan with a context window (previous², previous, current, next, next²); one rule class per letter group, evaluated by priority; each rule returns the emitted sound and how many letters it consumed.

Dependency rule: API → Normalization → Rules. Rules never depend on the API; nothing depends on I/O. DDD/Clean/Hexagonal are intentionally not used: a pure, I/O-free library gains nothing from them.

## Folder structure
```
.
├── assets/brand/              # icon.svg/png, logo.svg/png, metaphoneptbr.png
├── benchmarks/MetaphonePtBr.Benchmarks/
├── docs/                      # business/, architecture.md, migration guide
├── src/MetaphonePtBr/
│   ├── Metaphone.cs           # public API
│   ├── Normalization/
│   └── Rules/                 # one file per letter group
├── tests/MetaphonePtBr.Tests/
├── .config/dotnet-tools.json  # dotnet-stryker
├── .editorconfig              # Microsoft .NET default conventions
├── Directory.Build.props      # shared props: TreatWarningsAsErrors, analyzers, nullable
├── Directory.Packages.props   # central package management
├── global.json
├── stryker-config.json
├── CHANGELOG.md  CONTRIBUTING.md  CODE_OF_CONDUCT.md  SECURITY.md  LICENSE
└── MetaphonePtBr.slnx
```

## Conventions
- `Directory.Build.props`: `Nullable=enable`, `TreatWarningsAsErrors=true`, `AnalysisLevel=latest-recommended`, `EnforceCodeStyleInBuild=true`, `ImplicitUsings=enable`, `LangVersion=latest`.
- `.editorconfig` based on the Microsoft .NET default code-style conventions; `dotnet format --verify-no-changes` gates CI.
- Invalid input: `Encode` throws `ArgumentException` (with `ArgumentNullException` for null); `TryEncode` never throws.
- Hot path is allocation-light; no LINQ or regex in the rule engine.
- Versioning: SemVer; `Version` set in `Directory.Build.props` and overridden from the git tag in CD. Changelog follows Keep a Changelog.
- Conventional Commits.

## Testing strategy
- Unit tests per rule (table-driven `[Theory]`), per normalization case, plus end-to-end words from the README.
- Tests run on `net10.0` everywhere and on `net481` on the Windows CI runner (covers the `netstandard2.0` asset).
- Mutation testing: `dotnet stryker` with `thresholds.break = 90`.
- Benchmarks: run manually or in a scheduled workflow; not a PR gate.
- Commands: `dotnet build`, `dotnet test`, `dotnet format --verify-no-changes`, `dotnet tool run dotnet-stryker`.

## Security
- No I/O, no network, no secrets at runtime. Input is validated against an allow-list of PT-BR letters before any rule runs.
- Supply chain: central package versions, `NuGetAudit` (all, warnings as errors), lock files, Dependabot, osv-scanner/gitleaks in CI.
- Publishing via NuGet Trusted Publishing (OIDC); no long-lived API key in the repo.

## Deployment
Not a service. Release = push a `vX.Y.Z` tag → CD builds, tests, packs (`.nupkg` with embedded PDB) and pushes to NuGet.org, then creates a GitHub Release with the changelog section.

## ADR-001 — Initial stack and architecture
- Status: accepted
- Context: Full rewrite of MetaphonePtBr (currently 2.0.14) as 3.0.0 with intentional breaking changes, high quality bar (mutation testing, benchmarks) and NuGet best practices.
- Decision: C# 14 / .NET 10 LTS SDK; multi-target `netstandard2.0` + `net10.0`; single assembly with public API → normalization → rule engine layers; xUnit v3, Stryker.NET, BenchmarkDotNet; GitHub Actions with Trusted Publishing; `Directory.Build.props` with `TreatWarningsAsErrors`; Microsoft default `.editorconfig`; standard open-source community files.
- Consequences: Broad compatibility at the cost of polyfills and conditional code; strict build catches issues early but every warning must be resolved; no obsolete shims, so consumers must follow the migration guide.

## ADR-002 — Legacy 2.0.14 output as the behavioral oracle
- Status: accepted
- Context: The legacy README rule tables are ambiguous in places (priority, vowels after the first letter). The 3.0.0 rewrite must not silently change tokens.
- Decision: A fixture (`tests/MetaphonePtBr.Tests/Fixtures/oracle-2.0.14.tsv`, ~1800 words) generated once from the published 2.0.14 package is embedded in the tests; `Metaphone.Encode` must match every row. Future rule corrections update the fixture deliberately in their own feature and are documented in the CHANGELOG.
- Consequences: Byte-for-byte token compatibility with 2.0.14 is guaranteed for the corpus; rule changes become explicit, reviewable diffs of the fixture.

## ADR-003 — Side-by-side benchmarks against 2.0.14
- Status: accepted
- Context: 3.0.0 must be at least as fast as 2.x and must not allocate more (success criteria in `AGENTS.md`). Comparing against a published number is not enough: the comparison must run on the same machine, in the same process, with the same inputs. The 2.0.14 package has the same package id and assembly name as the 3.0.0 project, so a normal reference to both is impossible.
- Decision:
  - 2.0.14 is obtained with `PackageDownload Include="MetaphonePtBr" Version="[2.0.14]"`, which is not recorded in `packages.lock.json`, and is embedded as the resource `legacy/MetaphonePtBr.dll`. A `Target` fails the build when `NuGetPackageRoot` is empty or the DLL is missing.
  - `LegacyMetaphone` checks the SHA-256 of the embedded bytes and the assembly version before loading the DLL into an isolated, non-collectible `AssemblyLoadContext`. Both encoders are called through `Func<string, string>` delegates.
  - BenchmarkDotNet generates and restores its own project (`BenchmarkDotNet.Autogenerated.csproj`) that imports the root `Directory.Build.props`. It has no lock file, so with `CI=true` it would fail under locked mode (NU1004). `Directory.Build.props` therefore sets `RestorePackagesWithLockFile` and `RestoreLockedMode` only for projects other than `BenchmarkDotNet.Autogenerated`. For that generated project only, `TreatWarningsAsErrors` is false and `NuGetAuditLevel` is `moderate`, so a low-severity advisory on a transitive BenchmarkDotNet dependency does not break the generated wrapper. The benchmark code itself is still compiled with warnings as errors and audited at low severity.
  - The benchmarks run on demand through the `benchmarks.yml` workflow (`workflow_dispatch` only, `job` input `default` or `short`). The `Dry` job is not offered: it is a harness check, not a measurement.
  - Rejected: an extern alias for the 2.0.14 assembly. It needs both assemblies, which share one assembly name, referenced in the same compilation under different aliases; that is fragile when one side is a project reference and the other a package of the same id, and it would tie the benchmark project to build-time resolution details of the 2.0.14 package.
- Consequences:
  - The baseline is reproducible on any machine that restores `PackageDownload`, and the benchmark does not change the 3.0.0 public API.
  - Because `PackageDownload` is not locked, integrity is enforced by the SHA-256 constant in `LegacyMetaphone`. Updating the baseline means changing that constant deliberately, in its own change.
  - The generated BenchmarkDotNet project accepts a weaker warnings and audit policy than the rest of the solution. This is limited to that generated project and documented here so it is not relaxed further by accident.
  - Results depend on the machine. `docs/benchmarks.md` records the machine, date and BenchmarkDotNet version next to every result table.
