using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeEnumerableTestExtensions
    {
        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (!actual.Contains(expected))
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (actual.Contains(expected))
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            if (!actual.Any(condition))
                throw new ChuckedAWobbly(new ShouldlyMessage(elementPredicate.Body).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            if (actual.Any(condition))
                throw new ChuckedAWobbly(new ShouldlyMessage(elementPredicate.Body).ToString());
        }

        public static void ShouldAllBe<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            if (actual.Any(v => !condition(v)))
                throw new ChuckedAWobbly(new ShouldlyMessage(elementPredicate.Body).ToString());
        }

        public static void ShouldBeEmpty<T>(this IEnumerable<T> actual)
        {
            if (actual == null || (actual != null && actual.Count() != 0))
                throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
        }

        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> actual)
        {
            if (actual == null || actual != null && actual.Count() == 0)
                throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
        }

        public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance) 
        {
            if (actual.Where(a => Math.Abs(expected - a) < tolerance).Count() < 1)
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance) 
        {
            if (actual.Where(a => Math.Abs(expected - a) < tolerance).Count() < 1)
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }
    }

}