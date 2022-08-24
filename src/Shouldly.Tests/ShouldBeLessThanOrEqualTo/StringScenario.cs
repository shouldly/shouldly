namespace Shouldly.Tests.ShouldBeLessThanOrEqualTo
{
    public class StringScenario
    {
        [Fact]
        public void StringScenarioShouldFail()
        {
            var bee = "b";
            Verify.ShouldFail(() =>
bee.ShouldBeLessThanOrEqualTo("a", "Some additional context"),

errorWithSource:
@"bee
    should be less than or equal to
""a""
    but was
""b""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""b""
    should be less than or equal to
""a""
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "a".ShouldBeLessThanOrEqualTo("a");
        }
    }
}