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
            if (!dictionary.ContainsKey(key))
                throw new ChuckedAWobbly(new ShouldlyMessage(key).ToString());
        }

        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) 
        {
            if (dictionary.ContainsKey(key))
                throw new ChuckedAWobbly(new ShouldlyMessage(key).ToString());
        }

        public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val) 
        {
            if (!dictionary.ContainsKey(key))
                throw new ChuckedAWobbly(new ShouldlyMessage(val, key).ToString());

            if (!dictionary[key].Equals(val))
                throw new ChuckedAWobbly(new ShouldlyMessage(val, dictionary[key], key).ToString());
        }

        public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val) 
        {
            if (!dictionary.ContainsKey(key))
                throw new ChuckedAWobbly(new ShouldlyMessage(val, key).ToString());

            if (dictionary[key].Equals(val))
                throw new ChuckedAWobbly(new ShouldlyMessage(val, dictionary[key], key).ToString());
        }
    }
}
