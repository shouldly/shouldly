namespace Shouldly.Internals;

static class DeterministicBuildHelpers
{
    private static readonly Regex DeterministicPathRegex = new(@"^/_\d*/");

    private static readonly Lazy<IEnumerable<(string, string)>> LazySourcePathMap
        = new(() =>
        {
            var shouldlySourcePathMap = Environment.GetEnvironmentVariable("SHOULDLY_SOURCE_PATH_MAP") ?? "";
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
}