using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBe
{
    public class CaseIsInsensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "SamplE".ShouldBe("different", Case.Insensitive, () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "'SamplE' should be 'different' but was 'SamplE' " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "SamplE".ShouldBe("sAMPLe", Case.Insensitive);
        }
    }
}