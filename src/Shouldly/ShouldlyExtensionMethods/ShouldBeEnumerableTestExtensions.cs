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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage));
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, [NotNull] IEqualityComparer<T> comparer)
        {
            ShouldContain(actual, expected, comparer, () => null);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, [NotNull] IEqualityComparer<T> comparer, string customMessage)
        {
            ShouldContain(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, [NotNull] IEqualityComparer<T> comparer, [InstantHandle] Func<string> customMessage)
        {
            comparer.ShouldNotBeNull();
            if (!actual.Contains(expected, comparer))
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage));
            }
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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage));
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, [NotNull] IEqualityComparer<T> comparer)
        {
            ShouldNotContain(actual, expected, comparer, () => null);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, [NotNull] IEqualityComparer<T> comparer, string customMessage)
        {
            ShouldNotContain(actual, expected, comparer, () => customMessage);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, [NotNull] IEqualityComparer<T> comparer, [InstantHandle] Func<string> customMessage)
        {
            comparer.ShouldNotBeNull();
            if (actual.Contains(expected, comparer))
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage));
            }
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate)
        {
            ShouldContain(actual, elementPredicate, () => null);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, int expectedCount)
        {
            ShouldContain(actual, elementPredicate, expectedCount, () => null);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, int expectedCount, string customMessage)
        {
            ShouldContain(actual, elementPredicate, expectedCount, () => customMessage);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, int expectedCount, Func<string> customMessage)
        {
            var condition = elementPredicate.Compile();
            var actualCount = actual.Count(condition);
            if (actualCount != expectedCount)
            {
                throw new ShouldAssertException(new ShouldContainWithCountShouldlyMessage(elementPredicate.Body, actual, expectedCount, customMessage));
            }
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string customMessage)
        {
            ShouldContain(actual, elementPredicate, () => customMessage);
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, [InstantHandle] Func<string> customMessage)
        {
            var condition = elementPredicate.Compile();
            if (!actual.Any(condition))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(elementPredicate.Body, actual, customMessage));
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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(elementPredicate.Body, actual, customMessage));
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
                throw new ShouldAssertException(new ActualFilteredWithPredicateShouldlyMessage(elementPredicate.Body, actualResults, actual, customMessage));
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
            if (actual == null || actual.Any())
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage));
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
            if (actual == null || !actual.Any())
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage));
        }

        public static T ShouldHaveSingleItem<T>(this IEnumerable<T> actual)
        {
            return ShouldHaveSingleItem(actual, () => null);
        }

        public static T ShouldHaveSingleItem<T>(this IEnumerable<T> actual, string customMessage)
        {
            return ShouldHaveSingleItem(actual, () => customMessage);
        }

        public static T ShouldHaveSingleItem<T>(this IEnumerable<T> actual, [InstantHandle] Func<string> customMessage)
        {
            if (actual == null || actual.Count() != 1)
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage));

            return actual.Single();
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
                throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual, tolerance, customMessage));
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
                throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual, tolerance, customMessage));
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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage));
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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(actual, duplicates, customMessage));
        }

        public static void ShouldBe(this IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity)
        {
            ShouldBe(actual, expected, caseSensitivity, () => null);
        }

        public static void ShouldBe(this IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity, string customMessage)
        {
            ShouldBe(actual, expected, caseSensitivity, () => customMessage);
        }

        public static void ShouldBe(this IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomelyWithCaseSensitivity(
                v => Is.EnumerableStringEqualWithCaseSensitivity(v, expected, caseSensitivity),
                actual,
                expected,
                caseSensitivity,
                customMessage);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual)
        {
            ShouldBeInOrder(actual, SortDirection.Ascending);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, string customMessage)
        {
            ShouldBeInOrder(actual, SortDirection.Ascending, customMessage);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeInOrder(actual, SortDirection.Ascending, customMessage);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection)
        {
            ShouldBeInOrder(actual, expectedSortDirection, (IComparer<T>)null);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, string customMessage)
        {
            ShouldBeInOrder(actual, expectedSortDirection, null, customMessage);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeInOrder(actual, expectedSortDirection, (IComparer<T>)null, customMessage);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, IComparer<T> customComparer)
        {
            ShouldBeInOrder(actual, expectedSortDirection, customComparer, () => null);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, IComparer<T> customComparer, string customMessage)
        {
            ShouldBeInOrder(actual, expectedSortDirection, customComparer, () => customMessage);
        }

        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, IComparer<T> customComparer, [InstantHandle] Func<string> customMessage)
        {
            if (customComparer == null)
                customComparer = Comparer<T>.Default;

            var isOutOfOrder = expectedSortDirection == SortDirection.Ascending
                ? (Func<int, bool>)
                  (r => r > 0)   // If 'ascending', the previous value should never be greater than the current value
                : (r => r < 0);  // If 'descending', the previous value should never be less than the current value

            ShouldBeInOrder(actual, expectedSortDirection, (x, y) => isOutOfOrder(customComparer.Compare(x, y)), customMessage);
        }

        private static List<object> GetDuplicates<T>(IEnumerable<T> items)
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

        private static void ShouldBeInOrder<T>(IEnumerable<T> actual, SortDirection expectedSortDirection, Func<T, T, bool> isOutOfOrder, Func<string> customMessage)
        {
            var previousItem = default(T);
            var currentIndex = -1;

            foreach (var currentItem in actual)
            {
                if (++currentIndex > 0 // We only need to start comparing once we've passed the first item in the list
                    && isOutOfOrder(previousItem, currentItem))
                {
                    throw new ShouldAssertException(
                        new ExpectedOrderShouldlyMessage(actual, expectedSortDirection, currentIndex, currentItem, customMessage));
                }

                previousItem = currentItem;
            }
        }
    }

}