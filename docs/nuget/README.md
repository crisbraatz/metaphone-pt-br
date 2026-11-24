# MetaphonePtBr

![MetaphonePtBr](https://raw.githubusercontent.com/crisbraatz/metaphone-pt-br/main/assets/brand/logo.png)

**Brazilian Portuguese phonetic matching for .NET**

MetaphonePtBr turns a single Brazilian Portuguese word into a stable phonetic token. Words that sound alike share the same token, so names, addresses and brands can be matched even when they are spelled differently or contain typos. For example, `Christopher`, `Cristofer` and `Cristophr` all produce `KRSTF`.

> **Status:** version 3.0.0 is a full rewrite and has not been released yet. The latest stable release is 2.0.14. Migration notes for 2.x users are in the [Migrating from 2.x](#migrating-from-2x) section below.

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

These spelling variations all produce the same token, `KRSTF`: Christofer, Christofr, Christopher, Christophr, Cristofer, Cristofr and Cristophr.

Compare tokens to find candidates for a fuzzy match:

```csharp
bool sameSound = Metaphone.Encode("Christopher") == Metaphone.Encode("Cristophr"); // true
```

## Input contract

- The input must be a single word.
- The input must contain letters only.
- Supported accented Brazilian Portuguese letters are `ÁÀÂÃÉÊÍÓÔÕÚÜÇ` and their lowercase forms.
- Symbols, digits, spaces, hyphens, apostrophes and other non-letter characters are rejected.

## Errors

| Situation | `Metaphone.Encode` | `Metaphone.TryEncode` |
|-----------|--------------------|-----------------------|
| `null` input | Throws `ArgumentNullException` | Returns `false` |
| Input that is not a single supported word | Throws `ArgumentException` | Returns `false` |
| Valid word whose letters are all bypassed (for example `H`, `RR` or `LH`) | Returns an empty token | Returns `true` with an empty token |

`TryEncode` never throws.

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

## Documentation

- [Full README with the rule reference](https://github.com/crisbraatz/metaphone-pt-br#readme)
- [Rule reference](https://github.com/crisbraatz/metaphone-pt-br#-rule-reference)
- [Benchmarks](https://github.com/crisbraatz/metaphone-pt-br/blob/main/docs/benchmarks.md)
- [CHANGELOG](https://github.com/crisbraatz/metaphone-pt-br/blob/main/CHANGELOG.md)

## Credits

Based on [metaphone-ptbr](https://github.com/carlosjordao/metaphone-ptbr) by [Carlos Costa Jordão](https://github.com/carlosjordao).

## License

Released under the [MIT License](https://github.com/crisbraatz/metaphone-pt-br/blob/main/LICENSE).
