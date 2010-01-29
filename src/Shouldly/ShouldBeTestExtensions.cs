using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Shouldly
{
    [DebuggerStepThrough]
    public static class ShouldBeTestExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected)
        {
            actual.AssertAwesomely(Is.EqualTo(expected), actual, expected);
        }

        public static void ShouldBe(this float actual, float expected, double tolerance)
        {
            actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }

        public static void ShouldContain(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.StringContaining(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (!actual.Contains(expected))
            {
                throw new AssertionException(new ShouldlyMessage(expected, actual).ToString());
            }
        }

        public static void ShouldNotContain(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.Not.StringContaining(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldBeGreaterThan(this int actual, int expected)
        {
            actual.AssertAwesomely(Is.GreaterThan(expected), actual, expected);
        }

        private static void AssertAwesomely<T>(this T actual, IResolveConstraint specifiedConstraint, object originalActual, object originalExpected)
        {
            var constraint = specifiedConstraint.Resolve();
            if (constraint.Matches(actual)) return;

            throw new AssertionException(new ShouldlyMessage(originalExpected, originalActual).ToString());
        }
    }
}
