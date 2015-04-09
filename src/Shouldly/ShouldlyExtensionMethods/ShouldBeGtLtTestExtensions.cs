using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeGtLtTestExtensions
    {
        public static void ShouldBeGreaterThan<T>(this T actual, T expected) where T : IComparable<T>
        {
            ShouldBeGreaterThan(actual, expected, () => null);
        }

        public static void ShouldBeGreaterThan<T>(this T actual, T expected, string customMessage) where T : IComparable<T>
        {
            ShouldBeGreaterThan(actual, expected, () => customMessage);
        }

        public static void ShouldBeGreaterThan<T>(this T actual, T expected, Func<string> customMessage) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected) where T : IComparable<T>
        {
            ShouldBeLessThan(actual, expected, () => null);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected, string customMessage) where T : IComparable<T>
        {
            ShouldBeLessThan(actual, expected, () => customMessage);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected, Func<string> customMessage) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected) where T : IComparable<T>
        {
            ShouldBeGreaterThanOrEqualTo(actual, expected, () => null);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, string customMessage) where T : IComparable<T>
        {
            ShouldBeGreaterThanOrEqualTo(actual, expected, () => customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, Func<string> customMessage) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.GreaterThanOrEqualTo(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected) where T : IComparable<T>
        {
            ShouldBeLessThanOrEqualTo(actual, expected, () => null);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, string customMessage) where T : IComparable<T>
        {
            ShouldBeLessThanOrEqualTo(actual, expected, () => customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, Func<string> customMessage) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.LessThanOrEqualTo(v, expected), actual, expected, customMessage);
        } 
    }
}