using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using System;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeTestExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected)
        {
            actual.AssertAwesomely(Is.EqualTo(expected), actual, expected);
        }

        public static void ShouldBeTypeOf<T>(this object actual)
        {
            ShouldBeTypeOf(actual, typeof(T));
        }

        public static void ShouldBeTypeOf(this object actual, Type expected)
        {
            actual.AssertAwesomely(Is.InstanceOf(expected), actual.GetType(), expected);
        }

        public static void ShouldNotBeTypeOf<T>(this object actual)
        {
            ShouldNotBeTypeOf(actual, typeof(T));
        }

        public static void ShouldNotBeTypeOf(this object actual, Type expected)
        {
            actual.AssertAwesomely(!Is.InstanceOf(expected), actual, expected);
        }

        public static void ShouldNotBe<T>(this T actual, T expected)
        {
            actual.AssertAwesomely(Is.Not.EqualTo(expected), actual, expected);
        }

        public static void ShouldBe(this float actual, float expected, double tolerance)
        {
            actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }

        public static void ShouldBe(this IEnumerable<double> actual, IEnumerable<double> expected, double tolerance)
        {
            actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }

        public static void ShouldBe(this IEnumerable<float> actual, IEnumerable<float> expected, double tolerance)
        {
            actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }

        public static void ShouldBe(this double actual, double expected, double tolerance)
        {
            actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }

        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance)
        {
            actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }

        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance)
        {
            actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }

        public static void ShouldBeGreaterThan(this object actual, object expected)
        {
            actual.AssertAwesomely(Is.GreaterThan(expected), actual, expected);
        }

        public static void ShouldBeGreaterThanOrEqualTo(this object actual, object expected)
        {
            actual.AssertAwesomely(Is.GreaterThanOrEqualTo(expected), actual, expected);
        }

        public static void ShouldBeLessThan(this object actual, object expected)
        {
            actual.AssertAwesomely(Is.LessThan(expected), actual, expected);
        }

        public static void ShouldBeSameAs(this object actual, object expected)
        {
            actual.AssertAwesomely(Is.SameAs(expected), actual, expected);
        }

        public static void ShouldNotBeSameAs(this object actual, object expected)
        {
            actual.AssertAwesomely(Is.Not.SameAs(expected), actual, expected);
        }
    }
}