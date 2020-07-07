using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeDictionaryTestExtensions
    {
        public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey : notnull
        {
            ShouldContainKey(dictionary, key, () => customMessage);
        }

        public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, [InstantHandle] Func<string?>? customMessage)
            where TKey : notnull
        {
            if (!dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
        }
        
        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey : notnull
        {
            ShouldNotContainKey(dictionary, key, () => customMessage);
        }

        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, [InstantHandle] Func<string?>? customMessage)
            where TKey : notnull
        {
            if (dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
        }
        
        public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey : notnull
        {
            ShouldContainKeyAndValue(dictionary, key, val, () => customMessage);
        }

        public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, [InstantHandle] Func<string?>? customMessage)
            where TKey : notnull
        {
            if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
        }
        
        public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey : notnull
        {
            ShouldNotContainValueForKey(dictionary, key, val, () => customMessage);
        }

        public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, [InstantHandle] Func<string?>? customMessage)
            where TKey : notnull
        {
            if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
        }
    }
}
