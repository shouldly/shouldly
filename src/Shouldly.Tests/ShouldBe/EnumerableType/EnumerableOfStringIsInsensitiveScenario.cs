using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class EnumerableOfStringIsInsensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            new[] { "foo" }.ShouldBe(new[] { "FOo" }, Case.Insensitive);
        }

        protected override void ShouldThrowAWobbly()
        {
            new[] { "foo" }.ShouldBe(new[] { "different" }, Case.Insensitive, () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { \"foo\" } should be [\"different\"] (case insensitive comparison) but was [\"foo\"] difference [*\"foo\"*]" +
                           "Additional Info: Some additional context";
            }
        }
    }
}