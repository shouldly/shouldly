namespace Shouldly.Internals;

static class EnumerableExtensions
{
    public static IEnumerable<(int Index, T Value)> AsIndexed<T>(this IEnumerable<T> source)
    {
        var index = 0;

        foreach (var value in source)
        {
            yield return (index, value);
            index++;
        }
    }

    /// <summary>
    /// Reads at most <see cref="ShouldlyConfiguration.MaxEnumerableItemsInMessages"/> + 1 items,
    /// so describing an infinite or huge sequence in a failure message can't hang.
    /// <paramref name="truncated"/> is true when the sequence has more than the limit, in which
    /// case the returned list holds that one extra item too.
    /// </summary>
    public static List<object?> ReadForMessage(this IEnumerable source, out bool truncated)
    {
        var limit = ShouldlyConfiguration.MaxEnumerableItemsInMessages;
        var items = new List<object?>();

        foreach (var item in source)
        {
            items.Add(item);
            if (items.Count > limit)
                break;
        }

        truncated = items.Count > limit;
        return items;
    }

    /// <summary>
    /// Describes how many items a sequence has, as "3 items" or "1 item", for a failure message.
    /// Stops counting past <see cref="ShouldlyConfiguration.MaxEnumerableItemsInMessages"/>, so an
    /// infinite sequence is described as having "more than 1000" items rather than hanging.
    /// </summary>
    public static (string Count, string Noun) DescribeCountForMessage(this IEnumerable source)
    {
        var count = source.ReadForMessage(out var truncated).Count;
        if (truncated)
            return ($"more than {count - 1}", "items");

        return (count.ToString(), count == 1 ? "item" : "items");
    }
}
