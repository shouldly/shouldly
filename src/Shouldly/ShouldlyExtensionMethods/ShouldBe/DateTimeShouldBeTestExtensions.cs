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
            ShouldBe(actual, expected, tolerance, () => null);
        }

        public static void ShouldBe(this DateTime actual, DateTime expected, TimeSpan tolerance, string customMessage)
        {
            ShouldBe(actual, expected, tolerance, () => customMessage);
        }

        public static void ShouldBe(this DateTime actual, DateTime expected, TimeSpan tolerance, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance)
        {
            ShouldBe(actual, expected, tolerance, () => null);
        }

        public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, string customMessage)
        {
            ShouldBe(actual, expected, tolerance, () => customMessage);
        }

        public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
        {
            ShouldBe(actual, expected, tolerance, () => null);
        }

        public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, string customMessage)
        {
            ShouldBe(actual, expected, tolerance, () => customMessage);
        }

        public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance)
        {
            ShouldNotBe(actual, expected, tolerance, () => null);
        }

        public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance, string customMessage)
        {
            ShouldNotBe(actual, expected, tolerance, () => customMessage);
        }

        public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance)
        {
            ShouldNotBe(actual, expected, tolerance, () => null);
        }

        public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, string customMessage)
        {
            ShouldNotBe(actual, expected, tolerance, () => customMessage);
        }
        
        public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
        {
            ShouldNotBe(actual, expected, tolerance, () => null);
        }

        public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, string customMessage)
        {
            ShouldNotBe(actual, expected, tolerance, () => customMessage);
        }

        public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        } 
    }
}