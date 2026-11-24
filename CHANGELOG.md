# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Full rewrite for 3.0.0: new project structure targeting `netstandard2.0` and `net10.0`.
- Input normalization that enforces the Brazilian Portuguese input contract.
- New brand: icon and logo.
- Mutation testing (Stryker.NET), benchmarks (BenchmarkDotNet), SourceLink, deterministic builds and package validation.
- `Metaphone.Encode(string)` returns the phonetic token of a single Brazilian Portuguese word and throws on invalid input.
- `Metaphone.TryEncode(string?, out string?)` does the same without throwing.

### Removed
- **Breaking:** `GetMetaphoneToken()` is removed in favor of `Metaphone.Encode` and `Metaphone.TryEncode`.

[Unreleased]: https://github.com/crisbraatz/metaphone-pt-br/compare/v2.0.14...HEAD
