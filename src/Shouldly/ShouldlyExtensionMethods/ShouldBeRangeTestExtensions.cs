namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    extension<T>(T? actual)
    {
        /// <summary>
        /// Asserts that the actual value is one of the expected values.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeOneOf(params T[] expected)
        {
            ShouldBeOneOf(actual, expected, null);
        }

        /// <summary>
        /// Asserts that the actual value is one of the expected values.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeOneOf(T[] expected, string? customMessage)
        {
            // Enumerable.Contains on an array always tolerates null.
            if (!expected.Contains(actual!))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the actual value is one of the expected values using the specified comparer.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeOneOf(T[] expected, IEqualityComparer<T> comparer, string? customMessage = null)
        {
            // Enumerable.Contains on an array always tolerates null.
            if (!expected.Contains(actual!, comparer))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the actual value is not one of the expected values.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotBeOneOf(params T[] expected)
        {
            ShouldNotBeOneOf(actual, expected, null);
        }

        /// <summary>
        /// Asserts that the actual value is not one of the expected values.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotBeOneOf(T[] expected, string? customMessage)
        {
            // Enumerable.Contains on an array always tolerates null.
            if (expected.Contains(actual!))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        /// <summary>
        /// Asserts that the actual value is not one of the expected values using the specified comparer.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotBeOneOf(T[] expected, IEqualityComparer<T> comparer, string? customMessage = null)
        {
            // Enumerable.Contains on an array always tolerates null.
            if (expected.Contains(actual!, comparer))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }
    }

    extension<T>([DisallowNull] T actual) where T : IComparable<T>
    {
        /// <summary>
        /// Asserts that the actual value is within the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeInRange(T? from, T? to, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.InRange(v, from, to), actual, new { from, to }, customMessage);
        }

        /// <summary>
        /// Asserts that the actual value is not within the specified range.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotBeInRange(T? from, T? to, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.InRange(v, from, to), actual, new { from, to }, customMessage);
        }
    }
}