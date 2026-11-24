---
name: architecture-layered-library
description: Use when deciding where code goes in MetaphonePtBr (public API, normalization, rule engine) and what may depend on what.
level: 2
kind: architecture
version: Framework Design Guidelines (3rd ed.) / .NET library guidance
sources:
  - https://learn.microsoft.com/en-us/dotnet/standard/library-guidance/ — Microsoft Learn
  - https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/ — Microsoft Learn (Framework Design Guidelines)
  - model-knowledge
fetched: 2026-11-24
---

# Layered single-assembly library

## Layers (src/MetaphonePtBr)
| Layer | Folder | Visibility | Responsibility |
| :--- | :--- | :--- | :--- |
| Public API | `Metaphone.cs` | public | `Encode`, `TryEncode` (+ span overloads on net10.0). Argument checks, delegates inward. |
| Normalization | `Normalization/` | internal | Validate input contract, uppercase invariant, fold combining accents, produce canonical char buffer. |
| Rule engine | `Rules/` | internal | Context window (prev2, prev, cur, next, next2), one rule type per letter group, priority order, emit sound + consumed length. |

## Dependency rules
- API → Normalization → Rules. Never the reverse; rules never throw for user input (normalization already validated).
- No I/O, no statics with mutable state, no reflection, no allocations per letter.
- Public types are few and sealed/static; adding public API is a deliberate, documented decision (Package Validation guards it).

## Testing per layer
- Rules: table-driven theories per rule row of the rule reference (positive and boundary cases).
- Normalization: valid accents (precomposed and combining), every rejected class (digits, spaces, hyphen, apostrophe, ligatures, ordinals, non PT-BR letters), null/empty.
- API: end-to-end words, Encode/TryEncode parity, exceptions.
- Mutation testing covers all layers.

## Example
`Metaphone.Encode("Christopher")` → Normalizer yields `CHRISTOPHER` → engine: `CHR`→K, R consumed, I vowel dropped (not initial), S→S, T→T, `PH`→F, E dropped, `R$`→ (rule) → `KRSTF`.

## Anti-patterns
God switch statement in the API class; regex-based rules; leaking `internal` rule types publicly; tests coupled to private implementation details instead of rule behavior.
