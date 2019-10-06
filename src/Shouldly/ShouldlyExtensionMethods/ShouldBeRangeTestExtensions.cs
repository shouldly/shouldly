using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static void ShouldBeOneOf<T>([AllowNull] this T actual, params T[] expected)
        {
            ShouldBeOneOf(actual, expected, () => null);
        }
        public static void ShouldBeOneOf<T>([AllowNull] this T actual, T[] expected, string? customMessage)
        {
            ShouldBeOneOf(actual, expected, () => customMessage);
        }
        public static void ShouldBeOneOf<T>([AllowNull] this T actual, T[] expected, [InstantHandle] Func<string?>? customMessage)
        {
            if (!expected.Contains(actual))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        public static void ShouldNotBeOneOf<T>([AllowNull] this T actual, params T[] expected)
        {
            ShouldNotBeOneOf(actual, expected, () => null);
        }
        public static void ShouldNotBeOneOf<T>([AllowNull] this T actual, T[] expected, string? customMessage)
        {
            ShouldNotBeOneOf(actual, expected, () => customMessage);
        }
        public static void ShouldNotBeOneOf<T>([AllowNull] this T actual, T[] expected, [InstantHandle] Func<string?>? customMessage)
        {
            if (expected.Contains(actual))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        public static void ShouldBeInRange<T>([DisallowNull] this T actual, [AllowNull] T from, [AllowNull] T to) where T : IComparable<T>
        {
            ShouldBeInRange(actual, from, to, () => null);
        }
        public static void ShouldBeInRange<T>([DisallowNull] this T actual, [AllowNull] T from, [AllowNull] T to, string? customMessage) where T : IComparable<T>
        {
            ShouldBeInRange(actual, from, to, () => customMessage);
        }
        public static void ShouldBeInRange<T>([DisallowNull] this T actual, [AllowNull] T from, [AllowNull] T to, [InstantHandle] Func<string?>? customMessage) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.InRange<T>(v, @from, to), actual, new { @from, to }, customMessage);
        }

        public static void ShouldNotBeInRange<T>([DisallowNull] this T actual, [AllowNull] T from, [AllowNull] T to) where T : IComparable<T>
        {
            ShouldNotBeInRange(actual, from, to, () => null);
        }
        public static void ShouldNotBeInRange<T>([DisallowNull] this T actual, [AllowNull] T from, [AllowNull] T to, string? customMessage) where T : IComparable<T>
        {
            ShouldNotBeInRange(actual, from, to, () => customMessage);
        }
        public static void ShouldNotBeInRange<T>([DisallowNull] this T actual, [AllowNull] T from, [AllowNull] T to, [InstantHandle] Func<string?>? customMessage) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => !Is.InRange<T>(v, @from, to), actual, new { @from, to }, customMessage);
        }
    }
}