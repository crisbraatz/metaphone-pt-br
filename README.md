![MetaphonePtBr](https://raw.githubusercontent.com/crisbraatz/metaphone-pt-br/main/assets/brand/logo.png)

[![NuGet](https://img.shields.io/nuget/v/MetaphonePtBr)](https://www.nuget.org/packages/MetaphonePtBr)
[![Downloads](https://img.shields.io/nuget/dt/MetaphonePtBr)](https://www.nuget.org/packages/MetaphonePtBr)
[![CI](https://github.com/crisbraatz/metaphone-pt-br/actions/workflows/ci.yml/badge.svg)](https://github.com/crisbraatz/metaphone-pt-br/actions/workflows/ci.yml)
[![License](https://img.shields.io/github/license/crisbraatz/metaphone-pt-br)](https://github.com/crisbraatz/metaphone-pt-br/blob/main/LICENSE)

MetaphonePtBr is a .NET library that turns a single Brazilian Portuguese word into a stable phonetic token. Words that sound alike share the same token, so names, addresses and brands can be matched even when they are spelled differently or contain typos. For example, `Christopher`, `Cristofer` and `Cristophr` all produce `KRSTF`.

> **Status:** version 3.0.0 is a full rewrite and has not been released yet. The latest stable release is 2.0.14. The 2.x migration notes are in [Migrating from 2.x](#migrating-from-2x).

## Install

```bash
dotnet add package MetaphonePtBr
```

The package targets `netstandard2.0` (including .NET Framework 4.6.1 and later) and `net10.0`.

## Quick start

Add `using MetaphonePtBr;`, then call `Metaphone.Encode` to get the token. It throws when the input is invalid:

```csharp
using MetaphonePtBr;

string token = Metaphone.Encode("Cristopher"); // "KRSTF"
```

If you prefer not to handle exceptions, call `Metaphone.TryEncode`. It returns `false` for invalid input and never throws:

```csharp
using MetaphonePtBr;

if (Metaphone.TryEncode(userInput, out string? token))
{
    // token is the phonetic token for a valid word
}
```

## Matching example

These spelling variations produce the same token, `KRSTF`:

- Christofer
- Christofr
- Christopher
- Christophr
- Cristofer
- Cristofr
- Cristophr

Compare tokens to find candidates for a fuzzy match:

```csharp
bool sameSound = Metaphone.Encode("Christopher") == Metaphone.Encode("Cristophr"); // true
```

## Common use cases

- Name search, so that a user finds a customer even when the spelling differs
- Address search, such as street and neighbourhood names
- Product or brand matching
- Typo-tolerant lookup
- Duplicate detection for records whose words sound alike

## Input contract

- The input must be a single word.
- The input must contain letters only.
- Supported accented Brazilian Portuguese letters are `ÁÀÂÃÉÊÍÓÔÕÚÜÇ` and their lowercase forms.
- Accents may be given as precomposed characters or as combining marks.
- Symbols, digits, spaces, hyphens, apostrophes, ligatures, ordinal indicators and letters that are not part of Brazilian Portuguese are rejected.
- The result is a phonetic token, not the original word.

## Errors and exceptions

| Situation | `Metaphone.Encode` | `Metaphone.TryEncode` |
|-----------|--------------------|-----------------------|
| `null` input | Throws `ArgumentNullException` | Returns `false` |
| Input that is not a single supported word | Throws `ArgumentException` | Returns `false` |
| Valid word whose letters are all bypassed (for example `H`, `RR` or `LH`) | Returns an empty token | Returns `true` with an empty token |

`TryEncode` never throws.

## How it works

Each word is processed in two steps:

1. **Normalization.** The word is validated and its case is ignored. Accented letters are folded to their base letters, so the result uses only `A` to `Z` plus `Ç`.
2. **Rule engine.** The normalized word is read from left to right. For each letter, the engine checks the rules in priority order. A rule can look at the current letter, the letters before and after it, and the letters around them. If a letter matches no special rule, it keeps its default sound.

The full rule set is in the [rule reference](#rule-reference) below. The design is described in [docs/architecture.md](https://github.com/crisbraatz/metaphone-pt-br/blob/main/docs/architecture.md).

## Performance

3.0.0 is faster and allocates less than 2.0.14 for every input category measured. In the default BenchmarkDotNet job on an Apple M4, the mean time of `Metaphone.Encode` ranges from 0.39 times (accented words) to 0.79 times (long words) the 2.0.14 time, and allocations range from 0.31 to 0.46 times the 2.0.14 allocations. Oracle sample words, the closest proxy for real input, run in about 0.61 times the time and 0.38 times the allocations.

The full tables, the machine details and the instructions to run the benchmarks are in [docs/benchmarks.md](https://github.com/crisbraatz/metaphone-pt-br/blob/main/docs/benchmarks.md).

## Migrating from 2.x

Version 3.0.0 replaces the `string` extension method with static methods on `Metaphone`. The phonetic tokens do not change, so existing indexes built with 2.x keep matching.

Before (2.x):

```csharp
using MetaphonePtBr;

string token = "Cristopher".GetMetaphoneToken();
```

After (3.0.0):

```csharp
using MetaphonePtBr;

string token = Metaphone.Encode("Cristopher");
```

If your code relied on the extension method not throwing, use `Metaphone.TryEncode`:

```csharp
using MetaphonePtBr;

if (Metaphone.TryEncode(input, out string? token))
{
    // use token
}
```

## Target frameworks

- `netstandard2.0`, for .NET Framework 4.6.1 and later
- `net10.0`

The library is marked trimmable and AOT compatible on `net10.0`.

## Development

Requires the .NET SDK pinned in [`global.json`](https://github.com/crisbraatz/metaphone-pt-br/blob/main/global.json).

```bash
dotnet tool restore
dotnet build -c Release
dotnet test
dotnet format --verify-no-changes
dotnet stryker --config-file tests/MetaphonePtBr.Tests/stryker-config.json --solution MetaphonePtBr.slnx   # mutation testing, break threshold 90%
dotnet run -c Release --project benchmarks/MetaphonePtBr.Benchmarks   # benchmarks
dotnet pack src/MetaphonePtBr -c Release -o artifacts   # NuGet package
```

## Releasing

Push a `vX.Y.Z` tag. The [release workflow](https://github.com/crisbraatz/metaphone-pt-br/blob/main/.github/workflows/release.yml) builds, tests, packs and publishes to NuGet.org with Trusted Publishing, then creates a GitHub Release. The one-time setup is a Trusted Publishing policy on nuget.org for this repository and workflow, a GitHub environment named `nuget`, and an environment secret `NUGET_USER` with your nuget.org profile name.

## Contributing

See [CONTRIBUTING.md](https://github.com/crisbraatz/metaphone-pt-br/blob/main/CONTRIBUTING.md), the [Code of Conduct](https://github.com/crisbraatz/metaphone-pt-br/blob/main/CODE_OF_CONDUCT.md) and the [Security Policy](https://github.com/crisbraatz/metaphone-pt-br/blob/main/SECURITY.md). Changes are listed in the [CHANGELOG](https://github.com/crisbraatz/metaphone-pt-br/blob/main/CHANGELOG.md).

## Rule reference

Each table lists a pattern and the token it produces. The symbols used in the patterns are explained first. The rules match the 2.0.14 behavior.

### Symbols

| Symbol | Meaning                             |
|:------:|:------------------------------------|
| Letter | Letter                              |
|  `^`   | Beginning of word                   |
|  `$`   | End of word                         |
|  `[]`  | One occurrence of any listed letter |
|  `v`   | Any vowel                           |
|  `c`   | Any consonant                       |
|  `.`   | Any letter                          |
|  `0`   | Bypass                              |

### C

| Rule  | Result |
|:-----:|:-------|
|  CHR  | K      |
|  CH   | X      |
| C[EI] | S      |
| C[QK] | 0      |
|   C   | K      |
|  C$   | K      |
|   Ç   | S      |

### G

| Rule  | Result |
|:-----:|:-------|
| GH[v] | J      |
| GH[c] | GJ     |
| G[EI] | J      |
|   G   | G      |

### H

| Rule  | Result |
|:-----:|:-------|
| ^H[v] | v      |
|   H   | 0      |

### Immutables

| Rule | Result |
|:----:|:-------|
|  B   | B      |
|  D   | D      |
|  F   | F      |
|  J   | J      |
|  K   | K      |
|  M   | M      |
|  V   | V      |

### L

| Rule | Result |
|:----:|:-------|
|  LH  | 0      |
|  ^L  | L      |
| L[v] | L      |
|  L   | 0      |

### N

| Rule | Result |
|:----:|:-------|
|  N$  | M      |
|  NH  | 0      |
|  NN  | 0      |
|  N   | N      |

### P

| Rule | Result |
|:----:|:-------|
|  PH  | F      |
|  P   | P      |

### Q

| Rule | Result |
|:----:|:-------|
|  Q   | K      |

### R

| Rule | Result |
|:----:|:-------|
|  ^R  | 0      |
|  R$  | 0      |
|  RR  | 0      |
| vRv  | R      |
| .Rc  | R      |
| cRv  | R      |

### S

|  Rule   | Result |
|:-------:|:-------|
|   SS    | S      |
|   SH    | X      |
|   vSv   | Z      |
| SC[EI]  | S      |
| SC[AOU] | SK     |
|   SCH   | X      |
|  SC[.]  | S      |
|    S    | S      |

### T

| Rule | Result |
|:----:|:-------|
|  TH  | T      |
|  T   | T      |

### Vowels

| Rule | Result |
|:----:|:-------|
| ^[v] | v      |

### W

| Rule  | Result |
|:-----:|:-------|
| W[v]  | V      |
| W[LR] | V      |
| W[c]  | 0      |
|  W$   | 0      |

### X

|        Rule         | Result |
|:-------------------:|:-------|
|         X$          | X      |
|       ^EX[v]        | Z      |
|       .EX[EI]       | X      |
|      .EX[AOU]       | KS     |
|        EX[C]        | S      |
|       EX[PT]        | S      |
|        EX[.]        | KS     |
|  [vCKGLRX][AIOU]X   | X      |
| [DFMNPQSTVZ][AIOU]X | KS     |
|          X          | X      |

### Y

| Rule | Result |
|:----:|:-------|
|  Y   | I      |

### Z

| Rule | Result |
|:----:|:-------|
|  Z$  | S      |
|  Z   | Z      |


## Credits

Based on [metaphone-ptbr](https://github.com/carlosjordao/metaphone-ptbr) by [Carlos Costa Jordão](https://github.com/carlosjordao).

## License

[MIT](https://github.com/crisbraatz/metaphone-pt-br/blob/main/LICENSE)
