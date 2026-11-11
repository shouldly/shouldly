namespace Shouldly.Internals;

/// <summary>
/// A sequence as an assertion read it. The check and the failure message both read the capture, never the sequence
/// itself, so a sequence that can only be read once is read once, and no further than the check and the message ask.
/// </summary>
interface ICapturedSequence
{
    /// <summary>
    /// The sequence that was captured. Equality rules are looked up by its type, and an empty sequence is described
    /// by its own text and type rather than the capture's.
    /// </summary>
    object Original { get; }
}

/// <summary>
/// Captures sequences, so that an assertion and its failure message see the same items.
/// </summary>
static class CapturedSequence
{
    /// <summary>
    /// Captures <paramref name="sequence"/> before an assertion's check reads it. Read the capture's
    /// <see cref="CapturedSequence{T}.Items"/> from then on, and dispose the capture when the assertion ends.
    /// </summary>
    /// <remarks>
    /// A collection or a string, which can be read again, and null are left as they are. Any other sequence is buffered as it
    /// is first read rather than all up front, so an infinite or very long sequence is only read as far as
    /// anything actually asks.
    /// </remarks>
    public static CapturedSequence<T> Of<T>(IEnumerable<T>? sequence) =>
        sequence is null or string or IReadOnlyCollection<T> or ICollection<T> or ICollection or ICapturedSequence
            ? new(sequence, null)
            : new(null, new LazyCapture<T>(sequence, sequence));

    /// <summary>
    /// <paramref name="value"/> read as far as a failure message shows, when it is a sequence, so that the message
    /// can format it, fall back to it as the subject and highlight its differences without reading it again, and
    /// is unaffected if the sequence changes later. Any other value, and a collection, is returned as it is.
    /// </summary>
    /// <remarks>
    /// A sequence an assertion has already captured is read through its capture, never from the start again.
    /// </remarks>
    public static object? ForMessage(object? value) =>
        value is IEnumerable sequence and not (string or ICollection or FrozenSequence) && !NullEquivalence.IsNull(value)
            ? new FrozenSequence(sequence)
            : value;

    /// <summary>
    /// Captures <paramref name="value"/> lazily when it is a sequence, for a message that may need to read all of
    /// it: a count, or the items outside another sequence. Any other value, and a collection, is returned as it is.
    /// Release the capture with <see cref="Release"/> once the message is written.
    /// </summary>
    public static object? Lazily(object? value) =>
        value is IEnumerable sequence and not (string or ICollection or ICapturedSequence) && !NullEquivalence.IsNull(value)
            ? new LazyCapture<object?>(sequence.Cast<object?>(), value)
            : value;

    /// <summary>
    /// Releases a capture made by <see cref="Lazily"/>. Any other value is left alone, even one that is disposable.
    /// </summary>
    public static void Release(object? value)
    {
        if (value is ICapturedSequence and IDisposable capture)
            capture.Dispose();
    }

    /// <summary>
    /// The sequence <paramref name="value"/> captured, or <paramref name="value"/> itself when it isn't a capture.
    /// </summary>
    public static object Original(object value) =>
        value is ICapturedSequence captured ? captured.Original : value;

    /// <summary>
    /// A sequence buffered as it is first read from <paramref name="source"/>, which reads
    /// <paramref name="original"/>.
    /// </summary>
    internal sealed class LazyCapture<T>(IEnumerable<T> source, object original) : IEnumerable<T>, ICapturedSequence, IDisposable
    {
        private readonly List<T> _buffer = [];
        private IEnumerator<T>? _source;
        private bool _sourceFinished;

        public object Original => original;

        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0; ; index++)
            {
                if (index >= _buffer.Count && !TryBufferNext())
                    yield break;

                yield return _buffer[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private bool TryBufferNext()
        {
            if (_sourceFinished)
                return false;

            _source ??= source.GetEnumerator();
            if (_source.MoveNext())
            {
                _buffer.Add(_source.Current);
                return true;
            }

            Dispose();
            return false;
        }

        /// <summary>
        /// Releases the underlying enumerator, which may not have been read to the end.
        /// </summary>
        public void Dispose()
        {
            _sourceFinished = true;
            _source?.Dispose();
            _source = null;
        }
    }

    /// <summary>
    /// A sequence read once, as far as a failure message shows.
    /// </summary>
    private sealed class FrozenSequence : IEnumerable, ICapturedSequence
    {
        private readonly List<object?> _items;

        public FrozenSequence(IEnumerable sequence)
        {
            Original = CapturedSequence.Original(sequence);

            // One item more than a message shows is read, which is how the message knows to end with the "..." marker.
            _items = sequence.ReadForMessage(out _);
        }

        public object Original { get; }

        public IEnumerator GetEnumerator() => _items.GetEnumerator();
    }
}

/// <summary>
/// A sequence captured by <see cref="CapturedSequence.Of{T}"/>, for the length of one assertion.
/// </summary>
readonly struct CapturedSequence<T> : IDisposable
{
    private readonly CapturedSequence.LazyCapture<T>? _capture;

    internal CapturedSequence(IEnumerable<T>? unchanged, CapturedSequence.LazyCapture<T>? capture)
    {
        _capture = capture;
        Items = capture ?? unchanged;
    }

    /// <summary>
    /// The items to check and to show in the failure message: the sequence itself when it can be read again, or
    /// its capture.
    /// </summary>
    public IEnumerable<T>? Items { get; }

    /// <summary>
    /// Releases a capture made here. A sequence that was already captured, by an assertion that called this one,
    /// stays open for that assertion.
    /// </summary>
    public void Dispose() => _capture?.Dispose();
}
