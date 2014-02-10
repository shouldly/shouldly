using System;

namespace Shouldly
{
    internal static class ShouldlyCoreExtensions
    {
        internal static void AssertAwesomely<T>(this T actual, Func<T, bool> specifiedConstraint, object originalActual, object originalExpected)
        {
            if (specifiedConstraint(actual)) return;

            throw new ChuckedAWobbly(new ShouldlyMessage(originalExpected, originalActual).ToString());
        }
    }
}
