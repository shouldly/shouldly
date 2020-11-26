using System;
using System.Collections.Generic;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static void ShouldBeGreaterThan<T>(this T? actual, T? expected, IComparer<T> comparer, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.GreaterThan(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeGreaterThan<T>(this T? actual, T? expected, string? customMessage = null) where T : IComparable<T>?
        {
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeLessThan<T>(this T? actual, T? expected, IComparer<T> comparer, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.LessThan(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeLessThan<T>(this T? actual, T? expected, string? customMessage = null) where T : IComparable<T>?
        {
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T? actual, T? expected, IComparer<T> comparer, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.GreaterThanOrEqualTo(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T? actual, T? expected, string? customMessage = null) where T : IComparable<T>?
        {
            actual.AssertAwesomely(v => Is.GreaterThanOrEqualTo(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T? actual, T? expected, IComparer<T> comparer, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.LessThanOrEqualTo(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T? actual, T? expected, string? customMessage = null) where T : IComparable<T>?
        {
            actual.AssertAwesomely(v => Is.LessThanOrEqualTo(v, expected), actual, expected, customMessage);
        }
    }
}