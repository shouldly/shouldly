namespace Shouldly.Internals.Failures;

/// <summary>
/// How two collections compared without regard to order (see <see cref="UnorderedCollection"/>) that should have
/// been equal are told apart in a failure message: by what one has that the other doesn't. A dictionary's entries
/// are told apart by key.
/// </summary>
static class UnorderedWording
{
    /// <summary>
    /// The <c>difference</c> section of the message, or null when the two are compared in order, or nothing tells
    /// them apart as the assertion compared them.
    /// </summary>
    public static string? Difference(Differences found)
    {
        if (found.Equality.ComparesValuesItself ||
            found.Unordered is not { Items.Count: > 0 } differences)
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

    private static string Line(UnorderedDifference difference, bool byKey)
    {
        var item = byKey
            ? $"[{difference.Item.ToStringAwesomely()}]"
            : difference.Item.ToStringAwesomely();
        return difference.Kind switch
        {
            UnorderedDifferenceKind.ValueDiffers =>
                $"{item} should be {difference.Expected.ToStringAwesomely()} but was {difference.Actual.ToStringAwesomely()}",
            UnorderedDifferenceKind.Missing => $"{item} is missing",
            _ => $"{item} is not expected"
        };
    }
}
