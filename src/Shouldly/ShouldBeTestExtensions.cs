using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using JetBrains.Annotations;

namespace Shouldly
{
    //[DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeTestExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected, Func<string> customMessage = null)
        {
            if (ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName) || typeof(T) == typeof(string))
                actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<T>()), actual, expected, customMessage);
            else 
                actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder = false)
        {
            if (!ignoreOrder && ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName))
            {
                actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<IEnumerable<T>>()), actual, expected);
            }
            else
            {
                if (ignoreOrder)
                {
                    actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected), actual, expected);
                }
                else
                {
                    actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected);
                }
            }
        }

        public static void ShouldBe(this float actual, float expected, double tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this IEnumerable<double> actual, IEnumerable<double> expected, double tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this IEnumerable<float> actual, IEnumerable<float> expected, double tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this double actual, double expected, double tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this DateTime actual, DateTime expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected);
        }

        public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldNotBeOneOf<T>(this T actual, params T[] expected)
        {
            if (expected.Contains(actual))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldBeInRange<T>(this T actual, T from, T to) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => Is.InRange(v, from, to), actual, new { from, to });
        }

        public static void ShouldNotBeInRange<T>(this T actual, T from, T to) where T : IComparable<T>
        {
            actual.AssertAwesomely(v => !Is.InRange(v, from, to), actual, new { from, to });
        }
    }
}
