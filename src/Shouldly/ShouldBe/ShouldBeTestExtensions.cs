using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeTestExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected)
        {
            if (ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName) || typeof(T) == typeof(string))
                actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<T>()), actual, expected);
            else 
                actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected);
        }

        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected);
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

        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance);
        }

        public static void ShouldBeSameAs(this object actual, object expected)
        {
            actual.AssertAwesomely(v => Is.Same(v, expected), actual, expected);
        }

        public static void ShouldNotBeSameAs(this object actual, object expected)
        {
            actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected);
        }
    }
}
