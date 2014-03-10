namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class IntegerArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1 }.ShouldBeSubsetOf(new[] { 2, 3, 4 });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { 1 } should be subset of  [2, 3, 4] but does not"; }
        }

        protected override void ShouldPass()
        {
            new[] { 1 }.ShouldBeSubsetOf(new[] { 1, 2, 3 });
        }
    }
}