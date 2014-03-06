namespace Shouldly.Tests.ShouldBeLessThan
{
    public class StringVsNullScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "b".ShouldBeLessThan(null);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"b\" should be less than null but was \"b\""; }
        }

        protected override void ShouldPass()
        {
            ((string)null).ShouldBeLessThan("b");
        }
    }
}