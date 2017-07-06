using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        public static T ShouldBeAssignableTo<T>(this object actual)
        {
            return ShouldBeAssignableTo<T>(actual, () => null);
        }

        public static T ShouldBeAssignableTo<T>(this object actual, string customMessage)
        {
            return ShouldBeAssignableTo<T>(actual, () => customMessage);
        }

        public static T ShouldBeAssignableTo<T>(this object actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeAssignableTo(actual, typeof(T), customMessage);
            return (T)actual;
        }

        public static void ShouldBeAssignableTo(this object actual, Type expected)
        {
            ShouldBeAssignableTo(actual, expected, () => null);
        }

        public static void ShouldBeAssignableTo(this object actual, Type expected, string customMessage)
        {
            ShouldBeAssignableTo(actual, expected, () => customMessage);
        }

        public static void ShouldBeAssignableTo(this object actual, Type expected, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomely(v =>
            {
                if (actual == null && !expected.IsValueType())
                    return true;

                return Is.InstanceOf(v, expected);
            }, actual, expected, customMessage);
        }

        public static T ShouldBeOfType<T>(this object actual)
        {
            return ShouldBeOfType<T>(actual, () => null);
        }

        public static T ShouldBeOfType<T>(this object actual, string customMessage)
        {
            return ShouldBeOfType<T>(actual, () => customMessage);
        }

        public static T ShouldBeOfType<T>(this object actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeOfType(actual, typeof(T), customMessage);
            return (T)actual;
        }

        public static void ShouldBeOfType(this object actual, Type expected)
        {
            ShouldBeOfType(actual, expected, () => null);
        }

        public static void ShouldBeOfType(this object actual, Type expected, string customMessage)
        {
            ShouldBeOfType(actual, expected, () => customMessage);
        }

        public static void ShouldBeOfType(this object actual, Type expected, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomely(v => v != null && v.GetType() == expected, actual, expected, customMessage);
        }

        public static void ShouldNotBeAssignableTo<T>(this object actual)
        {
            ShouldNotBeAssignableTo<T>(actual, () => null);
        }

        public static void ShouldNotBeAssignableTo<T>(this object actual, string customMessage)
        {
            ShouldNotBeAssignableTo<T>(actual, () => customMessage);
        }

        public static void ShouldNotBeAssignableTo<T>(this object actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldNotBeAssignableTo(actual, typeof(T), customMessage);
        }

        public static void ShouldNotBeAssignableTo(this object actual, Type expected)
        {
            ShouldNotBeAssignableTo(actual, expected, () => null);
        }

        public static void ShouldNotBeAssignableTo(this object actual, Type expected, string customMessage)
        {
            ShouldNotBeAssignableTo(actual, expected, () => customMessage);
        }

        public static void ShouldNotBeAssignableTo(this object actual, Type expected, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.InstanceOf(v, expected), actual, expected, customMessage);
        }

        public static void ShouldNotBeOfType<T>(this object actual)
        {
            ShouldNotBeOfType<T>(actual, () => null);
        }

        public static void ShouldNotBeOfType<T>(this object actual, string customMessage)
        {
            ShouldNotBeOfType<T>(actual, () => customMessage);
        }

        public static void ShouldNotBeOfType<T>(this object actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldNotBeOfType(actual, typeof(T), customMessage);
        }

        public static void ShouldNotBeOfType(this object actual, Type expected)
        {
            ShouldNotBeOfType(actual, expected, () => null);
        }

        public static void ShouldNotBeOfType(this object actual, Type expected, string customMessage)
        {
            ShouldNotBeOfType(actual, expected, () => customMessage);
        }

        public static void ShouldNotBeOfType(this object actual, Type expected, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomely(v => v == null || v.GetType() != expected, actual, expected, customMessage);
        }
    }
}