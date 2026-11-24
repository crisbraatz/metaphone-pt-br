<p align="center">
  <img src="https://raw.githubusercontent.com/crisbraatz/metaphone-pt-br/main/assets/brand/logo.png" alt="MetaphonePtBr" width="520">
</p>

<p align="center">
  <a href="https://www.nuget.org/packages/MetaphonePtBr"><img src="https://img.shields.io/nuget/v/MetaphonePtBr" alt="NuGet"></a>
  <a href="https://www.nuget.org/packages/MetaphonePtBr"><img src="https://img.shields.io/nuget/dt/MetaphonePtBr" alt="Downloads"></a>
  <a href="https://github.com/crisbraatz/metaphone-pt-br/actions/workflows/ci.yml"><img src="https://github.com/crisbraatz/metaphone-pt-br/actions/workflows/ci.yml/badge.svg" alt="CI"></a>
  <a href="LICENSE"><img src="https://img.shields.io/github/license/crisbraatz/metaphone-pt-br" alt="License"></a>
</p>

Brazilian Portuguese phonetic matching for .NET. `MetaphonePtBr` converts a single word into a stable phonetic token for name, address, brand and fuzzy search: `Christopher`, `Cristofer` and `Cristophr` all become `KRSTF`.

> **Status:** version 3.0.0 is a full rewrite and is under active development. The latest stable release is 2.0.14.

## Install

```bash
dotnet add package MetaphonePtBr
```

Targets `netstandard2.0` (including .NET Framework 4.6.1+) and `net10.0`.

## Input contract

- A single word containing letters only.
- Supported accented letters: `ÁÀÂÃÉÊÍÓÔÕÚÜÇ` and their lowercase forms, precomposed or as combining marks.
- Digits, spaces, symbols, hyphens, apostrophes, ligatures, ordinal indicators and non PT-BR letters are rejected.

## Development

Requires the .NET SDK pinned in [`global.json`](global.json).

```bash
dotnet tool restore
dotnet build
dotnet test
dotnet format --verify-no-changes
cd tests/MetaphonePtBr.Tests && dotnet stryker          # mutation testing (break threshold 90%)
dotnet run -c Release --project benchmarks/MetaphonePtBr.Benchmarks   # benchmarks
dotnet pack src/MetaphonePtBr -c Release -o artifacts    # NuGet package
```

Benchmark results against 2.0.14 are in [docs/benchmarks.md](docs/benchmarks.md).

## Releasing

Push a `vX.Y.Z` tag. The [release workflow](.github/workflows/release.yml) builds, tests, packs and publishes to NuGet.org using Trusted Publishing, then creates a GitHub Release. One-time setup: create a Trusted Publishing policy on nuget.org for this repository and workflow, a GitHub environment named `nuget`, and an environment secret `NUGET_USER` with your nuget.org profile name.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md), the [Code of Conduct](CODE_OF_CONDUCT.md) and the [Security Policy](SECURITY.md). Changes are listed in the [CHANGELOG](CHANGELOG.md).

## Credits

Based on [metaphone-ptbr](https://github.com/carlosjordao/metaphone-ptbr) by [Carlos Costa Jordão](https://github.com/carlosjordao).

## License

[MIT](LICENSE)
