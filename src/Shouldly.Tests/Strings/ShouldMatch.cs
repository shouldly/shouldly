namespace Shouldly.Tests.Strings
{
    public class ShouldMatch : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldMatch(@"\d+");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should match \"\\d+\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldMatch(@"C.e{2}s[e]");
        }
    }
}