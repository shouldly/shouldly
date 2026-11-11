namespace Shouldly.Internals;

/// <summary>
/// A sequence read once, as far as a failure message shows, so that the message can format it, fall back to it as
/// the subject and highlight its differences without reading the sequence itself again. A sequence that can only be
/// enumerated once would otherwise throw, or come up empty, on the second read.
/// </summary>
sealed class SequenceSnapshot : IEnumerable, IEnumerableProxy
{
    private readonly List<object?> _items;

    private SequenceSnapshot(IEnumerable sequence)
    {
        ProxiedValue = sequence;

        // One item more than a message shows is read, which is how the message knows to end with the "..." marker.
        _items = sequence.ReadForMessage(out _);
    }

    /// <summary>
    /// The sequence that was read. An empty sequence is described by its own text and type, not the snapshot's.
    /// </summary>
    public object ProxiedValue { get; }

    public IEnumerator GetEnumerator() => _items.GetEnumerator();

    /// <summary>
    /// A snapshot of <paramref name="value"/> when it is a sequence that may not survive being read twice, and
    /// otherwise <paramref name="value"/> itself. A collection can be read again, so it is left as it is.
    /// </summary>
    public static object? Of(object? value) =>
        value is IEnumerable sequence and not (string or ICollection or IEnumerableProxy) && !NullEquivalence.IsNull(value)
            ? new SequenceSnapshot(sequence)
            : value;
}
