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
            ShouldBeGreaterThan(actual, default(decimal), () => null);
        }

        public static void ShouldBePositive(this decimal actual, string customMessage)
        {
            ShouldBeGreaterThan(actual, default(decimal), customMessage);
        }

        public static void ShouldBePositive(this decimal actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeGreaterThan(actual, default(decimal), customMessage);
        }

        public static void ShouldBeNegative(this decimal actual)
        {
            ShouldBeLessThan(actual, default(decimal), () => null);
        }

        public static void ShouldBeNegative(this decimal actual, string customMessage)
        {
            ShouldBeLessThan(actual, default(decimal), customMessage);
        }

        public static void ShouldBeNegative(this decimal actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeLessThan(actual, default(decimal), customMessage);
        }

        /// <summary>
        /// double
        /// </summary>
        public static void ShouldBePositive(this double actual)
        {
            ShouldBeGreaterThan(actual, default(double), () => null);
        }

        public static void ShouldBePositive(this double actual, string customMessage)
        {
            ShouldBeGreaterThan(actual, default(double), customMessage);
        }

        public static void ShouldBePositive(this double actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeGreaterThan(actual, default(double), customMessage);
        }

        public static void ShouldBeNegative(this double actual)
        {
            ShouldBeLessThan(actual, default(double), () => null);
        }

        public static void ShouldBeNegative(this double actual, string customMessage)
        {
            ShouldBeLessThan(actual, default(double), customMessage);
        }

        public static void ShouldBeNegative(this double actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeLessThan(actual, default(double), customMessage);
        }

        /// <summary>
        /// float
        /// </summary>
        public static void ShouldBePositive(this float actual)
        {
            ShouldBeGreaterThan(actual, default(float), () => null);
        }

        public static void ShouldBePositive(this float actual, string customMessage)
        {
            ShouldBeGreaterThan(actual, default(float), customMessage);
        }

        public static void ShouldBePositive(this float actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeGreaterThan(actual, default(float), customMessage);
        }

        public static void ShouldBeNegative(this float actual)
        {
            ShouldBeLessThan(actual, default(float), () => null);
        }

        public static void ShouldBeNegative(this float actual, string customMessage)
        {
            ShouldBeLessThan(actual, default(float), customMessage);
        }

        public static void ShouldBeNegative(this float actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeLessThan(actual, default(float), customMessage);
        }

        /// <summary>
        /// int
        /// </summary>
        public static void ShouldBePositive(this int actual)
        {
            ShouldBeGreaterThan(actual, default(int), () => null);
        }

        public static void ShouldBePositive(this int actual, string customMessage)
        {
            ShouldBeGreaterThan(actual, default(int), customMessage);
        }

        public static void ShouldBePositive(this int actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeGreaterThan(actual, default(int), customMessage);
        }

        public static void ShouldBeNegative(this int actual)
        {
            ShouldBeLessThan(actual, default(int), () => null);
        }

        public static void ShouldBeNegative(this int actual, string customMessage)
        {
            ShouldBeLessThan(actual, default(int), customMessage);
        }

        public static void ShouldBeNegative(this int actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeLessThan(actual, default(int), customMessage);
        }

        /// <summary>
        /// long
        /// </summary>
        public static void ShouldBePositive(this long actual)
        {
            ShouldBeGreaterThan(actual, default(long), () => null);
        }

        public static void ShouldBePositive(this long actual, string customMessage)
        {
            ShouldBeGreaterThan(actual, default(long), customMessage);
        }

        public static void ShouldBePositive(this long actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeGreaterThan(actual, default(long), customMessage);
        }

        public static void ShouldBeNegative(this long actual)
        {
            ShouldBeLessThan(actual, default(long), () => null);
        }

        public static void ShouldBeNegative(this long actual, string customMessage)
        {
            ShouldBeLessThan(actual, default(long), customMessage);
        }

        public static void ShouldBeNegative(this long actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeLessThan(actual, default(long), customMessage);
        }

        /// <summary>
        /// short
        /// </summary>
        public static void ShouldBePositive(this short actual)
        {
            ShouldBeGreaterThan(actual, default(short), () => null);
        }

        public static void ShouldBePositive(this short actual, string customMessage)
        {
            ShouldBeGreaterThan(actual, default(short), customMessage);
        }

        public static void ShouldBePositive(this short actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeGreaterThan(actual, default(short), customMessage);
        }

        public static void ShouldBeNegative(this short actual)
        {
            ShouldBeLessThan(actual, default(short), () => null);
        }

        public static void ShouldBeNegative(this short actual, string customMessage)
        {
            ShouldBeLessThan(actual, default(short), customMessage);
        }

        public static void ShouldBeNegative(this short actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldBeLessThan(actual, default(short), customMessage);
        }
    }
}