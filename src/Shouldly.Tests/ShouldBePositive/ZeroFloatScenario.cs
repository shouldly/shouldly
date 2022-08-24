namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroFloatScenario
    {
        [Fact]
        public void ZeroFloatScenarioShouldFail()
        {
            var val = 0f;
            Verify.ShouldFail(() =>
val.ShouldBePositive("Some additional context"),

errorWithSource:
@"val
    should be positive but
0f
    is negative

Additional Info:
    Some additional context",

errorWithoutSource:
@"0f
    should be positive but is negative

Additional Info:
    Some additional context");
        }
    }
}