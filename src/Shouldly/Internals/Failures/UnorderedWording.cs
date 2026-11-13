namespace Shouldly.Internals.Failures;

/// <summary>
/// How two collections compared without regard to order (see <see cref="UnorderedCollection"/>) that should have
/// been equal are told apart in a failure message: by what one has that the other doesn't. A dictionary's entries
/// are told apart by key.
/// </summary>
static class UnorderedWording
{
    /// <summary>
    /// The <c>difference</c> section of the message, or null when <paramref name="actual"/> and
    /// <paramref name="expected"/> are compared in order, or nothing tells them apart as the assertion compared
    /// them.
    /// </summary>
    public static string? Difference(object? actual, object? expected, ComparisonRecord? record)
    {
        var comparison = record ?? ComparisonRecord.Default;
        if (comparison.ComparesValuesItself ||
            UnorderedCollection.Find(actual, expected, comparison) is not { Items.Count: > 0 } differences)
            return null;

        var limit = ShouldlyConfiguration.MaxEnumerableItemsInMessages;
        var lines = differences.Items
            .Take(limit)
            .Select(difference => Line(difference, differences.ByKey))
            .ToList();
        if (differences.Items.Count > limit)
            lines.Add(StringHelpers.EnumerableTruncationMarker);

        return $"""

                    difference
                {string.Join("\r\n", lines)}
                """;
    }

    private static string Line(UnorderedCollection.Difference difference, bool byKey)
    {
        var item = byKey
            ? $"[{difference.Item.ToStringAwesomely()}]"
            : difference.Item.ToStringAwesomely();
        return difference.Kind switch
        {
            UnorderedCollection.DifferenceKind.ValueDiffers =>
                $"{item} should be {difference.Expected.ToStringAwesomely()} but was {difference.Actual.ToStringAwesomely()}",
            UnorderedCollection.DifferenceKind.Missing => $"{item} is missing",
            _ => $"{item} is not expected"
        };
    }
}
