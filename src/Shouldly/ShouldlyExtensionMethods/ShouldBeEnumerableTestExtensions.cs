using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeEnumerableTestExtensions
    {
        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected)
        {
            ShouldContain(actual, expected, () => null);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, string customMessage)
        {
            ShouldContain(actual, expected, () => customMessage);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, [InstantHandle] Func<string> customMessage)
        {
            if (!actual.Contains(expected))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected)
        {
            ShouldNotContain(actual, expected, () => null);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, string customMessage)
        {
            ShouldNotContain(actual, expected, () => customMessage);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, [InstantHandle] Func<string> customMessage)
        {
            if (actual.Contains(expected))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate)
        {
            ShouldContain(actual, elementPredicate, () => null);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string customMessage)
        {
            ShouldContain(actual, elementPredicate, () => customMessage);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, [InstantHandle] Func<string> customMessage)
        {
            var condition = elementPredicate.Compile();
            if (!actual.Any(condition))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(elementPredicate.Body, customMessage).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate)
        {
            ShouldNotContain(actual, elementPredicate, () => null);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string customMessage)
        {
            ShouldNotContain(actual, elementPredicate, () => customMessage);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, [InstantHandle] Func<string> customMessage)
        {
            var condition = elementPredicate.Compile();
            if (actual.Any(condition))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(elementPredicate.Body, customMessage).ToString());
        }

        public static void ShouldAllBe<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate)
        {
            ShouldAllBe(actual, elementPredicate, () => null);
        }

        public static void ShouldAllBe<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string customMessage)
        {
            ShouldAllBe(actual, elementPredicate, () => customMessage);
        }

        public static void ShouldAllBe<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, [InstantHandle] Func<string> customMessage)
        {
            var condition = elementPredicate.Compile();
            var actualResults = actual.Where(part => !condition(part));
            if (actualResults.Any())
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(elementPredicate.Body, actualResults, customMessage).ToString());
        }

        public static void ShouldBeEmpty<T>(this IEnumerable<T> actual)
        {
            ShouldBeEmpty(actual, () => null);
        }

        public static void ShouldBeEmpty<T>(this IEnumerable<T> actual, string customMessage)
        {
            ShouldBeEmpty(actual, () => customMessage);
        }

        public static void ShouldBeEmpty<T>(this IEnumerable<T> actual, [InstantHandle] Func<string> customMessage)
        {
            if (actual == null || (actual != null && actual.Count() != 0))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }

        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> actual)
        {
            ShouldNotBeEmpty(actual, () => null);
        }

        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> actual, string customMessage)
        {
            ShouldNotBeEmpty(actual, () => customMessage);
        }

        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> actual, [InstantHandle] Func<string> customMessage)
        {
            if (actual == null || actual != null && !actual.Any())
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }

        public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance)
        {
            ShouldContain(actual, expected, tolerance, () => null);
        }

        public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance, string customMessage)
        {
            ShouldContain(actual, expected, tolerance, () => customMessage);
        }

        public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance, [InstantHandle] Func<string> customMessage)
        {
            if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
                throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual, tolerance, customMessage).ToString());
        }

        public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance)
        {
            ShouldContain(actual, expected, tolerance, () => null);
        }

        public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance, string customMessage)
        {
            ShouldContain(actual, expected, tolerance, () => customMessage);
        }

        public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance, [InstantHandle] Func<string> customMessage)
        {
            if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
                throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual, tolerance, customMessage).ToString());
        }

        public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            ShouldBeSubsetOf(actual, expected, () => null);
        }

        public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected, string customMessage)
        {
            ShouldBeSubsetOf(actual, expected, () => customMessage);
        }

        public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected, [InstantHandle] Func<string> customMessage)
        {
            if (actual.Equals(expected))
                return;

            var missing = actual.Except(expected);
            if (missing.Any())
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, missing, customMessage).ToString());
        }

        public static void ShouldBeUnique<T>(this IEnumerable<T> actual)
        {
            ShouldBeUnique(actual, () => null);
        }

        public static void ShouldBeUnique<T>(this IEnumerable<T> actual, string customMessage)
        {
            ShouldBeUnique(actual, () => customMessage);
        }

        public static void ShouldBeUnique<T>(this IEnumerable<T> actual, [InstantHandle] Func<string> customMessage)
        {
            var duplicates = GetDuplicates(actual);
            if (duplicates.Any())
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(new List<T>(), duplicates, customMessage).ToString());
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