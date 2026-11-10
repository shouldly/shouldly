namespace Shouldly.Internals;

interface IEnumerableProxy
{
    object ProxiedValue { get; }
}

/// <summary>
/// Makes a one-shot sequence safe to enumerate more than once: the assertion and its failure
/// message must see the same items. Items are buffered as they are first read rather than all up
/// front, so an infinite or very long sequence is only read as far as anything actually asks.
/// </summary>
sealed class EnumerableProxy<T> : IEnumerable<T>, IEnumerableProxy, IDisposable
{
    public static IEnumerable<T>? WrapNonCollection(IEnumerable<T>? baseEnum)
    {
        if (baseEnum is (null or IReadOnlyCollection<T> or ICollection<T> or ICollection))
        {
            return baseEnum;
        }

        if (baseEnum is EnumerableProxy<T>)
        {
            throw new ArgumentException("Value already wrapped.", nameof(baseEnum));
        }

        return new EnumerableProxy<T>(baseEnum);
    }

    public IEnumerable<T> ProxiedValue { get; }
    object IEnumerableProxy.ProxiedValue => ProxiedValue;

    private readonly List<T> _buffer = [];
    private IEnumerator<T>? _source;
    private bool _sourceFinished;

    private EnumerableProxy(IEnumerable<T> baseEnum) =>
        ProxiedValue = baseEnum;

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

        _source ??= ProxiedValue.GetEnumerator();
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
