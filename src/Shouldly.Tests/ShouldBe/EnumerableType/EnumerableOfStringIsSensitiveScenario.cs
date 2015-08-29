using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class EnumerableOfStringIsSensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            new[] { "foo" }.ShouldBe(new[] { "foo" }, Case.Sensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            new[] { "foo" }.ShouldBe(new[] { "FoO" }, Case.Sensitive, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { \"foo\" } should be [\"FoO\"] (case sensitive comparison) but was [\"foo\"] difference [*\"foo\"*]" +
                           "Additional Info: Some additional context";
            }
        }
    }
}