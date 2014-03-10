namespace Shouldly.Tests.ShouldBeOneOf
{
    public class IntScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.ShouldBeOneOf(4, 5, 6);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "1 should be one of [4, 5, 6] but was 1"; }
        }

        protected override void ShouldPass()
        {
            1.ShouldBeOneOf(1, 2, 3);
        }
    }
}