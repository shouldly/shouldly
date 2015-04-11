using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeUnique
{
    public class StringArrayScenario : ShouldlyShouldTestScenario
    {
        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new string[] { \"string2\", \"string1\", \"string42\", \"string2\" } " +
                       "should be unique but [\"string2\"] was duplicated" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldThrowAWobbly()
        {
            new string[] {"string2", "string1", "string42", "string2"}.ShouldBeUnique("Some additional context");
        }

        protected override void ShouldPass()
        {
            new string[] {"string2", "string1", "string42", "string53"}.ShouldBeUnique(() => "Some additional context");
        }
    }
}