using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Shouldly 
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeDictionaryTestExtensions 
    {
        public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            ShouldContainKey(dictionary, key, () => null);
        }

        public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string customMessage)
        {
            ShouldContainKey(dictionary, key, () => customMessage);
        }

        public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<string> customMessage)
        {
            if (!dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(key, customMessage()).ToString());
        }

        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            ShouldNotContainKey(dictionary, key, () => null);
        }

        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string customMessage)
        {
            ShouldNotContainKey(dictionary, key, () => customMessage);
        }

        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<string> customMessage)
        {
            if (dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(key, customMessage()).ToString());
        }

        public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val) 
        {
            ShouldContainKeyAndValue(dictionary, key, val, () => null);
        }

        public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string customMessage)
        {
            ShouldContainKeyAndValue(dictionary, key, val, () => customMessage);
        }

        public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, Func<string> customMessage)
        {
            if (!dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(val, key, customMessage()).ToString());

            if (!dictionary[key].Equals(val))
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary[key], key, customMessage()).ToString());
        }

        public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val)
        {
            ShouldNotContainValueForKey(dictionary, key, val, () => null);
        }

        public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string customMessage)
        {
            ShouldNotContainValueForKey(dictionary, key, val, () => customMessage);
        }

        public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, Func<string> customMessage)
        {
            if (!dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(val, key, customMessage()).ToString());

            if (dictionary[key].Equals(val))
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary[key], key, customMessage()).ToString());
        }
    }
}
