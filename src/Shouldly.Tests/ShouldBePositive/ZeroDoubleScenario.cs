namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroDoubleScenario
    {
        [Fact]
        public void ZeroDoubleScenarioShouldFail()
        {
            var val = 0.0;
            Verify.ShouldFail(() =>
val.ShouldBePositive("Some additional context"),

errorWithSource:
@"val
    should be positive but
0d
    is negative

Additional Info:
    Some additional context",

errorWithoutSource:
@"0d
    should be positive but is negative

Additional Info:
    Some additional context");
        }
    }
}