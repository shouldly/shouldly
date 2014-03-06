namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class EqualScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.ShouldBeGreaterThan(1);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "1 should be greater than 1 but was 1"; }
        }
    }
}