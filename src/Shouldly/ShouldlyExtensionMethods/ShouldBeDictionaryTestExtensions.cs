﻿
#if NET9_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static partial class ShouldBeDictionaryTestExtensions
{
    public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    public static void ShouldContainKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.Any(entry => Equals(entry.Key, key)))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    public static void ShouldNotContainKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.Any(entry => Equals(entry.Key, key)))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    public static void ShouldContainKeyAndValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue val, string? customMessage = null)
       where TKey : notnull
    {
        if (!dictionary.Any(entry => Equals(entry.Key, key) && Equals(entry.Value, val)))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    public static void ShouldNotContainValueForKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.Any(entry => Equals(entry.Key, key)) || dictionary.Any(entry => Equals(entry.Key, key) && Equals(entry.Value, val)))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

#if NET9_0_OR_GREATER
    [OverloadResolutionPriority(1)]
    public static void ShouldContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    [OverloadResolutionPriority(1)]
    public static void ShouldNotContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    [OverloadResolutionPriority(1)]
    public static void ShouldContainKeyAndValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    [OverloadResolutionPriority(1)]
    public static void ShouldNotContainValueForKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }
#endif
}