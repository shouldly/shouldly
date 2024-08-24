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
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key,  customMessage).ToString());
    }

    public static void ShouldContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    public static void ShouldNotContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    public static void ShouldContainKeyAndValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    public static void ShouldNotContainValueForKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    public static void ShouldContainKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    public static void ShouldNotContainKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
    }

    public static void ShouldContainKeyAndValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }

    public static void ShouldNotContainValueForKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
            throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
    }
}