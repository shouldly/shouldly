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
                "should be unique [ \"string2\", \"string1\", \"string42\", \"string2\" ] but does not";
            }
        }

        protected override void ShouldThrowAWobbly()
        {
            new string[] { "string2", "string1", "string42", "string2" }.ShouldBeUnique();
        }

        protected override void ShouldPass()
        {
            new string[] { "string2", "string1", "string42", "string53" }.ShouldBeUnique();
        }
    }
}