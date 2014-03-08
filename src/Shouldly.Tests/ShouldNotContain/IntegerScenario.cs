namespace Shouldly.Tests.ShouldNotContain
{
    public class IntegerScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1, 2, 3, 4, 5 }.ShouldNotContain(3);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return " new[] { 1, 2, 3, 4, 5 } should not contain 3 but was [1, 2, 3, 4, 5]"; }
        }

        protected override void ShouldPass()
        {
            new[] { 1, 2, 3, 4, 5 }.ShouldNotContain(7);
        }
    }
}