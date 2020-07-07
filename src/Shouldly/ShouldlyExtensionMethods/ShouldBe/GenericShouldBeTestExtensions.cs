using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeTestExtensions
    {
        [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
        public static void  ShouldBe<T>(
            [AllowNull, NotNullIfNotNull("expected")] this T actual,
            [AllowNull, NotNullIfNotNull("actual")] T expected
            , string? customMessage = null)
        {
            ShouldBe(actual, expected, () => customMessage);
        }
        [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
        public static void ShouldBe<T>(
            [AllowNull, NotNullIfNotNull("expected")] this T actual,
            [AllowNull, NotNullIfNotNull("actual")] T expected,
            [InstantHandle] Func<string?>? customMessage)
        {
            if (ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!) || typeof(T) == typeof(string))
                actual!.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<T>()), actual, expected, customMessage);
            else
                actual!.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage);
        }

        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>([AllowNull] this T actual, [AllowNull] T expected, string? customMessage = null)
        {
            ShouldNotBe(actual, expected, () => customMessage);
        }
        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>([AllowNull] this T actual, [AllowNull] T expected, [InstantHandle] Func<string?>? customMessage)
        {
            actual!.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBe<T>(
            [NotNullIfNotNull("expected")] this IEnumerable<T>? actual,
            [NotNullIfNotNull("actual")] IEnumerable<T>? expected,
            bool ignoreOrder = false)
        {
            ShouldBe(actual, expected, ignoreOrder, () => null);
        }
        public static void ShouldBe<T>(
            [NotNullIfNotNull("expected")] this IEnumerable<T>? actual,
            [NotNullIfNotNull("actual")] IEnumerable<T>? expected,
            bool ignoreOrder,
            string? customMessage)
        {
            ShouldBe(actual, expected, ignoreOrder, () => customMessage);
        }
        public static void ShouldBe<T>(
            [NotNullIfNotNull("expected")] this IEnumerable<T>? actual,
            [NotNullIfNotNull("actual")] IEnumerable<T>? expected,
            bool ignoreOrder,
            [InstantHandle] Func<string?>? customMessage)
        {
            if (!ignoreOrder && ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!))
            {
                actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<IEnumerable<T>?>()), actual, expected, customMessage);
            }
            else
            {
                if (ignoreOrder)
                {
                    actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected), actual, expected, customMessage);
                }
                else
                {
                    actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage);
                }
            }
        }

        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, () => customMessage);
        }

        public static void ShouldBeSameAs(
            [NotNullIfNotNull("expected")] this object? actual,
            [NotNullIfNotNull("actual")] object? expected)
        {
            ShouldBeSameAs(actual, expected, () => null);
        }
        public static void ShouldBeSameAs(
            [NotNullIfNotNull("expected")] this object? actual,
            [NotNullIfNotNull("actual")] object? expected,
            string? customMessage)
        {
            ShouldBeSameAs(actual, expected, () => customMessage);
        }
        public static void ShouldBeSameAs(
            [NotNullIfNotNull("expected")] this object? actual,
            [NotNullIfNotNull("actual")] object? expected,
            [InstantHandle] Func<string?>? customMessage)
        {
            actual.AssertAwesomely(v => Is.Same(v, expected), actual, expected, customMessage);
        }

        public static void ShouldNotBeSameAs(this object? actual, object? expected, string? customMessage = null)
        {
            ShouldNotBeSameAs(actual, expected, () => customMessage);
        }
        public static void ShouldNotBeSameAs(this object? actual, object? expected, [InstantHandle] Func<string?>? customMessage)
        {
            actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected, customMessage);
        }
    }
}
