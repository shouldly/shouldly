using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeDateTimeTestExtensions
    {
        public static void ShouldBe(this DateTime actual, DateTime expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        } 
    }
}