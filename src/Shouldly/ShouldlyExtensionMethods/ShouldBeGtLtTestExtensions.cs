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
            ShouldBeGreaterThan(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldBeGreaterThan<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, Func<string?>? customMessage)
        {
            actual!.AssertAwesomely(v => Is.GreaterThan(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeGreaterThan<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null) where T : IComparable<T>?
        {
            ShouldBeGreaterThan(actual, expected, () => customMessage);
        }

        public static void ShouldBeGreaterThan<T>([AllowNull] this T actual, [AllowNull] T expected, [InstantHandle] Func<string?>? customMessage) where T : IComparable<T>?
        {
            actual!.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeLessThan<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null) where T : IComparable<T>?
        {
            ShouldBeLessThan(actual, expected, () => customMessage);
        }

        public static void ShouldBeLessThan<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, string? customMessage = null)
        {
            ShouldBeLessThan(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldBeLessThan<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, Func<string?>? customMessage)
        {
            actual!.AssertAwesomely(v => Is.LessThan(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeLessThan<T>([AllowNull] this T actual, [AllowNull] T expected, [InstantHandle] Func<string?>? customMessage) where T : IComparable<T>?
        {
            actual!.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null) where T : IComparable<T>?
        {
            ShouldBeGreaterThanOrEqualTo(actual, expected, () => customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, string? customMessage = null)
        {
            ShouldBeGreaterThanOrEqualTo(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, Func<string?>? customMessage)
        {
            actual!.AssertAwesomely(v => Is.GreaterThanOrEqualTo(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, [InstantHandle] Func<string?>? customMessage) where T : IComparable<T>?
        {
            actual!.AssertAwesomely(v => Is.GreaterThanOrEqualTo(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null) where T : IComparable<T>?
        {
            ShouldBeLessThanOrEqualTo(actual, expected, () => customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, string? customMessage = null)
        {
            ShouldBeLessThanOrEqualTo(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, IComparer<T> comparer, Func<string?>? customMessage)
        {
            actual!.AssertAwesomely(v => Is.LessThanOrEqualTo(actual, expected, comparer), actual, expected, customMessage);
        }

        public static void ShouldBeLessThanOrEqualTo<T>([AllowNull] this T actual, [AllowNull] T expected, [InstantHandle] Func<string?>? customMessage) where T : IComparable<T>?
        {
            actual!.AssertAwesomely(v => Is.LessThanOrEqualTo(v, expected), actual, expected, customMessage);
        }
    }
}