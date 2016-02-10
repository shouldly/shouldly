using System;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class ShouldBeTestExtensions
    {
        /// <summary>
        /// decimal
        /// </summary>
        public static void ShouldBePositive(this decimal actual)
        {
            ShouldBePositive(actual, () => null);
        }

        public static void ShouldBePositive(this decimal actual, string customMessage)
        {
            ShouldBePositive(actual, () => customMessage);
        }

        public static void ShouldBePositive(this decimal actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(decimal);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeNegative(this decimal actual)
        {
            ShouldBeNegative(actual, () => null);
        }

        public static void ShouldBeNegative(this decimal actual, string customMessage)
        {
            ShouldBeNegative(actual, () => customMessage);
        }

        public static void ShouldBeNegative(this decimal actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(decimal);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// double
        /// </summary>
        public static void ShouldBePositive(this double actual)
        {
            ShouldBePositive(actual, () => null);
        }

        public static void ShouldBePositive(this double actual, string customMessage)
        {
            ShouldBePositive(actual, () => customMessage);
        }

        public static void ShouldBePositive(this double actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(double);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeNegative(this double actual)
        {
            ShouldBeNegative(actual, () => null);
        }

        public static void ShouldBeNegative(this double actual, string customMessage)
        {
            ShouldBeNegative(actual, () => customMessage);
        }

        public static void ShouldBeNegative(this double actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(double);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// float
        /// </summary>
        public static void ShouldBePositive(this float actual)
        {
            ShouldBePositive(actual, () => null);
        }

        public static void ShouldBePositive(this float actual, string customMessage)
        {
            ShouldBePositive(actual, () => customMessage);
        }

        public static void ShouldBePositive(this float actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(float);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeNegative(this float actual)
        {
            ShouldBeNegative(actual, () => null);
        }

        public static void ShouldBeNegative(this float actual, string customMessage)
        {
            ShouldBeNegative(actual, () => customMessage);
        }

        public static void ShouldBeNegative(this float actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(float);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// int
        /// </summary>
        public static void ShouldBePositive(this int actual)
        {
            ShouldBePositive(actual, () => null);
        }

        public static void ShouldBePositive(this int actual, string customMessage)
        {
            ShouldBePositive(actual, () => customMessage);
        }

        public static void ShouldBePositive(this int actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(int);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeNegative(this int actual)
        {
            ShouldBeNegative(actual, () => null);
        }

        public static void ShouldBeNegative(this int actual, string customMessage)
        {
            ShouldBeNegative(actual, () => customMessage);
        }

        public static void ShouldBeNegative(this int actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(int);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// long
        /// </summary>
        public static void ShouldBePositive(this long actual)
        {
            ShouldBePositive(actual, () => null);
        }

        public static void ShouldBePositive(this long actual, string customMessage)
        {
            ShouldBePositive(actual, () => customMessage);
        }

        public static void ShouldBePositive(this long actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(long);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeNegative(this long actual)
        {
            ShouldBeNegative(actual, () => null);
        }

        public static void ShouldBeNegative(this long actual, string customMessage)
        {
            ShouldBeNegative(actual, () => customMessage);
        }

        public static void ShouldBeNegative(this long actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(long);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// short
        /// </summary>
        public static void ShouldBePositive(this short actual)
        {
            ShouldBePositive(actual, () => null);
        }

        public static void ShouldBePositive(this short actual, string customMessage)
        {
            ShouldBePositive(actual, () => customMessage);
        }

        public static void ShouldBePositive(this short actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(short);
            actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
        }

        public static void ShouldBeNegative(this short actual)
        {
            ShouldBeNegative(actual, () => null);
        }

        public static void ShouldBeNegative(this short actual, string customMessage)
        {
            ShouldBeNegative(actual, () => customMessage);
        }

        public static void ShouldBeNegative(this short actual, [InstantHandle] Func<string> customMessage)
        {
            var expected = default(short);
            actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
        }
    }
}