namespace Shouldly.Tests.Strings.ShouldBe
{
    public class CaseIsInsensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "SamplE".ShouldBe("different", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "'SamplE' should be 'different' but was 'SamplE'"; }
        }

        protected override void ShouldPass()
        {
            "SamplE".ShouldBe("sAMPLe", Case.Insensitive);
        }
    }
}