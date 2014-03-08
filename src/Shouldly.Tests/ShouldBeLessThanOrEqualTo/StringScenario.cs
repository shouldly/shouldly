namespace Shouldly.Tests.ShouldBeLessThanOrEqualTo
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "b".ShouldBeLessThanOrEqualTo("a");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"b\" should be less than or equal to \"a\" but was \"b\""; }
        }

        protected override void ShouldPass()
        {
            "a".ShouldBeLessThanOrEqualTo("a");
        }
    }
}