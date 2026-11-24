using System;
using System.Collections.Generic;
using System.IO;

using MetaphonePtBr;

using Xunit;

namespace MetaphonePtBr.Tests;

public sealed class OracleTests
{
    private const string FixtureResourceName = "oracle-2.0.14.tsv";

    public static TheoryData<string, string> OracleEntries()
    {
        var data = new TheoryData<string, string>();

        foreach (var (word, token) in LoadFixture())
        {
            data.Add(word, token);
        }

        return data;
    }

    [Fact]
    public void Fixture_has_at_least_1500_entries()
    {
        var count = 0;

        foreach (var _ in LoadFixture())
        {
            count++;
        }

        Assert.True(count >= 1500, $"Expected at least 1500 fixture entries, found {count}.");
    }

    [Theory]
    [MemberData(nameof(OracleEntries))]
    public void Encode_matches_oracle_2_0_14(string word, string token)
    {
        Assert.Equal(token, Metaphone.Encode(word));
    }

    private static List<(string Word, string Token)> LoadFixture()
    {
        using var stream = typeof(OracleTests).Assembly.GetManifestResourceStream(FixtureResourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{FixtureResourceName}' was not found.");
        using var reader = new StreamReader(stream, System.Text.Encoding.UTF8);

        var entries = new List<(string Word, string Token)>();
        string? line;

        while ((line = reader.ReadLine()) is not null)
        {
            if (line.Length == 0)
            {
                continue;
            }

            var tab = line.IndexOf('\t');
            if (tab < 0)
            {
                throw new FormatException($"Fixture line has no tab separator: '{line}'.");
            }

            entries.Add((line.Substring(0, tab), line.Substring(tab + 1)));
        }

        return entries;
    }
}