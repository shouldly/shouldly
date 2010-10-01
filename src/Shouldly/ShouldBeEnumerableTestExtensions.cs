using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeEnumerableTestExtensions
    {
        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (!actual.Contains(expected))
                throw new AssertionException(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (actual.Contains(expected))
                throw new AssertionException(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance) {
            var result = actual.Where(a => ((expected - tolerance < a) & (a < expected + tolerance))).Count();
            if (result < 1)
                throw new AssertionException(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance) {
            var result = actual.Where(a => ((expected - tolerance < a) & (a < expected + tolerance))).Count();
            if (result < 1)
                throw new AssertionException(new ShouldlyMessage(expected, actual).ToString());
        }
    }
}