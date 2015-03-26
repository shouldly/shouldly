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
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.GreaterThanOrEqualTo(v, expected), actual, expected);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.LessThanOrEqualTo(v, expected), actual, expected);
        } 
    }
}