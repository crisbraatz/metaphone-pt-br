using BenchmarkDotNet.Attributes;

namespace MetaphonePtBr.Benchmarks;

/// <summary>
/// Compares the published MetaphonePtBr 2.0.14 encoder (baseline) with the current <see cref="Metaphone.Encode"/>.
/// Both sides are invoked through a <see cref="Func{T, TResult}"/> so they pay the same delegate-call overhead.
/// </summary>
// CA1707: benchmark method names encode the library version (Legacy_2_0_14, Current_3_0_0) on purpose, for the report.
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Method names encode the compared library versions in the benchmark report.")]
[MemoryDiagnoser]
public class EncodeComparisonBenchmarks
{
    private Func<string, string> _legacy = null!;
    private Func<string, string> _current = null!;
    private string[] _words = [];

    [Params(
        InputCategory.Short,
        InputCategory.Medium,
        InputCategory.Long,
        InputCategory.Accented,
        InputCategory.OracleSample)]
    public InputCategory Category { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _legacy = LegacyMetaphone.CreateEncoder();
        _current = Metaphone.Encode;
        _words = Accepted(BenchmarkInputs.Get(Category), _legacy);

        if (_words.Length == 0)
        {
            throw new InvalidOperationException($"No words in category '{Category}' are accepted by both encoders.");
        }
    }

    [Benchmark(Baseline = true)]
    public int Legacy_2_0_14()
    {
        int sum = 0;
        foreach (string word in _words)
        {
            sum += _legacy(word).Length;
        }

        return sum;
    }

    [Benchmark]
    public int Current_3_0_0()
    {
        int sum = 0;
        foreach (string word in _words)
        {
            sum += _current(word).Length;
        }

        return sum;
    }

    /// <summary>
    /// Keeps only the words that the current encoder accepts and the legacy encoder does not reject.
    /// </summary>
    private static string[] Accepted(string[] candidates, Func<string, string> legacy)
    {
        var accepted = new List<string>(candidates.Length);
        foreach (string word in candidates)
        {
            if (!Metaphone.TryEncode(word, out _))
            {
                continue;
            }

            try
            {
                _ = legacy(word);
            }
#pragma warning disable CA1031 // The legacy encoder throws arbitrary exceptions on unsupported input; any throw means the word is excluded.
            catch (Exception)
#pragma warning restore CA1031
            {
                continue;
            }

            accepted.Add(word);
        }

        return [.. accepted];
    }
}