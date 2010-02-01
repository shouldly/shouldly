using System.Diagnostics;
using NUnit.Framework;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeStringTestExtensions
    {
        public static void ShouldBeCloseTo(this string actual, object expected)
        {
            actual.Quotify()
                  .StripWhitespace()
                  .AssertAwesomely(Is.EqualTo(
                    (expected ?? "NULL").ToString()
                            .Quotify()
                            .StripWhitespace()), actual, expected);
        }

        public static void ShouldContain(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.StringContaining(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldNotContain(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.Not.StringContaining(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldBeGreaterThan(this int actual, int expected)
        {
            actual.AssertAwesomely(Is.GreaterThan(expected), actual, expected);
        }
    }
}