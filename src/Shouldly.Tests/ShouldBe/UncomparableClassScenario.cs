namespace Shouldly.Tests.ShouldBe
{
    public class UncomparableClassScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new UncomparableClass("ted").ShouldBe(new UncomparableClass("bob"));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new UncomparableClass(\"ted\") should be bob but was ted"; }
        }
    }
}