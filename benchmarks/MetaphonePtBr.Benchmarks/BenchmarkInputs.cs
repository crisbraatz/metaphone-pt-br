using System.Reflection;

namespace MetaphonePtBr.Benchmarks;

/// <summary>
/// Word sets used by <see cref="EncodeComparisonBenchmarks"/>, grouped by length and shape.
/// </summary>
// CA1720: the member names Short and Long are the category names the benchmark reports use.
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Category names are part of the benchmark report.")]
public enum InputCategory
{
    /// <summary>Words of at most four letters.</summary>
    Short,

    /// <summary>Words of five to eight letters.</summary>
    Medium,

    /// <summary>Words of at least twelve letters.</summary>
    Long,

    /// <summary>Words with diacritics.</summary>
    Accented,

    /// <summary>Every ninth word of the 2.0.14 oracle fixture.</summary>
    OracleSample,
}

/// <summary>
/// Provides the raw input words for each <see cref="InputCategory"/>.
/// </summary>
public static class BenchmarkInputs
{
    private const string OracleResourceName = "oracle-2.0.14.tsv";
    private const int OracleStep = 9;

    /// <summary>
    /// Returns the raw words of a category. Words that the encoders reject are not filtered here.
    /// </summary>
    /// <param name="category">The category to return.</param>
    /// <returns>The words of the category.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="category"/> is not defined.</exception>
    public static string[] Get(InputCategory category) => category switch
    {
        InputCategory.Short => ["Ana", "Rui", "Eva", "Leo", "Ivo", "Bia", "Gil", "Luz"],
        InputCategory.Medium => ["Cristina", "Marcelo", "Ricardo", "Fernanda", "Gustavo", "Patricia", "Rodrigo", "Juliana"],
        InputCategory.Long => ["paralelepipedo", "inconstitucional", "otorrinolaringologista", "desenvolvimento", "extraordinariamente", "responsabilidade", "internacionalizacao", "oftalmologista"],
        InputCategory.Accented => ["ação", "João", "Conceição", "Gonçalves", "Açúcar", "Pêssego", "Árvore", "Ônibus"],
        InputCategory.OracleSample => ReadOracleSample(),
        _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Unknown input category."),
    };

    private static string[] ReadOracleSample()
    {
        using Stream stream = typeof(BenchmarkInputs).Assembly.GetManifestResourceStream(OracleResourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{OracleResourceName}' was not found.");
        using var reader = new StreamReader(stream);

        var words = new List<string>();
        int line = 0;
        string? text;
        while ((text = reader.ReadLine()) is not null)
        {
            if (line++ % OracleStep != 0)
            {
                continue;
            }

            int tab = text.IndexOf('\t');
            words.Add(tab < 0 ? text : text.Substring(0, tab));
        }

        return [.. words];
    }
}