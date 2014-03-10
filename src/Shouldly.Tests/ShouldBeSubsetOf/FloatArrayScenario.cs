namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class FloatArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1f }.ShouldBeSubsetOf(new[] { 2f, 3f, 4f });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { 1f } should be subset of  [2, 3, 4] but does not"; }
        }

        protected override void ShouldPass()
        {
            new[] { 1f }.ShouldBeSubsetOf(new[] { 1f, 2f, 3f });
        }
    }
}