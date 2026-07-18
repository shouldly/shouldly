using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for dictionary assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeDictionaryTestExtensions
{
    // These assertions accept IEnumerable<KeyValuePair<TKey, TValue>> — the single interface that
    // both IDictionary<,> and IReadOnlyDictionary<,> derive from — rather than one overload per
    // interface. A concrete Dictionary<,> (and most BCL dictionaries) implements *both* interfaces,
    // so a pair of sibling-interface overloads is ambiguous (CS0121) on any LangVersion < 13, where
    // [OverloadResolutionPriority] is silently a no-op. Neither interface is more specific than the
    // other, so there is no tie-break to fall back on (unlike the scalar-vs-enumerable ShouldBe case
    // in #1278). Collapsing to the shared base type keeps a single unambiguous overload at every
    // LangVersion and TFM, while still covering IDictionary-only and IReadOnlyDictionary-only types.
    // See issue #1284.

    /// <summary>
    /// Asserts that the dictionary contains the specified key.
    /// </summary>
    public static void ShouldContainKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, string? customMessage = null,
        [CallerArgumentExpression(nameof(dictionary))] string? actualExpression = null)
        where TKey : notnull
    {
        if (!ContainsKey(dictionary, key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage, actualExpression: actualExpression).ToString());
    }

    /// <summary>
    /// Asserts that the dictionary does not contain the specified key.
    /// </summary>
    public static void ShouldNotContainKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, string? customMessage = null,
        [CallerArgumentExpression(nameof(dictionary))] string? actualExpression = null)
        where TKey : notnull
    {
        if (ContainsKey(dictionary, key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage, actualExpression: actualExpression).ToString());
    }

    /// <summary>
    /// Asserts that the dictionary contains the specified key with the specified value.
    /// </summary>
    public static void ShouldContainKeyAndValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue val, string? customMessage = null,
        [CallerArgumentExpression(nameof(dictionary))] string? actualExpression = null)
        where TKey : notnull
    {
        if (!TryGetValue(dictionary, key, out var actual) || !Equals(actual, val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage, actualExpression: actualExpression).ToString());
    }

    /// <summary>
    /// Asserts that the dictionary does not contain the specified value for the specified key.
    /// </summary>
    public static void ShouldNotContainValueForKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue val, string? customMessage = null,
        [CallerArgumentExpression(nameof(dictionary))] string? actualExpression = null)
        where TKey : notnull
    {
        if (!TryGetValue(dictionary, key, out var actual) || Equals(actual, val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage, actualExpression: actualExpression).ToString());
    }

    private static bool ContainsKey<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key)
        where TKey : notnull =>
        TryGetValue(dictionary, key, out _);

    private static bool TryGetValue<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, out TValue value)
        where TKey : notnull
    {
        // Prefer the dictionaries' own key comparers via their ContainsKey/TryGetValue members;
        // fall back to a linear scan for any other IEnumerable<KeyValuePair<,>> the caller passes.
        switch (dictionary)
        {
            case IReadOnlyDictionary<TKey, TValue> readOnlyDictionary:
                return readOnlyDictionary.TryGetValue(key, out value!);
            case IDictionary<TKey, TValue> mutableDictionary:
                return mutableDictionary.TryGetValue(key, out value!);
            default:
                foreach (var pair in dictionary)
                {
                    if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                    {
                        value = pair.Value;
                        return true;
                    }
                }

                value = default!;
                return false;
        }
    }
}
