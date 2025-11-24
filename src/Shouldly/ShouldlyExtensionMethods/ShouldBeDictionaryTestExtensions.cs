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
    extension<TKey, TValue>(IDictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        /// <summary>
        /// Asserts that the dictionary contains the specified key.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldContainKey(TKey key, string? customMessage = null)
        {
            if (!dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the dictionary does not contain the specified key.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotContainKey(TKey key, string? customMessage = null)
        {
            if (dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the dictionary contains the specified key with the specified value.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldContainKeyAndValue(TKey key, TValue val, string? customMessage = null)
        {
            if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the dictionary does not contain the specified value for the specified key.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotContainValueForKey(TKey key, TValue val, string? customMessage = null)
        {
            if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key,  customMessage).ToString());
        }
    }

#if NET9_0_OR_GREATER
    extension<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        /// <summary>
        /// Asserts that the read-only dictionary contains the specified key.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [OverloadResolutionPriority(1)]
        public void ShouldContainKey(TKey key, string? customMessage = null)
        {
            if (!dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the read-only dictionary does not contain the specified key.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [OverloadResolutionPriority(1)]
        public void ShouldNotContainKey(TKey key, string? customMessage = null)
        {
            if (dictionary.ContainsKey(key))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(key, dictionary, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the read-only dictionary contains the specified key with the specified value.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [OverloadResolutionPriority(1)]
        public void ShouldContainKeyAndValue(TKey key, TValue val, string? customMessage = null)
        {
            if (!dictionary.ContainsKey(key) || !Equals(dictionary[key], val))
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the read-only dictionary does not contain the specified value for the specified key.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [OverloadResolutionPriority(1)]
        public void ShouldNotContainValueForKey(TKey key, TValue val, string? customMessage = null)
        {
            if (!dictionary.ContainsKey(key) || Equals(dictionary[key], val))
                throw new ShouldAssertException(new ExpectedActualKeyShouldlyMessage(val, dictionary, key, customMessage).ToString());
        }
    }

#endif
}