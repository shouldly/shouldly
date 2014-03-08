namespace Shouldly.Tests.ShouldBe.CaseSensitivity
{
    public class CaseIsSensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "SamplE".ShouldBe("sAMPLe", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "'SamplE' should be 'sAMPLe' but was 'SamplE'"; }
        }

        protected override void ShouldPass()
        {
            "SamplE".ShouldBe("SamplE", Case.Sensitive);
        }
    }
}