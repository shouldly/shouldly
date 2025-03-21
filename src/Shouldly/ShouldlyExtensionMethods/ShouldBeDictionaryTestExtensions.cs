﻿
using System.ComponentModel;
#if NET9_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeDictionaryTestExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key,  customMessage).ToString());
    }

#if NET9_0_OR_GREATER
    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldNotContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldContainKeyAndValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

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