namespace Shouldly.Tests.ShouldBeBoolean
{
    public class TrueScenario
    {
        [Fact]
        public void TrueScenarioShouldFail()
        {
            const bool myValue = false;
            Verify.ShouldFail(() =>
myValue.ShouldBeTrue("Some additional context"),

errorWithSource:
@"myValue
    should be
True
    but was
False

Additional Info:
    Some additional context",

errorWithoutSource:
@"False
    should be
True
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            true.ShouldBeTrue();
        }
    }
}