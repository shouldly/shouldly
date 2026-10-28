using System.Collections;

namespace Shouldly.Tests.Dictionaries;

/// <summary>
/// The dictionary value assertions accept any <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey,TValue}"/>,
/// so their failure messages must not assume the non-generic <see cref="IDictionary"/>. See issue #601.
/// </summary>
public class NonDictionaryKeyValueSequences
{
    private static IEnumerable<KeyValuePair<string, int>> PairList() =>
        new List<KeyValuePair<string, int>> { new("a", 1) };

    private static Dictionary<string, int> Dictionary() =>
        new() { ["a"] = 1 };

    [Fact]
    public void ShouldContainKeyAndValueReportsAWrongValue()
    {
        Verify.ShouldFail(() =>
            PairList().ShouldContainKeyAndValue("a", 2, "Some additional context"));
    }

    [Fact]
    public void ShouldContainKeyAndValueReportsAMissingKey()
    {
        Verify.ShouldFail(() =>
            PairList().ShouldContainKeyAndValue("z", 1, "Some additional context"));
    }

    [Fact]
    public void ShouldNotContainValueForKeyReportsAPresentPair()
    {
        Verify.ShouldFail(() =>
            PairList().ShouldNotContainValueForKey("a", 1, "Some additional context"));
    }

    [Fact]
    public void ShouldNotContainValueForKeyReportsAMissingKey()
    {
        Verify.ShouldFail(() =>
            PairList().ShouldNotContainValueForKey("z", 1, "Some additional context"));
    }

    [Fact]
    public void ArrayOfPairsReportsAWrongValue()
    {
        KeyValuePair<string, int>[] pairs = [new("a", 1)];

        Verify.ShouldFail(() =>
            pairs.ShouldContainKeyAndValue("a", 2, "Some additional context"));
    }

    [Fact]
    public void LinqProjectionOverADictionaryReportsAWrongValue()
    {
        Verify.ShouldFail(() =>
            Dictionary().Where(pair => true).ShouldContainKeyAndValue("a", 999, "Some additional context"));
    }

    [Fact]
    public void LinqProjectionOverADictionaryReportsAPresentPair()
    {
        Verify.ShouldFail(() =>
            Dictionary().Select(pair => pair).ShouldNotContainValueForKey("a", 1, "Some additional context"));
    }

    [Fact]
    public void NullValuesAreReportedOverAPairSequence()
    {
        IEnumerable<KeyValuePair<string, string?>> pairs =
            new List<KeyValuePair<string, string?>> { new("k", null) };

        Verify.ShouldFail(() =>
            pairs.ShouldContainKeyAndValue("k", "x", "Some additional context"));
    }

    // The shape from #601: a generic IDictionary<,> (like ASP.NET Core's IHeaderDictionary) that is not a
    // non-generic IDictionary and looks keys up with its own comparer. The message must report the lookup
    // the assertion made, not a case-sensitive re-scan that cannot find the key.
    [Fact]
    public void GenericOnlyDictionaryWithACustomComparerReportsAWrongValue()
    {
        var headers = new CaseInsensitiveHeaders { ["WWW-Authenticate"] = "Bearer" };

        Verify.ShouldFail(() =>
            headers.ShouldContainKeyAndValue("www-authenticate", "Basic", "Some additional context"));
    }

    [Fact]
    public void GenericOnlyDictionaryWithACustomComparerReportsAPresentPair()
    {
        var headers = new CaseInsensitiveHeaders { ["WWW-Authenticate"] = "Bearer" };

        Verify.ShouldFail(() =>
            headers.ShouldNotContainValueForKey("www-authenticate", "Bearer", "Some additional context"));
    }

    [Fact]
    public void SingleUseSequenceIsEnumeratedOnlyOnce()
    {
        var pairs = new SingleUseSequence<KeyValuePair<string, int>>([new("a", 1)]);

        Verify.ShouldFail(() =>
            pairs.ShouldContainKeyAndValue("a", 2, "Some additional context"));
    }

    [Fact]
    public void PassingAssertionsAreUnaffected()
    {
        PairList().ShouldContainKeyAndValue("a", 1);
        PairList().ShouldNotContainValueForKey("a", 2);
        PairList().ShouldContainKey("a");
        PairList().ShouldNotContainKey("z");
    }

    [Fact]
    public void CollectionValuesStillCompareStructurallyOverAPairSequence()
    {
        IEnumerable<KeyValuePair<string, int[]>> pairs =
            new List<KeyValuePair<string, int[]>> { new("k", [1, 2, 3]) };

        pairs.ShouldContainKeyAndValue("k", [1, 2, 3]);
        Should.Throw<ShouldAssertException>(() => pairs.ShouldContainKeyAndValue("k", [1, 2, 4]));
    }

    private sealed class CaseInsensitiveHeaders : IDictionary<string, string>
    {
        private readonly Dictionary<string, string> _inner = new(StringComparer.OrdinalIgnoreCase);

        public string this[string key] { get => _inner[key]; set => _inner[key] = value; }
        public ICollection<string> Keys => _inner.Keys;
        public ICollection<string> Values => _inner.Values;
        public int Count => _inner.Count;
        public bool IsReadOnly => false;
        public void Add(string key, string value) => _inner.Add(key, value);
        public void Add(KeyValuePair<string, string> item) => _inner.Add(item.Key, item.Value);
        public void Clear() => _inner.Clear();
        public bool Contains(KeyValuePair<string, string> item) => ((ICollection<KeyValuePair<string, string>>)_inner).Contains(item);
        public bool ContainsKey(string key) => _inner.ContainsKey(key);
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, string>>)_inner).CopyTo(array, arrayIndex);
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _inner.GetEnumerator();
        public bool Remove(string key) => _inner.Remove(key);
        public bool Remove(KeyValuePair<string, string> item) => ((ICollection<KeyValuePair<string, string>>)_inner).Remove(item);
        public bool TryGetValue(string key, out string value) => _inner.TryGetValue(key, out value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private sealed class SingleUseSequence<T>(IEnumerable<T> source) : IEnumerable<T>
    {
        private bool _enumerated;

        public IEnumerator<T> GetEnumerator()
        {
            if (_enumerated)
                throw new InvalidOperationException("This sequence can only be enumerated once.");
            _enumerated = true;
            return source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
