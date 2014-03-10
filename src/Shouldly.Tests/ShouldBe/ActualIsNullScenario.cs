using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class ActualIsNullScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            string nullString = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            nullString.ShouldBe(string.Empty);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "nullString should be \"\" but was null"; }
        }
    }
}