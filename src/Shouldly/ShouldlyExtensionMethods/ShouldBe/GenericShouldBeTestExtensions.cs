﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeTestExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected)
        {
            ShouldBe(actual, expected, () => null);
        }
        public static void ShouldBe<T>(this T actual, T expected, string customMessage)
        {
            ShouldBe(actual, expected, () => customMessage);
        }
        public static void ShouldBe<T>(this T actual, T expected, Func<string> customMessage)
        {
            if (ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName) || typeof(T) == typeof(string))
                actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<T>()), actual, expected, customMessage);
            else
                actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage);
        }

        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected)
        {
            ShouldNotBe(actual, expected, () => null);
        }
        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, string customMessage)
        {
            ShouldNotBe(actual, expected, () => customMessage);
        }
        [ContractAnnotation("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, Func<string> customMessage )
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder = false)
        {
            ShouldBe(actual, expected, ignoreOrder, () => null);
        }
        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder, string customMessage)
        {
            ShouldBe(actual, expected, ignoreOrder, () => customMessage);
        }
        public static void ShouldBe<T>(this IEnumerable<T> actual, IEnumerable<T> expected, bool ignoreOrder, Func<string> customMessage)
        {
            if (!ignoreOrder && ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName))
            {
                actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<IEnumerable<T>>()), actual, expected, customMessage);
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

        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance)
        {
            ShouldBe(actual, expected, tolerance, () => null);
        }
        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, string customMessage)
        {
            ShouldBe(actual, expected, tolerance, () => customMessage);
        }
        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
        }

        public static void ShouldBeSameAs(this object actual, object expected)
        {
            ShouldBeSameAs(actual, expected, () => null);
        }
        public static void ShouldBeSameAs(this object actual, object expected, string customMessage)
        {
            ShouldBeSameAs(actual, expected, () => customMessage);
        }
        public static void ShouldBeSameAs(this object actual, object expected, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.Same(v, expected), actual, expected, customMessage);
        }

        public static void ShouldNotBeSameAs(this object actual, object expected)
        {
            ShouldNotBeSameAs(actual, expected, () => null);
        }
        public static void ShouldNotBeSameAs(this object actual, object expected, string customMessage)
        {
            ShouldNotBeSameAs(actual, expected, () => customMessage);
        }
        public static void ShouldNotBeSameAs(this object actual, object expected, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected, customMessage);
        }
    }
}
