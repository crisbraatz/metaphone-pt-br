# Benchmarks: 3.0.0 against 2.0.14

This page records how MetaphonePtBr 3.0.0 performs against the published 2.0.14 release. 2.0.14 is the baseline: the 3.0.0 `Metaphone.Encode` must be at least as fast as 2.0.14 and must not allocate more, for every input category.

## Machine and environment

| Item | Value |
|------|-------|
| Date | 2026-11-24 |
| CPU | Apple M4, 1 CPU, 10 logical and 10 physical cores |
| OS | macOS 27.2.1 (26B131), Darwin 27.2.0 |
| .NET SDK | 10.0.404 |
| .NET runtime | .NET 10.0.15 (Arm64 RyuJIT armv8.0-a) |
| BenchmarkDotNet | 0.15.8 |
| Job | Default (`--job default`), idle machine |

## How to run

Locally:

```bash
dotnet run -c Release --project benchmarks/MetaphonePtBr.Benchmarks -- --filter '*EncodeComparison*'
```

On demand in CI: open Actions > Benchmarks > Run workflow and pick the `job` input (`default` or `short`). The workflow publishes the report as the job summary and as the `benchmark-results` artifact.

## How 2.0.14 is loaded side by side

Both versions run in the same process:

- 2.0.14 is declared with `PackageDownload` (not `PackageReference`), so it does not clash with the 3.0.0 project reference of the same package id and assembly name. The 2.0.14 DLL is embedded as the resource `legacy/MetaphonePtBr.dll`.
- `LegacyMetaphone` verifies the SHA-256 of the embedded bytes and the assembly version before loading anything, then loads the DLL into an isolated `AssemblyLoadContext`.
- Both encoders are exposed as `Func<string, string>` delegates, so the call overhead is the same for both sides.

## Input categories

| Category | Words |
|----------|-------|
| Short | 4 letters or fewer (for example `Ana`, `Rui`, `Leo`) |
| Medium | 5 to 8 letters (for example `Cristina`, `Ricardo`, `Juliana`) |
| Long | 12 letters or more (for example `paralelepipedo`, `responsabilidade`) |
| Accented | Words with Portuguese accents or cedilla (for example `ação`, `João`, `Árvore`) |
| OracleSample | Every 9th word of the 2.0.14 oracle fixture |

Only words that both encoders accept are measured, so every case compares the same inputs. Inputs are built in `GlobalSetup` and are not part of the measurement.

## Results

Columns: Category, Method (`Legacy_2_0_14` is the baseline), Mean, Ratio (Current / Legacy), Allocated, Alloc Ratio (Current / Legacy).

```

BenchmarkDotNet v0.15.8, macOS 27.2.1 (26B131) [Darwin 27.2.0]
Apple M4, 1 CPU, 10 logical and 10 physical cores
.NET SDK 10.0.404
  [Host]     : .NET 10.0.15 (10.0.15, 10.0.1526.53008), Arm64 RyuJIT armv8.0-a
  DefaultJob : .NET 10.0.15 (10.0.15, 10.0.1526.53008), Arm64 RyuJIT armv8.0-a


```
| Method        | Category     | Mean        | Error    | StdDev   | Ratio | Gen0    | Allocated | Alloc Ratio |
|-------------- |------------- |------------:|---------:|---------:|------:|--------:|----------:|------------:|
| **Legacy_2_0_14** | **Short**        |    **621.1 ns** |  **1.03 ns** |  **0.86 ns** |  **1.00** |  **0.3929** |   **3.21 KB** |        **1.00** |
| Current_3_0_0 | Short        |    354.3 ns |  0.91 ns |  0.85 ns |  0.57 |  0.1230 |   1.01 KB |        0.31 |
|               |              |             |          |          |       |         |           |             |
| **Legacy_2_0_14** | **Medium**       |    **748.1 ns** |  **1.59 ns** |  **1.41 ns** |  **1.00** |  **0.4406** |    **3.6 KB** |        **1.00** |
| Current_3_0_0 | Medium       |    579.0 ns |  1.67 ns |  1.39 ns |  0.77 |  0.1602 |   1.31 KB |        0.36 |
|               |              |             |          |          |       |         |           |             |
| **Legacy_2_0_14** | **Long**         |  **1,226.2 ns** |  **1.64 ns** |  **1.37 ns** |  **1.00** |  **0.6123** |   **5.01 KB** |        **1.00** |
| Current_3_0_0 | Long         |    969.5 ns |  1.46 ns |  1.29 ns |  0.79 |  0.2441 |      2 KB |        0.40 |
|               |              |             |          |          |       |         |           |             |
| **Legacy_2_0_14** | **Accented**     |  **1,683.9 ns** |  **7.16 ns** |  **6.69 ns** |  **1.00** |  **0.4749** |   **3.89 KB** |        **1.00** |
| Current_3_0_0 | Accented     |    661.0 ns |  5.15 ns |  4.82 ns |  0.39 |  0.2165 |   1.77 KB |        0.46 |
|               |              |             |          |          |       |         |           |             |
| **Legacy_2_0_14** | **OracleSample** | **24,726.2 ns** | **63.70 ns** | **59.59 ns** |  **1.00** | **11.0168** |  **90.08 KB** |        **1.00** |
| Current_3_0_0 | OracleSample | 15,086.8 ns | 56.84 ns | 53.17 ns |  0.61 |  4.1962 |   34.4 KB |        0.38 |

## Conclusion

In the default job on this machine, 3.0.0 `Metaphone.Encode` is faster than 2.0.14 in every category, with mean ratios between 0.39 (Accented) and 0.79 (Long), and allocates less in every category, with allocation ratios between 0.31 (Short) and 0.46 (Accented). The gap is largest for accented words and for the oracle sample, which is the closest proxy for real-world input. The benchmark targets the 2.0.14 behavior on the same inputs, so the comparison is a like-for-like check of the internals, not of the rule set.
