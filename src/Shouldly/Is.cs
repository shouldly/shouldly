using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Shouldly
{
    internal class Is
    {
        public static bool InRange(IComparable comparable, IComparable @from, IComparable to)
        {
            throw new NotImplementedException();
        }

        public static bool InRange<T>(IComparable<T> comparable, T @from, T to)
        {
            throw new NotImplementedException();
        }

        public static bool Same(object actual, object expected)
        {
            if (actual == null && expected == null)
                return true;
            if (actual == null || expected == null)
                return false;

            return ReferenceEquals(actual, expected);
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

        public static bool InstanceOf(object o, Type expected)
        {
            if (o == null)
                return false;
            return expected.IsInstanceOfType(o);
        }

        public static bool StringMatchingRegex(string actual, string regexPattern)
        {
            return Regex.IsMatch(actual, regexPattern);
        }

        public static bool StringContainingIgnoreCase(string actual, string expected)
        {
            if (actual == null)
                return false;

            return actual.IndexOf(expected, StringComparison.InvariantCultureIgnoreCase) != -1;
        }

        public static bool EndsWithIgnoringCase(string actual, string expected)
        {
            if (actual == null)
                return false;

            return actual.EndsWith(expected, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool StringStartingWithIgnoreCase(string actual, string expected)
        {
            if (actual == null)
                return false;

            return actual.StartsWith(expected, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool StringEqualIgnoreCase(string actual, string expected)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals(actual, expected);
        }

        public static bool Equal(object actual, object expected)
        {
            if (actual == null && expected == null)
                return true;
            if (actual == null || expected == null)
                return false;

            return actual.Equals(expected);
        }

        public static bool GreaterThanOrEqualTo<T>(IComparable<T> comparable, T expected)
        {
            return Compare(comparable, expected) >= 0;
        }

        public static bool LessThanOrEqualTo<T>(IComparable<T> comparable, T expected)
        {
            return Compare(comparable, expected) <= 0;
        }

        public static bool GreaterThan<T>(IComparable<T> comparable, T expected)
        {
            return Compare(comparable, expected) > 0;
        }

        public static bool LessThan<T>(IComparable<T> comparable, T expected)
        {
            return Compare(comparable, expected) < 0;
        }

        private static decimal Compare<T>(IComparable<T> comparable, T expected)
        {
            if (!typeof (T).IsValueType)
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