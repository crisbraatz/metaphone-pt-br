# Contributing to MetaphonePtBr

Thanks for your interest in improving MetaphonePtBr! Issues and pull requests are welcome.

## Ground rules
- Be respectful: this project follows the [Code of Conduct](CODE_OF_CONDUCT.md).
- Report security problems privately, as described in [SECURITY.md](SECURITY.md).
- For larger changes (new rules, API changes), open an issue first so we can agree on the approach.

## Development setup
1. Install the .NET SDK version pinned in [`global.json`](global.json) (currently 10.0.x LTS).
2. Restore tools and packages:
   ```bash
   dotnet tool restore
   dotnet restore
   ```

## Before opening a pull request
All of these must pass (CI runs them too):

```bash
dotnet format --verify-no-changes
dotnet build -c Release
dotnet test -c Release
cd tests/MetaphonePtBr.Tests && dotnet stryker
```

- Warnings are errors (`TreatWarningsAsErrors`); fix them instead of suppressing.
- Every behavior change needs tests. Phonetic rule changes need a test per rule row and an update to the rule reference in the README.
- Mutation score must stay at or above 90%.
- Performance-sensitive changes: run the benchmarks (`dotnet run -c Release --project benchmarks/MetaphonePtBr.Benchmarks`) and include the results.
- Add an entry to [CHANGELOG.md](CHANGELOG.md) under `Unreleased`.

## Commit messages
Use [Conventional Commits](https://www.conventionalcommits.org/): `feat(rules): handle SCH`, `fix(normalization): reject ordinal indicators`, `docs: ...`, `chore: ...`.

## Releases
Maintainers release by pushing a `vX.Y.Z` tag; CI builds, tests, packs and publishes to NuGet.org.
