using NUnit.Framework.Constraints;

namespace Shouldly
{
    internal static class ShouldlyCoreExtensions
    {
        internal static void AssertAwesomely<T>(this T actual, IResolveConstraint specifiedConstraint, object originalActual, object originalExpected)
        {
            var constraint = specifiedConstraint.Resolve();
            if (constraint.Matches(actual)) return;

            throw new ChuckedAWobbly(new ShouldlyMessage(originalExpected, originalActual).ToString());
        }
    }
}
