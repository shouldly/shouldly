using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class ObjectArrayOfIntScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var arr = new object[] { 1, 2, 3 };
            var arr2 = new object[] { 1, 2 };

            arr.ShouldBeSubsetOf(arr2);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "arr should be subset of [1, 2] but [3] do not"; }
        }

        protected override void ShouldPass()
        {
            var arr = new object[] { 1, 2, 3 };
            var arr2 = new object[] { 1, 2, 3, 4 };

            arr.ShouldBeSubsetOf(arr2);
        }
    }
}