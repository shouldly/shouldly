namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class DecimalArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1m }.ShouldBeSubsetOf(new[] { 2m, 3m, 4m });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { 1m } should be subset of  [2, 3, 4] but does not"; }
        }

        protected override void ShouldPass()
        {
            new[] { 1m }.ShouldBeSubsetOf(new[] { 1m, 2m, 3m });
        }
    }
}