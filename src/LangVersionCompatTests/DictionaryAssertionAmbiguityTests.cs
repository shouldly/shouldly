using Shouldly;
using Xunit;

namespace LangVersionCompatTests;

/// <summary>
/// Regression guard for https://github.com/shouldly/shouldly/issues/1284.
///
/// This project is pinned to <c>LangVersion 12</c> (see the .csproj). The v5 dictionary
/// assertions previously shipped a pair of sibling-interface overloads —
/// <c>IDictionary&lt;,&gt;</c> and (on net9.0+) <c>IReadOnlyDictionary&lt;,&gt;</c> — with the
/// read-only one decorated <c>[OverloadResolutionPriority(1)]</c> to break the tie. Because a
/// concrete <c>Dictionary&lt;,&gt;</c> implements both interfaces, calling e.g.
/// <c>dict.ShouldContainKey(key)</c> on it failed with CS0121 on any <c>LangVersion &lt; 13</c>,
/// where <c>[OverloadResolutionPriority]</c> is silently a no-op. Unlike #1278 these are sibling
/// interfaces with no "more specific" tie-break, so equalising arity could not help.
///
/// The fix collapses each assertion to a single overload typed to the shared base,
/// <c>IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;</c>. The <em>compilation</em> of this
/// file is the primary regression test; the runtime assertions confirm each call still binds and
/// dispatches correctly for the concrete, mutable-interface, and read-only-interface receivers.
/// </summary>
public class DictionaryAssertionAmbiguityTests
{
    [Fact]
    public void Concrete_dictionary_binds_without_ambiguity()
    {
        // The exact repro from the issue: a Dictionary<,> implements both dictionary interfaces.
        var dict = new Dictionary<string, int> { ["a"] = 1 };

        dict.ShouldContainKey("a");
        dict.ShouldNotContainKey("b");
        dict.ShouldContainKeyAndValue("a", 1);
        dict.ShouldNotContainValueForKey("a", 2);
    }

    [Fact]
    public void Mutable_dictionary_interface_binds()
    {
        IDictionary<string, int> dict = new Dictionary<string, int> { ["a"] = 1 };

        dict.ShouldContainKey("a");
        dict.ShouldNotContainKey("b");
        dict.ShouldContainKeyAndValue("a", 1);
        dict.ShouldNotContainValueForKey("a", 2);
    }

    [Fact]
    public void ReadOnly_dictionary_interface_binds()
    {
        IReadOnlyDictionary<string, int> dict = new Dictionary<string, int> { ["a"] = 1 };

        dict.ShouldContainKey("a");
        dict.ShouldNotContainKey("b");
        dict.ShouldContainKeyAndValue("a", 1);
        dict.ShouldNotContainValueForKey("a", 2);
    }

    /// <summary>
    /// Regression guard for https://github.com/shouldly/shouldly/issues/1459. A dictionary that also
    /// enumerates its values, as MSBuild's <c>PropertyDictionary&lt;T&gt;</c> does, implements two
    /// <c>IEnumerable&lt;&gt;</c>s, so <c>TKey</c> and <c>TValue</c> can't be inferred from the
    /// <c>IEnumerable&lt;KeyValuePair&lt;,&gt;&gt;</c> overload alone. The <c>IDictionary&lt;,&gt;</c> overload
    /// infers them, and is a better conversion than the other wherever both apply, so it adds no ambiguity.
    /// </summary>
    [Fact]
    public void Dictionary_that_also_enumerates_its_values_binds()
    {
        var dict = new ValueEnumeratingDictionary<int> { ["a"] = 1 };

        dict.ShouldContainKey("a");
        dict.ShouldNotContainKey("b");
        dict.ShouldContainKeyAndValue("a", 1);
        dict.ShouldNotContainValueForKey("a", 2);

        Should.Throw<ShouldAssertException>(() => dict.ShouldContainKey("b"))
            .Message.ShouldContain("should contain key");
    }

    /// <summary>
    /// The shape of MSBuild's <c>PropertyDictionary&lt;T&gt;</c>: an <c>IDictionary&lt;string, T&gt;</c>
    /// that is also an <c>IEnumerable&lt;T&gt;</c> of its values.
    /// </summary>
    private sealed class ValueEnumeratingDictionary<T> : IDictionary<string, T>, IEnumerable<T>
    {
        private readonly Dictionary<string, T> _inner = new();

        public T this[string key] { get => _inner[key]; set => _inner[key] = value; }
        public ICollection<string> Keys => _inner.Keys;
        public ICollection<T> Values => _inner.Values;
        public int Count => _inner.Count;
        public bool IsReadOnly => false;
        public void Add(string key, T value) => _inner.Add(key, value);
        public void Add(KeyValuePair<string, T> item) => ((ICollection<KeyValuePair<string, T>>)_inner).Add(item);
        public void Clear() => _inner.Clear();
        public bool Contains(KeyValuePair<string, T> item) => ((ICollection<KeyValuePair<string, T>>)_inner).Contains(item);
        public bool ContainsKey(string key) => _inner.ContainsKey(key);
        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, T>>)_inner).CopyTo(array, arrayIndex);
        public bool Remove(string key) => _inner.Remove(key);
        public bool Remove(KeyValuePair<string, T> item) => ((ICollection<KeyValuePair<string, T>>)_inner).Remove(item);
        public bool TryGetValue(string key, out T value) => _inner.TryGetValue(key, out value!);
        public IEnumerator<T> GetEnumerator() => _inner.Values.GetEnumerator();
        IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator() => _inner.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
