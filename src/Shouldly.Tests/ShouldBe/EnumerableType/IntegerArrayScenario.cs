using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IntegerArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {99, 2, 3, 5}.ShouldBe(new[] {1, 2, 3, 4});
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] {99, 2, 3, 5} should be [1, 2, 3, 4] " +
                         "but was [99, 2, 3, 5] difference [*99*, 2, 3, *5*]"; }
        }

        protected override void ShouldPass()
        {
            new[] { 1, 2, 3, 4 }.ShouldBe(new[] { 1, 2, 3, 4 });
        }
    }
}