using System;
using System.Diagnostics;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static T? ShouldBeAssignableTo<T>(this object? actual, string? customMessage = null)
        {
            ShouldBeAssignableTo(actual, typeof(T), customMessage);
            return (T?)actual;
        }

        public static void ShouldBeAssignableTo(this object? actual, Type expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v =>
            {
                if (actual == null && !expected.IsValueType())
                    return true;

                return expected.IsInstanceOfType(v);
            }, actual, expected, customMessage);
        }

        public static T ShouldBeOfType<T>([NotNull] this object? actual, string? customMessage = null)
        {
            ShouldBeOfType(actual, typeof(T), customMessage);
            return (T)actual;
        }

        public static void ShouldBeOfType([NotNull] this object? actual, Type expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v => v != null && v.GetType() == expected, actual, expected, customMessage);
            Debug.Assert(actual != null);
        }

        public static void ShouldNotBeAssignableTo<T>(this object? actual, string? customMessage = null)
        {
            ShouldNotBeAssignableTo(actual, typeof(T), customMessage);
        }

        public static void ShouldNotBeAssignableTo(this object? actual, Type expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !expected.IsInstanceOfType(v), actual, expected, customMessage);
        }

        public static void ShouldNotBeOfType<T>(this object? actual, string? customMessage = null)
        {
            ShouldNotBeOfType(actual, typeof(T), customMessage);
        }

        public static void ShouldNotBeOfType(this object? actual, Type expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v => v == null || v.GetType() != expected, actual, expected, customMessage);
        }
    }
}