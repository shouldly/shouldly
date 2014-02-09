using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Shouldly
{
    internal class Is
    {
        public static bool NotInRange(IComparable comparable, IComparable @from, IComparable to)
        {
            throw new NotImplementedException();
        }

        public static bool InRange(IComparable comparable, IComparable @from, IComparable to)
        {
            throw new NotImplementedException();
        }

        public static bool Same(object o, object expected)
        {
            throw new NotImplementedException();
        }

        public static bool LessThan(object o, object expected)
        {
            throw new NotImplementedException();
        }

        public static bool GreaterThanOrEqualTo(object o, object expected)
        {
            throw new NotImplementedException();
        }

        public static bool GreaterThan(object actual, object expected)
        {
            throw new NotImplementedException();
        }

        public static bool Equal(IEnumerable<decimal> object1, IEnumerable<decimal> expected, decimal tolerance)
        {
            // Is.EqualTo(expected).Within(tolerance)
            throw new NotImplementedException();
        }

        public static bool Equal(decimal actual, decimal expected, decimal tolerance)
        {
            return Math.Abs(actual - expected) < tolerance;
        }

        public static bool Equal(double actual, double expected, double tolerance)
        {
            return Math.Abs(actual - expected) < tolerance;
        }

        public static bool Equal(IEnumerable<float> object1, IEnumerable<float> expected, double tolerance)
        {
            // Is.EqualTo(expected).Within(tolerance)
            throw new NotImplementedException();
        }

        public static bool Equal(IEnumerable<double> object1, IEnumerable<double> expected, double tolerance)
        {
            // Is.EqualTo(expected).Within(tolerance)
            throw new NotImplementedException();
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
    }
}