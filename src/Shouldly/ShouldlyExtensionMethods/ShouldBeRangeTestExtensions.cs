using System;
using System.Diagnostics;
using System.Linq;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static void ShouldBeOneOf<T>(this T actual, params T[] expected)
        {
            if (!expected.Contains(actual))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldNotBeOneOf<T>(this T actual, params T[] expected)
        {
            if (expected.Contains(actual))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldBeInRange<T>(this T actual, T from, T to) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.InRange<T>(v, @from, to), actual, new { @from, to });
        }

        public static void ShouldNotBeInRange<T>(this T actual, T from, T to) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => !Is.InRange<T>(v, @from, to), actual, new { @from, to });
        }
    }
}