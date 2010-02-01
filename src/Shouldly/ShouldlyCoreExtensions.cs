using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Shouldly
{
    public static class ShouldlyCoreExtensions
    {
        public static void AssertAwesomely<T>(this T actual, IResolveConstraint specifiedConstraint, object originalActual, object originalExpected)
        {
            var constraint = specifiedConstraint.Resolve();
            if (constraint.Matches(actual)) return;

            throw new AssertionException(new ShouldlyMessage(originalExpected, originalActual).ToString());
        }

    }
}
