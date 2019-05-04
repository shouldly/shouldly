using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Shouldly
{
    internal static class Is
    {
        public static bool InRange<T>(T comparable, T @from, T to) where T : IComparable<T>
        {
            return comparable.CompareTo(from) >= 0 && comparable.CompareTo(to) <= 0;
        }

        public static bool Same(object actual, object expected)
        {
            if (actual == null && expected == null)
                return true;
            if (actual == null || expected == null)
                return false;

            return ReferenceEquals(actual, expected);
        }

        public static bool Equal<T>(T expected, T actual)
        {
            return Equal(expected, actual, GetEqualityComparer<T>());
        }

        public static bool Equal<T>(T expected, T actual, IEqualityComparer<T> comparer)
        {
            return comparer.Equals(actual, expected);
        }

        static IEqualityComparer<T> GetEqualityComparer<T>(IEqualityComparer innerComparer = null)
        {
            return new EqualityComparer<T>(innerComparer);
        }

        public static bool Equal<T>(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            if (actual == null && expected == null)
                return true;
            if (actual == null || expected == null)
                return false;

            var expectedEnum = expected.GetEnumerator();
            var actualEnum = actual.GetEnumerator();

            for (; ; )
            {
                var expectedHasData = expectedEnum.MoveNext();
                var actualHasData = actualEnum.MoveNext();

                if (!expectedHasData && !actualHasData)
                    return true;

                if (expectedHasData != actualHasData || !Equal(actualEnum.Current, expectedEnum.Current))
                {
                    return false;
                }
            }
        }

        public static bool EqualIgnoreOrder<T>(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            if (actual == null && expected == null)
                return true;
            
            if (actual == null || expected == null)
                return false;

            if (actual is ICollection actualCollection && expected is ICollection && actualCollection.Count != ((ICollection)expected).Count)
                return false;

            var expectedList = expected.ToList();
            foreach (var actualElement in actual)
            {
                var match = expectedList.FirstOrDefault(x => Equal(x, actualElement));
                if (!expectedList.Remove(match))
                    return false;
            }

            return !expectedList.Any();
        }

        public static bool Equal(IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance)
        {
            var expectedEnum = expected.GetEnumerator();
            var actualEnum = actual.GetEnumerator();

            for (; ; )
            {
                var expectedHasData = expectedEnum.MoveNext();
                var actualHasData = actualEnum.MoveNext();

                if (!expectedHasData && !actualHasData)
                    return true;

                if (expectedHasData != actualHasData || !Equal(actualEnum.Current, expectedEnum.Current, tolerance))
                {
                    return false;
                }
            }
        }

        public static bool Equal(IEnumerable<float> actual, IEnumerable<float> expected, double tolerance)
        {
            var expectedEnum = expected.GetEnumerator();
            var actualEnum = actual.GetEnumerator();

            for (; ; )
            {
                var expectedHasData = expectedEnum.MoveNext();
                var actualHasData = actualEnum.MoveNext();

                if (!expectedHasData && !actualHasData)
                    return true;

                if (expectedHasData != actualHasData || !Equal(actualEnum.Current, expectedEnum.Current, tolerance))
                {
                    return false;
                }
            }
        }

        public static bool Equal(IEnumerable<double> actual, IEnumerable<double> expected, double tolerance)
        {
            var expectedEnum = expected.GetEnumerator();
            var actualEnum = actual.GetEnumerator();

            for (; ; )
            {
                var expectedHasData = expectedEnum.MoveNext();
                var actualHasData = actualEnum.MoveNext();

                if (!expectedHasData && !actualHasData)
                    return true;

                if (expectedHasData != actualHasData || !Equal(actualEnum.Current, expectedEnum.Current, tolerance))
                {
                    return false;
                }
            }
        }

        public static bool Equal(decimal actual, decimal expected, decimal tolerance)
        {
            return Math.Abs(actual - expected) < tolerance;
        }

        public static bool Equal(double actual, double expected, double tolerance)
        {
            return Math.Abs(actual - expected) < tolerance;
        }

        public static bool Equal(DateTime actual, DateTime expected, TimeSpan tolerance)
        {
            return (actual - expected).Duration() < tolerance;
        }

        public static bool Equal(DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance)
        {
            return (actual - expected).Duration() < tolerance;
        }

        public static bool Equal(TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
        {
            return (actual - expected).Duration() < tolerance;
        }

        public static bool InstanceOf(object o, Type expected)
        {
            return o != null ? expected.IsInstanceOfType(o) : false; 
        }

        public static bool StringMatchingRegex(string actual, string regexPattern)
        {
            return Regex.IsMatch(actual, regexPattern);
        }

        public static bool StringContainingIgnoreCase(string actual, string expected)
        {
            if (actual == null)
                return false;

            return actual.IndexOf(expected, StringComparison.OrdinalIgnoreCase) != -1;
        }

        public static bool StringContainingUsingCaseSensitivity(string actual, string expected)
        {
            if (actual == null)
                return false;

            return actual.IndexOf(expected, StringComparison.Ordinal) != -1;
        }


        public static bool EndsWithUsingCaseSensitivity(string actual, string expected, Case caseSensitivity)
        {
            if (actual == null)
                return false;

            if (caseSensitivity == Case.Insensitive)
            {
                return actual.EndsWith(expected, StringComparison.OrdinalIgnoreCase);
            }

            return actual.EndsWith(expected);
        }

        public static bool StringStartingWithUsingCaseSensitivity(string actual, string expected, Case caseSensitivity)
        {
            if (actual == null)
                return false;

            if (caseSensitivity == Case.Insensitive)
            {
                return actual.StartsWith(expected, StringComparison.OrdinalIgnoreCase);
            }

            return actual.StartsWith(expected);
        }

        public static bool StringEqualWithCaseSensitivity(string actual, string expected, Case caseSensitivity)
        {
            if (caseSensitivity == Case.Insensitive)
            {
                return StringComparer.OrdinalIgnoreCase.Equals(actual, expected);
            }

            return StringComparer.Ordinal.Equals(actual, expected);
        }

        public static bool EnumerableStringEqualWithCaseSensitivity(IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity)
        {
            var expectedEnum = expected.GetEnumerator();
            var actualEnum = actual.GetEnumerator();

            for (; ; )
            {
                var expectedHasData = expectedEnum.MoveNext();
                var actualHasData = actualEnum.MoveNext();

                var bothListsProcessed = !expectedHasData && !actualHasData;
                if (bothListsProcessed)
                    return true;

                var listsHaveDifferentLengths = !expectedHasData || !actualHasData;
                if (listsHaveDifferentLengths)
                    return false;

                if (!StringEqualWithCaseSensitivity(actualEnum.Current, expectedEnum.Current, caseSensitivity))
                {
                    return false;
                }
            }
        }

        public static bool GreaterThanOrEqualTo<T>(IComparable<T> comparable, T expected)
        {
            return Compare(comparable, expected) >= 0;
        }

        public static bool GreaterThanOrEqualTo<T>(T actual, T expected, IComparer<T> comparer)
        {
            return Compare(actual, expected, comparer) >= 0;
        }

        public static bool LessThanOrEqualTo<T>(IComparable<T> comparable, T expected)
        {
            return Compare(comparable, expected) <= 0;
        }

        public static bool LessThanOrEqualTo<T>(T actual, T expected, IComparer<T> comparer)
        {
            return Compare(actual, expected, comparer) <= 0;
        }

        public static bool GreaterThan<T>(IComparable<T> comparable, T expected)
        {
            return Compare(comparable, expected) > 0;
        }

        public static bool GreaterThan<T>(T actual, T expected, IComparer<T> comparer)
        {
            return Compare(actual, expected, comparer) > 0;
        }

        public static bool LessThan<T>(IComparable<T> comparable, T expected)
        {
            return Compare(comparable, expected) < 0;
        }

        public static bool LessThan<T>(T actual, T expected, IComparer<T> comparer)
        {
            return Compare(actual, expected, comparer) < 0;
        }

        static decimal Compare<T>(T actual, T expected, IComparer<T> comparer)
        {
            return comparer.Compare(actual, expected);
        }
        static decimal Compare<T>(IComparable<T> comparable, T expected)
        {
            if (!typeof(T).IsValueType())
            {
                // ReSharper disable CompareNonConstrainedGenericWithNull
                if (comparable == null)
                    return expected == null ? 0 : -1;
                if (expected == null)
                    return +1;
                // ReSharper restore CompareNonConstrainedGenericWithNull
            }

            return comparable.CompareTo(expected);
        }
    }
}