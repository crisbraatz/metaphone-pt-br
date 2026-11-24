using System.Reflection;
using System.Runtime.Loader;
using System.Security.Cryptography;

namespace MetaphonePtBr.Benchmarks;

/// <summary>
/// Provides the published MetaphonePtBr 2.0.14 encoder next to the 3.0.0 project reference.
/// The 2.0.14 assembly is embedded (see the benchmarks csproj), verified by SHA-256 and loaded
/// into its own <see cref="AssemblyLoadContext"/>, so it does not clash with the 3.0.0 assembly
/// that has the same identity.
/// </summary>
public static class LegacyMetaphone
{
    // SHA-256 of lib/netstandard2.0/MetaphonePtBr.dll from the MetaphonePtBr 2.0.14 NuGet package.
    // Source: ~/.nuget/packages/metaphoneptbr/2.0.14/lib/netstandard2.0/MetaphonePtBr.dll, placed there by
    // `dotnet restore` through PackageDownload (NuGet signature and hash checked at download).
    // Computed on 2026-11-24 with `shasum -a 256`. Update only together with the package version.
    internal const string ExpectedSha256 = "daa6fce8ab34ab432a90db1d08cbd579d1ac943c1ea2b36ced26a233011ebb01";

    private const string ResourceName = "legacy/MetaphonePtBr.dll";
    private const string AssemblyName = "MetaphonePtBr-2.0.14";
    private const string TypeName = "MetaphonePtBr.Metaphone";
    private const string MethodName = "GetMetaphoneToken";
    // The 2.x line keeps AssemblyVersion 2.0.0.0 (the NuGet package version is 2.0.14). The exact package
    // bytes are pinned by ExpectedSha256, so this check only guards against loading a different 2.x line.
    private static readonly Version ExpectedVersion = new(2, 0, 0, 0);

    private static readonly Lazy<Func<string, string>> Encoder = new(Load);

    /// <summary>
    /// Returns the 2.0.14 <c>GetMetaphoneToken</c> function as a delegate.
    /// The first call verifies the embedded bytes and loads the assembly; later calls return the same delegate.
    /// </summary>
    /// <exception cref="InvalidOperationException">The resource, type or method is missing, or the hash or version does not match.</exception>
    public static Func<string, string> CreateEncoder() => Encoder.Value;

    private static Func<string, string> Load()
    {
        var assembly = typeof(LegacyMetaphone).Assembly;
        using Stream resource = assembly.GetManifestResourceStream(ResourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{ResourceName}' was not found. Run dotnet restore and rebuild.");

        using var buffer = new MemoryStream();
        resource.CopyTo(buffer);
        byte[] bytes = buffer.ToArray();

        // Verify the bytes before any code from them is loaded.
        string actualSha256 = Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
        if (!string.Equals(actualSha256, ExpectedSha256, StringComparison.Ordinal))
        {
            throw new InvalidOperationException(
                $"Legacy MetaphonePtBr SHA-256 mismatch. Expected {ExpectedSha256}, actual {actualSha256}.");
        }

        var context = new AssemblyLoadContext(AssemblyName, isCollectible: false);
        using var image = new MemoryStream(bytes);
        Assembly legacy = context.LoadFromStream(image);

        if (legacy.GetName().Version != ExpectedVersion)
        {
            throw new InvalidOperationException(
                $"Loaded assembly version is {legacy.GetName().Version}, expected {ExpectedVersion}.");
        }

        Type type = legacy.GetType(TypeName, throwOnError: false)
            ?? throw new InvalidOperationException($"Type '{TypeName}' was not found in the legacy assembly.");

        MethodInfo method = type.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Static, [typeof(string)])
            ?? throw new InvalidOperationException($"Method '{MethodName}(string)' was not found on '{TypeName}'.");

        return method.CreateDelegate<Func<string, string>>();
    }
}