using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Collections.Generic;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static void ShouldBeOneOf<T>([AllowNull] this T actual, params T[] expected)
        {
            ShouldBeOneOf(actual, expected, (string?)null);
        }
        public static void ShouldBeOneOf<T>([AllowNull] this T actual, T[] expected, string? customMessage)
        {
            // Enumerable.Contains on an array always tolerates null.
            if (!expected.Contains(actual!))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        public static void ShouldBeOneOf<T>(this T actual, T[] expected, IEqualityComparer<T> comparer)
        {
            ShouldBeOneOf(actual, expected, comparer, (string?) null);
        }
        public static void ShouldBeOneOf<T>(this T actual, T[] expected, IEqualityComparer<T> comparer, string customMessage)
        {
            if (!expected.Contains(actual, comparer))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        public static void ShouldNotBeOneOf<T>(this T actual, params T[] expected)
        {
            ShouldNotBeOneOf(actual, expected, (string) null);
        }
        public static void ShouldNotBeOneOf<T>([AllowNull] this T actual, T[] expected, string? customMessage)
        {
            // Enumerable.Contains on an array always tolerates null.
            if (expected.Contains(actual!))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, IEqualityComparer<T> comparer)
        {
            ShouldNotBeOneOf(actual, expected, comparer, (string?) null);
        }
        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, IEqualityComparer<T> comparer, string customMessage)
        {
            if (expected.Contains(actual, comparer))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        public static void ShouldBeInRange<T>(this T actual, T from, T to) where T : IComparable<T>
        {
            ShouldBeInRange(actual, from, to, (string?) null);
        }
        public static void ShouldBeInRange<T>([DisallowNull] this T actual, [AllowNull] T from, [AllowNull] T to, string? customMessage) where T : IComparable<T>

        {
            actual.AssertAwesomely(v => Is.InRange<T>(v, @from, to), actual, new { @from, to }, customMessage);
        }

        public static void ShouldNotBeInRange<T>([DisallowNull] this T actual, [AllowNull] T from, [AllowNull] T to, string? customMessage = null) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => !Is.InRange<T>(v, @from, to), actual, new { @from, to }, customMessage);
        }
    }
}