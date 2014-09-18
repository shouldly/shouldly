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
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (actual.Contains(expected))
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            if (!actual.Any(condition))
                throw new ChuckedAWobbly(new ShouldlyMessage(elementPredicate.Body).ToString());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            if (actual.Any(condition))
                throw new ChuckedAWobbly(new ShouldlyMessage(elementPredicate.Body).ToString());
        }

        public static void ShouldAllBe<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            var condition = elementPredicate.Compile();
            if (actual.Any(v => !condition(v)))
                throw new ChuckedAWobbly(new ShouldlyMessage(elementPredicate.Body).ToString());
        }

        public static void ShouldBeEmpty<T>(this IEnumerable<T> actual)
        {
            if (actual == null || (actual != null && actual.Count() != 0))
                throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
        }

        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> actual)
        {
            if (actual == null || actual != null && !actual.Any())
                throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
        }

        public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance) 
        {
            if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
        }

        public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance) 
        {
            if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
                throw new ChuckedAWobbly(new ShouldlyMessage(expected, actual).ToString());
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
                throw new ChuckedAWobbly(new ShouldlyMessage(expected).ToString());
        }

		public static void ShouldBeUnique<T>(this IEnumerable<T> actual)
		{
			var list = new List<object>();

			foreach (object o1 in actual)
			{
				foreach (object o2 in list)
					if (o1 != null && o1.Equals(o2))
						throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
				list.Add(o1);
			}
		}

		public static void ShouldNotBeUnique<T>(this IEnumerable<T> actual)
		{
			var list = new List<object>();

			foreach (object o1 in actual)
			{
				foreach(object o2 in list)
					if (o1 != null && o1.Equals(o2))
						return;
				list.Add(o1);
			}
			throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
		}
    }

}