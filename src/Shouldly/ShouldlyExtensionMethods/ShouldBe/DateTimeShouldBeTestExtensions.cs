using System;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static void ShouldBe(this DateTime actual, DateTime expected, TimeSpan tolerance,
            string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance,
            string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance,
            string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance,
            string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance,
            string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance,
            string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }
    }
}