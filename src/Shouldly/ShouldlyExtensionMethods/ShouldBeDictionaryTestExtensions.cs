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
    /// <summary>
    /// Asserts that the dictionary contains the specified key.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the dictionary does not contain the specified key.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the dictionary contains the specified key with the specified value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the dictionary does not contain the specified value for the specified key.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key,  customMessage).ToString());
    }

#if NET9_0_OR_GREATER
    /// <summary>
    /// Asserts that the read-only dictionary contains the specified key.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only dictionary does not contain the specified key.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldNotContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only dictionary contains the specified key with the specified value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldContainKeyAndValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only dictionary does not contain the specified value for the specified key.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldNotContainValueForKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }
#endif
}