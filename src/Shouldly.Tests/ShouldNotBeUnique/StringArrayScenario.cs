using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeUnique
{
    public class StringArrayScenario : ShouldlyShouldTestScenario
    {
        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new string[] { \"string2\", \"string1\", \"string42\", \"string53\" } " +
                "should not be unique but was";
            }
        }

        protected override void ShouldThrowAWobbly()
        {
            new string[] { "string2", "string1", "string42", "string53" }.ShouldNotBeUnique();
        }

        protected override void ShouldPass()
        {
            new string[] { "string2", "string1", "string42", "string2" }.ShouldNotBeUnique();
        }
    }
}