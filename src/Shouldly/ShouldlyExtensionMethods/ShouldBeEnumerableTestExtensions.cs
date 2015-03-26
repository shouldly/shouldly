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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (actual.Contains(expected))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            if (!actual.Any(condition))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(elementPredicate.Body).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            if (actual.Any(condition))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(elementPredicate.Body).ToString());
        }

        public static void ShouldAllBe<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            var actualResults = actual.Where(part => !condition(part));
            if (actualResults.Any())
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(elementPredicate.Body, actualResults).ToString());
        }

        public static void ShouldBeEmpty<T>(this IEnumerable<T> actual)
        {
            if (actual == null || (actual != null && actual.Count() != 0))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual).ToString());
        }

        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> actual)
        {
            if (actual == null || actual != null && !actual.Any())
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual).ToString());
        }

        public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance)
        {
            if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
                throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual, tolerance).ToString());
        }

        public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance)
        {
            if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
                throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual, tolerance).ToString());
        }

        public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            if (actual.Equals(expected))
                return;

            List<T> actualList = actual.ToList();
            List<T> expectedList = expected.ToList();

            if (!actualList.TrueForAll(element =>
            {
                if (expectedList.Contains(element))
                {
                    expectedList.Remove(element);
                    return true;
                }
                return false;
            }))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(expected).ToString());
        }

        public static void ShouldBeUnique<T>(this IEnumerable<T> actual)
        {
            var duplicates = GetDuplicates(actual);
            if (duplicates.Any())
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(new List<T>(), duplicates).ToString());
        }

        static List<object> GetDuplicates<T>(IEnumerable<T> items)
        {
            var list = new List<object>();
            var duplicates = new List<object>();

            foreach (object o1 in items)
            {
                duplicates.AddRange(list.Where(o2 => o1 != null && o1.Equals(o2)));
                list.Add(o1);
            }

            return duplicates;
        }
    }

}