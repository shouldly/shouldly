namespace Shouldly.Tests.ShouldBeLessThan
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "b".ShouldBeLessThan("a");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"b\" should be less than \"a\" but was \"b\""; }
        }

        protected override void ShouldPass()
        {
            "a".ShouldBeLessThan("b");
        }
    }
}