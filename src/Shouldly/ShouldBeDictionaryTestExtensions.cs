using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;

namespace Shouldly {


    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeDictionaryTestExtensions {
        public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            if (!dictionary.ContainsKey(key))
                throw new AssertionException(new ShouldlyMessage(key).ToString());

        }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val) {

            if (!dictionary.ContainsKey(key))
                throw new AssertionException(new ShouldlyMessage(key).ToString());

            if (!dictionary[key].Equals(val))
                throw new AssertionException(new ShouldlyMessage(val,dictionary[key]).ToString());


        }
        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            if (dictionary.ContainsKey(key))
                throw new AssertionException(new ShouldlyMessage(key).ToString());

        }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val) {

            if (!dictionary.ContainsKey(key))
                throw new AssertionException(new ShouldlyMessage(key).ToString());

            if (dictionary[key].Equals(val))
                throw new AssertionException(new ShouldlyMessage(dictionary[key],val).ToString());

        }
    }


}
