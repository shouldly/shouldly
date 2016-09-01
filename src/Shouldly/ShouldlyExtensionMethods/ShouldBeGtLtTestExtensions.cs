using System;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static void ShouldBeGreaterThan<T>(this T actual, T expected) where T : IComparable<T>
        {
            ShouldBeGreaterThan(actual, expected, () => null);
        }

        public static void ShouldBeGreaterThan<T>(this T actual, T expected, IComparer<T> comparer)
        {
            ShouldBeGreaterThan(actual, expected, comparer, () => null);
        }

        public static void ShouldBeGreaterThan<T>(this T actual, T expected, IComparer<T> comparer, string customMessage)
        {
            ShouldBeGreaterThan(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldBeGreaterThan<T>(this T actual, T expected, IComparer<T> comparer, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.GreaterThan(actual, expected, comparer), actual, expected, customMessage);
        }
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, string customMessage) where T : IComparable<T>
        {
            ShouldBeGreaterThan(actual, expected, () => customMessage);
        }

        public static void ShouldBeGreaterThan<T>(this T actual, T expected, [InstantHandle] Func<string> customMessage) where T : IComparable<T>
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

        public static void ShouldBeLessThan<T>(this T actual, T expected, IComparer<T> comparer)
        {
            ShouldBeLessThan(actual, expected, comparer, () => null);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected, IComparer<T> comparer, string customMessage)
        {
            ShouldBeLessThan(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected, IComparer<T> comparer, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.LessThan(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected, [InstantHandle] Func<string> customMessage) where T : IComparable<T>
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

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, IComparer<T> comparer)
        {
            ShouldBeGreaterThanOrEqualTo(actual, expected, comparer, () => null);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, IComparer<T> comparer, string customMessage)
        {
            ShouldBeGreaterThanOrEqualTo(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, IComparer<T> comparer, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.GreaterThanOrEqualTo(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, [InstantHandle] Func<string> customMessage) where T : IComparable<T>
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

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, IComparer<T> comparer)
        {
            ShouldBeLessThanOrEqualTo(actual, expected, comparer, () => null);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, IComparer<T> comparer, string customMessage)
        {
            ShouldBeLessThanOrEqualTo(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, IComparer<T> comparer, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.LessThanOrEqualTo(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, [InstantHandle] Func<string> customMessage) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.LessThanOrEqualTo(v, expected), actual, expected, customMessage);
        } 
    }
}