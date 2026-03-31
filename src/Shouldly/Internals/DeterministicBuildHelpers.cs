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
            {
                var candidate = repoRoot + fileName.Remove(0, match.Length);
                if (File.Exists(candidate))
                    return candidate;
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
        // Scan candidate directories for all ShouldlyPathMaps_* files and merge them.
        // We can't rely on the entry assembly name because under VSTest the entry
        // assembly is "testhost", not the test project that the sidecar was written for.
        // Multiple test assemblies may share an output directory, so we merge all mappings.
        var allMappings = new List<string>();

        foreach (var dir in GetCandidateDirectories())
        {
            try
            {
                if (!Directory.Exists(dir))
                    continue;

                var matches = Directory.GetFiles(dir, "ShouldlyPathMaps_*")
                    .OrderBy(path => path, StringComparer.Ordinal);

                foreach (var match in matches)
                {
                    var contents = File.ReadAllText(match).Trim();
                    if (!string.IsNullOrEmpty(contents))
                        allMappings.Add(contents);
                }
            }
            catch
            {
                // Continue to next candidate directory
            }
        }

        return allMappings.Count > 0
            ? string.Join(",", allMappings)
            : null;
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
            // Walk up from each candidate directory (AppContext.BaseDirectory, then Assembly.Location).
            // Under coverage/tooling hosts that redirect AppContext.BaseDirectory, the assembly
            // location may be the only path that leads to the actual repo root.
            foreach (var candidate in GetCandidateDirectories())
            {
                var dir = new DirectoryInfo(candidate);
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
