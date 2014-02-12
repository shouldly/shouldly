using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeTestExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected)
        {
             actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected);
        }
        
        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder = false)
        {
             actual.AssertAwesomely(v => Is.Equal(v, expected, ignoreOrder), actual, expected);
        }

        public static T ShouldBeTypeOf<T>(this object actual)
        {
            ShouldBeTypeOf(actual, typeof(T));
            return (T)actual;
        }

        public static void ShouldBeTypeOf(this object actual, Type expected)
        {
            actual.AssertAwesomely(v=> Is.InstanceOf(v, expected), actual.GetType(), expected);
        }

        public static void ShouldNotBeTypeOf<T>(this object actual)
        {
            ShouldNotBeTypeOf(actual, typeof(T));
        }

        public static void ShouldNotBeTypeOf(this object actual, Type expected)
        {
            actual.AssertAwesomely(v => !Is.InstanceOf(v, expected), actual, expected);
        }

        public static void ShouldNotBe<T>(this T actual, T expected)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected);
        }

        public static void ShouldBe(this float actual, float expected, double tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected);
        }

        public static void ShouldBe(this IEnumerable<double> actual, IEnumerable<double> expected, double tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected);
        }

        public static void ShouldBe(this IEnumerable<float> actual, IEnumerable<float> expected, double tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected);
        }

        public static void ShouldBe(this double actual, double expected, double tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected);
        }

        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected);
        }

        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected);
        }

        public static void ShouldBeGreaterThan<T>(this T actual, T expected) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected);
        }

        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.GreaterThanOrEqualTo(v, expected), actual, expected);
        }

        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.LessThanOrEqualTo(v, expected), actual, expected);
        }

        public static void ShouldBeSameAs(this object actual, object expected)
        {
            actual.AssertAwesomely(v => Is.Same(v, expected), actual, expected);
        }

        public static void ShouldNotBeSameAs(this object actual, object expected)
        {
            actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected);
        }

        public static void ShouldBeOneOf<T>(this T actual, params T[] expected)
        {
            if (!expected.Contains(actual))
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldNotBeOneOf<T>(this T actual, params T[] expected)
        {
            if (expected.Contains(actual))
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldBeInRange<T>(this T actual, T from, T to) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.InRange(v, from, to), actual, new {from, to});
        }

        public static void ShouldNotBeInRange<T>(this T actual, T from, T to) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => !Is.InRange(v, from, to), actual, new {from, to});
        }
    }
}
