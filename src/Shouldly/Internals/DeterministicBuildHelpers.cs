namespace Shouldly.Internals;

static class DeterministicBuildHelpers
{
    private static readonly Regex DeterministicPathRegex = new(@"^/_\d*/");

    private static readonly Lazy<IEnumerable<(string, string)>> LazySourcePathMap
        = new(ResolveSourcePathMappings);

    private static readonly Lazy<string?> LazyDiscoveredRepoRoot
        = new(TryDiscoverRepoRoot);

    private static IEnumerable<(string, string)> SourcePathMap => LazySourcePathMap.Value;

    internal static string? ResolveDeterministicPaths(string? fileName)
    {
        if (fileName == null)
            return null;

        foreach (var (path, placeholder) in SourcePathMap)
        {
            if (fileName.StartsWith(placeholder, StringComparison.Ordinal))
            {
                return fileName.Replace(placeholder, path);
            }
        }

        // Last resort: if we discovered a repo root but have no explicit mappings,
        // use regex to handle any numbered deterministic prefix (/_/, /_0/, /_10/, etc.)
        var repoRoot = LazyDiscoveredRepoRoot.Value;
        if (repoRoot != null)
        {
            var match = DeterministicPathRegex.Match(fileName);
            if (match.Success)
                return repoRoot + fileName.Remove(0, match.Length);
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
            // Scan candidate directories for any ShouldlyPathMaps_* file.
            // We can't rely on the entry assembly name because under VSTest the entry
            // assembly is "testhost", not the test project that the sidecar was written for.
            foreach (var dir in GetCandidateDirectories())
            {
                var match = Directory.GetFiles(dir, "ShouldlyPathMaps_*").FirstOrDefault();
                if (match != null)
                    return File.ReadAllText(match).Trim();
            }
        }
        catch
        {
            // Silently fail - source expressions just won't resolve deterministic paths
        }

        return null;
    }

    private static IEnumerable<string> GetCandidateDirectories()
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Try AppContext.BaseDirectory first
        var baseDir = AppContext.BaseDirectory;
        if (seen.Add(baseDir))
            yield return baseDir;

        // Fallback: try the directory containing the entry assembly.
        // Under coverage tool hosts, AppContext.BaseDirectory may point elsewhere
        // but Assembly.Location still points to the original output directory.
        var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
        var assemblyLocation = entryAssembly?.Location;
        if (!string.IsNullOrEmpty(assemblyLocation))
        {
            var assemblyDir = Path.GetDirectoryName(assemblyLocation);
            if (assemblyDir != null && seen.Add(assemblyDir))
                yield return assemblyDir;
        }
    }

    private static string? TryDiscoverRepoRoot()
    {
        try
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var gitPath = Path.Combine(dir.FullName, ".git");
                if (Directory.Exists(gitPath) ||
                    File.Exists(gitPath) || // .git can be a file in worktrees/submodules
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

    private static string EnsureTrailingSlash(string path)
    {
        var separator = Path.DirectorySeparatorChar.ToString();
        return path.EndsWith(separator, StringComparison.Ordinal)
            ? path
            : path + separator;
    }
}
