namespace Shouldly.Internals;

static class DeterministicBuildHelpers
{
    private static readonly Regex DeterministicPathRegex = new(@"^/_\d*/");

    private static readonly Lazy<IEnumerable<(string, string)>> LazySourcePathMap
        = new(ResolveSourcePathMappings);

    private static IEnumerable<(string, string)> SourcePathMap => LazySourcePathMap.Value;

    internal static string? ResolveDeterministicPaths(string? fileName)
    {
        foreach (var (path, placeholder) in SourcePathMap)
        {
            if (fileName?.StartsWith(placeholder, StringComparison.Ordinal) == true)
            {
                return fileName.Replace(placeholder, path);
            }
        }

        return fileName;
    }

    internal static bool PathAppearsToBeDeterministic(string fileName) => DeterministicPathRegex.IsMatch(fileName);

    private static IEnumerable<(string, string)> ResolveSourcePathMappings()
    {
        // Priority 1: Explicit env var override (only set manually by users)
        var envVar = Environment.GetEnvironmentVariable("SHOULDLY_SOURCE_PATH_MAP") ?? "";
        if (!string.IsNullOrEmpty(envVar))
            return ParsePathMapFormat(envVar);

        // Priority 2: Sidecar file written at build time
        var fileContents = TryReadPathMapsFile();
        if (fileContents != null)
            return ParsePathMapFormat(fileContents);

        // Priority 3: Runtime repo root discovery
        var repoRoot = TryDiscoverRepoRoot();
        if (repoRoot != null)
            return GenerateRepoRootMappings(repoRoot);

        return [];
    }

    private static IEnumerable<(string, string)> ParsePathMapFormat(string pathMap)
    {
        return pathMap
            .Split(',')
            .Select(x => x.Split('='))
            .Where(x => x.Length == 2)
            .Select(x => (x[0], x[1]));
    }

    private static string? TryReadPathMapsFile()
    {
        try
        {
            var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
            if (entryAssembly == null)
                return null;

            var assemblyName = entryAssembly.GetName().Name;
            var fileName = $"ShouldlyPathMaps_{assemblyName}";

            // Try AppContext.BaseDirectory first
            var baseDir = AppContext.BaseDirectory;
            var pathMapsFile = Path.Combine(baseDir, fileName);
            if (File.Exists(pathMapsFile))
                return File.ReadAllText(pathMapsFile).Trim();

            // Fallback: try the directory containing the entry assembly.
            // Under coverage tool hosts, AppContext.BaseDirectory may point elsewhere
            // but Assembly.Location still points to the original output directory.
            var assemblyLocation = entryAssembly.Location;
            if (!string.IsNullOrEmpty(assemblyLocation))
            {
                var assemblyDir = Path.GetDirectoryName(assemblyLocation);
                if (assemblyDir != null && assemblyDir != baseDir)
                {
                    pathMapsFile = Path.Combine(assemblyDir, fileName);
                    if (File.Exists(pathMapsFile))
                        return File.ReadAllText(pathMapsFile).Trim();
                }
            }
        }
        catch
        {
            // Silently fail - source expressions just won't resolve deterministic paths
        }

        return null;
    }

    private static string? TryDiscoverRepoRoot()
    {
        try
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                if (Directory.Exists(Path.Combine(dir.FullName, ".git")) ||
                    File.Exists(Path.Combine(dir.FullName, "global.json")))
                {
                    return EnsureTrailingSlash(dir.FullName);
                }

                dir = dir.Parent;
            }
        }
        catch
        {
            // Silently fail
        }

        return null;
    }

    private static IEnumerable<(string, string)> GenerateRepoRootMappings(string repoRoot)
    {
        // Generate mappings for /_/ through /_9/ to cover numbered deterministic prefixes
        yield return (repoRoot, "/_/");
        for (var i = 0; i <= 9; i++)
            yield return (repoRoot, $"/_{i}/");
    }

    private static string EnsureTrailingSlash(string path)
    {
        var separator = Path.DirectorySeparatorChar.ToString();
        return path.EndsWith(separator, StringComparison.Ordinal)
            ? path
            : path + separator;
    }
}
