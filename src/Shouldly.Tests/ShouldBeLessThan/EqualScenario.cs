namespace Shouldly.Tests.ShouldBeLessThan
{
    public class EqualScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.ShouldBeLessThan(1);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "1 should be less than 1 but was 1"; }
        }
    }
}