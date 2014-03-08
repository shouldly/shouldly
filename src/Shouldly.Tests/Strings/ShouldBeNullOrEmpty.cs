namespace Shouldly.Tests.Strings
{
    public class ShouldBeNullOrEmpty : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "a".ShouldBeNullOrEmpty();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"a\" should be null or empty"; } 
        }

        protected override void ShouldPass()
        {
            ((string)null).ShouldBeNullOrEmpty();
            string.Empty.ShouldBeNullOrEmpty();
        }
    }
}