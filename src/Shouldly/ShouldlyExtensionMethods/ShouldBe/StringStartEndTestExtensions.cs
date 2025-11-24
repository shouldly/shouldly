using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    extension([NotNull] string? actual)
    {
        /// <summary>
        /// Asserts that a string starts with another string
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldStartWith(string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
            Debug.Assert(actual != null);
        }

        /// <summary>
        /// Asserts that a string ends with another string
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldEndWith(string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
            Debug.Assert(actual != null);
        }
    }

    extension(string? actual)
    {
        /// <summary>
        /// Asserts that a string does not start with another string
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotStartWith(string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that a string does not end with another string
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotEndWith(string expected, Case caseSensitivity)
        {
            ShouldNotEndWith(actual, expected, null, caseSensitivity);
        }

        /// <summary>
        /// Asserts that a string does not end with another string
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotEndWith(string expected, string? customMessage = null, Case caseSensitivity = Case.Insensitive)
        {
            actual.AssertAwesomely(v => !Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        }
    }
}