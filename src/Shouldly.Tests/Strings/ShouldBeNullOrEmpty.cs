using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings
{
    public class ShouldBeNullOrEmpty : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "a".ShouldBeNullOrEmpty(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"a\" should be null or empty " +
                       "Additional Info: " +
                       "Some additional context";
            } 
        }

        protected override void ShouldPass()
        {
            ((string)null).ShouldBeNullOrEmpty();
            string.Empty.ShouldBeNullOrEmpty();
        }
    }
}