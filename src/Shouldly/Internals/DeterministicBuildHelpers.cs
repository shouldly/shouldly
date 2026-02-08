namespace Shouldly.Internals;

static class DeterministicBuildHelpers
{
    private static readonly Regex DeterministicPathRegex = new(@"^/_\d*/");

    private static readonly Lazy<IEnumerable<(string, string)>> LazySourcePathMap
        = new(() =>
        {
            var shouldlySourcePathMap = Environment.GetEnvironmentVariable("SHOULDLY_SOURCE_PATH_MAP") ?? "";

            // Fallback: read from ShouldlyPathMaps file in the output directory.
            // This is needed for Microsoft Testing Platform (MTP) where the test exe is launched
            // directly and doesn't inherit the env var set during MSBuild.
            if (string.IsNullOrEmpty(shouldlySourcePathMap))
            {
                shouldlySourcePathMap = TryReadPathMapsFile() ?? "";
            }

            return shouldlySourcePathMap
                .Split(',')
                .Select(x => x.Split('='))
                .Where(x => x.Length == 2)
                .Select(x => (x[0], x[1]));
        });

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

    private static string? TryReadPathMapsFile()
    {
        try
        {
            var baseDir = AppContext.BaseDirectory;
            var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                var pathMapsFile = Path.Combine(baseDir, $"ShouldlyPathMaps_{entryAssembly.GetName().Name}");
                if (File.Exists(pathMapsFile))
                {
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
}