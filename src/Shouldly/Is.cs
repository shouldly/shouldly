using System;
using System.Collections.Generic;

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

        public static bool GreaterThan(object o, object expected)
        {
            throw new NotImplementedException();
        }

        public static bool Equal(IEnumerable<decimal> object1, IEnumerable<decimal> expected, decimal tolerance)
        {
            // Is.EqualTo(expected).Within(tolerance)
            throw new NotImplementedException();
        }

        public static bool Equal(decimal object1, decimal expected, decimal tolerance)
        {
            // Is.EqualTo(expected).Within(tolerance)
            throw new NotImplementedException();
        }

        public static bool Equal(double object1, double expected, double tolerance)
        {
            // Is.EqualTo(expected).Within(tolerance)
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static bool StringMatchingRegex(string s, string regexPattern)
        {
            throw new NotImplementedException();
        }

        public static bool StringContainingIgnoreCase(string s, string expected)
        {
            throw new NotImplementedException();
        }

        public static bool EndsWithIgnoringCase(string s, string expected)
        {
            throw new NotImplementedException();
        }

        public static bool StringStartingWithIgnoreCase(string s, string expected)
        {
            throw new NotImplementedException();
        }

        public static bool StringEqualIgnoreCase(string s, string expected)
        {
            throw new NotImplementedException();
        }

        public static bool StringEqual(string s, string expected)
        {
            throw new NotImplementedException();
        }

        public static bool Equal(object elementAt, object expected)
        {
            throw new NotImplementedException();
        }
    }
}