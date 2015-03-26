using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeTypeTestExtensions
    {
        public static T ShouldBeAssignableTo<T>(this object actual)
        {
            ShouldBeAssignableTo(actual, typeof(T));
            return (T)actual;
        }

        public static void ShouldBeAssignableTo(this object actual, Type expected)
        {
            actual.AssertAwesomely(v => Is.InstanceOf(v, expected), actual == null ? null : actual.GetType(), expected);
        }

        public static T ShouldBeOfType<T>(this object actual)
        {
            ShouldBeOfType(actual, typeof(T));
            return (T)actual;
        }

        public static void ShouldBeOfType(this object actual, Type expected)
        {
            actual.AssertAwesomely(v => v != null && v.GetType() == expected, actual == null ? null : actual.GetType(), expected);
        }

        public static void ShouldNotBeAssignableTo<T>(this object actual)
        {
            ShouldNotBeAssignableTo(actual, typeof(T));
        }

        public static void ShouldNotBeAssignableTo(this object actual, Type expected)
        {
            actual.AssertAwesomely(v => !Is.InstanceOf(v, expected), actual, expected);
        }

        public static void ShouldNotBeOfType<T>(this object actual)
        {
            ShouldNotBeOfType(actual, typeof(T));
        }

        public static void ShouldNotBeOfType(this object actual, Type expected)
        {
            actual.AssertAwesomely(v => v == null || v.GetType() != expected, actual, expected);
        } 
    }
}