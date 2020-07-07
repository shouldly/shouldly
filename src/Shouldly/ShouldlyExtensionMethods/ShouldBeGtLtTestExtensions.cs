using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static void ShouldBeGreaterThan<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, string? customMessage = null)
        {
            actual!.AssertAwesomely(v => Is.GreaterThan(actual, expected, comparer), actual, expected, () => customMessage);
        }

        public static void ShouldBeGreaterThan<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null) where T : IComparable<T>?
        {
            actual!.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, () => customMessage);
        }

        public static void ShouldBeLessThan<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, string? customMessage = null)
        {
            actual!.AssertAwesomely(v => Is.LessThan(actual, expected, comparer), actual, expected, () => customMessage);
        }

        public static void ShouldBeLessThan<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null) where T : IComparable<T>?
        {
            actual!.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, () => customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, string? customMessage = null)
        {
            actual!.AssertAwesomely(v => Is.GreaterThanOrEqualTo(actual, expected, comparer), actual, expected, () => customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null) where T : IComparable<T>?
        {
            actual!.AssertAwesomely(v => Is.GreaterThanOrEqualTo(v, expected), actual, expected, () => customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, string? customMessage = null)
        {
            actual!.AssertAwesomely(v => Is.LessThanOrEqualTo(actual, expected, comparer), actual, expected, () => customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null) where T : IComparable<T>?
        {
            actual!.AssertAwesomely(v => Is.LessThanOrEqualTo(v, expected), actual, expected, () => customMessage);
        }
    }
}