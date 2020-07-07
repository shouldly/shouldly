using System;
using System.Diagnostics;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static T ShouldBeAssignableTo<T>(this object? actual, string? customMessage = null)
        {
            return ShouldBeAssignableTo<T>(actual, () => customMessage);
        }

        public static T ShouldBeAssignableTo<T>(this object? actual, [InstantHandle] Func<string?>? customMessage)
        {
            ShouldBeAssignableTo(actual, typeof(T), customMessage);
            return (T)actual!;
        }

        public static void ShouldBeAssignableTo(this object? actual, Type expected, string? customMessage = null)
        {
            ShouldBeAssignableTo(actual, expected, () => customMessage);
        }

        public static void ShouldBeAssignableTo(this object? actual, Type expected, [InstantHandle] Func<string?>? customMessage)
        {
            actual.AssertAwesomely(v =>
            {
                if (actual == null && !expected.IsValueType())
                    return true;

                return Is.InstanceOf(v, expected);
            }, actual, expected, customMessage);
        }

        public static T ShouldBeOfType<T>([NotNull] this object? actual, string? customMessage = null)
        {
            return ShouldBeOfType<T>(actual, () => customMessage);
        }

        public static T ShouldBeOfType<T>([NotNull] this object? actual, [InstantHandle] Func<string?>? customMessage)
        {
            ShouldBeOfType(actual, typeof(T), customMessage);
            return (T)actual;
        }

        public static void ShouldBeOfType([NotNull] this object? actual, Type expected, string? customMessage = null)
        {
            ShouldBeOfType(actual, expected, () => customMessage);
        }

        public static void ShouldBeOfType([NotNull] this object? actual, Type expected, [InstantHandle] Func<string?>? customMessage)
        {
            actual.AssertAwesomely(v => v != null && v.GetType() == expected, actual, expected, customMessage);
            Debug.Assert(actual != null);
        }

        public static void ShouldNotBeAssignableTo<T>(this object? actual, string? customMessage = null)
        {
            ShouldNotBeAssignableTo<T>(actual, () => customMessage);
        }

        public static void ShouldNotBeAssignableTo<T>(this object? actual, [InstantHandle] Func<string?>? customMessage)
        {
            ShouldNotBeAssignableTo(actual, typeof(T), customMessage);
        }

        public static void ShouldNotBeAssignableTo(this object? actual, Type expected, string? customMessage = null)
        {
            ShouldNotBeAssignableTo(actual, expected, () => customMessage);
        }

        public static void ShouldNotBeAssignableTo(this object? actual, Type expected, [InstantHandle] Func<string?>? customMessage)
        {
            actual.AssertAwesomely(v => !Is.InstanceOf(v, expected), actual, expected, customMessage);
        }

        public static void ShouldNotBeOfType<T>(this object? actual, string? customMessage = null)
        {
            ShouldNotBeOfType<T>(actual, () => customMessage);
        }

        public static void ShouldNotBeOfType<T>(this object? actual, [InstantHandle] Func<string?>? customMessage)
        {
            ShouldNotBeOfType(actual, typeof(T), customMessage);
        }

        public static void ShouldNotBeOfType(this object? actual, Type expected, string? customMessage = null)
        {
            ShouldNotBeOfType(actual, expected, () => customMessage);
        }

        public static void ShouldNotBeOfType(this object? actual, Type expected, [InstantHandle] Func<string?>? customMessage)
        {
            actual.AssertAwesomely(v => v == null || v.GetType() != expected, actual, expected, customMessage);
        }
    }
}