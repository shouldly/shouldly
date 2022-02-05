using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeDictionaryTestExtensions
    {
        public static void ShouldContainKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, string? customMessage = null)
            where TKey : notnull
        {
            var containsKey = dictionary switch
            {
                _ when dictionary is IDictionary<TKey, TValue> dict => dict.ContainsKey(key),
                _ when dictionary is IReadOnlyDictionary<TKey, TValue> dict => dict.ContainsKey(key),
                _ => dictionary.Any(kvp => Equals(kvp.Key, key))
            };

            if (!containsKey)
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
        }

        public static void ShouldNotContainKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, string? customMessage = null)
            where TKey : notnull
        {
            var containsKey = dictionary switch
            {
                _ when dictionary is IDictionary<TKey, TValue> dict => dict.ContainsKey(key),
                _ when dictionary is IReadOnlyDictionary<TKey, TValue> dict => dict.ContainsKey(key),
                _ => dictionary.Any(kvp => Equals(kvp.Key, key))
            };

            if (containsKey)
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
        }

        public static void ShouldContainKeyAndValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey : notnull
        {
            var containsKeyValue = dictionary switch
            {
                _ when dictionary is IDictionary<TKey, TValue> dict => dict.ContainsKey(key) && Equals(dict[key], val),
                _ when dictionary is IReadOnlyDictionary<TKey, TValue> dict => dict.ContainsKey(key) && Equals(dict[key], val),
                _ => dictionary.Any(kvp => Equals(kvp.Key, key) && Equals(kvp.Value, val))
            };

            if (!containsKeyValue)
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
        }

        public static void ShouldNotContainValueForKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey : notnull
        {
            var containsKey = false;
            var containValueForKey = dictionary switch
            {
                _ when dictionary is IDictionary<TKey, TValue> dict => (containsKey = dict.ContainsKey(key)) && Equals(dict[key], val),
                _ when dictionary is IReadOnlyDictionary<TKey, TValue> dict => (containsKey = dict.ContainsKey(key)) && Equals(dict[key], val),
                _ => dictionary.Any(kvp => (containsKey |= Equals(kvp.Key, key)) && Equals(kvp.Value, val))
            };

            if (!containsKey || containValueForKey)
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key,  customMessage).ToString());
        }
    }
}
