using BenchmarkDotNet.Attributes;

using MetaphonePtBr.Normalization;

namespace MetaphonePtBr.Benchmarks;

[MemoryDiagnoser]
public class NormalizationBenchmarks
{
    [Params("Cristopher", "ação", "paralelepipedo")]
    public string Word { get; set; } = string.Empty;

    [Benchmark]
    public string? TryNormalize()
    {
        _ = WordNormalizer.TryNormalize(Word, out string? normalized);
        return normalized;
    }
}