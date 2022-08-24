namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class EqualScenario
    {
        [Fact]
        public void EqualScenarioShouldFail()
        {
            var one = 1;
            Verify.ShouldFail(() =>
one.ShouldBeGreaterThan(1, "Some additional context"),

errorWithSource:
@"one
    should be greater than
1
    but was
1

Additional Info:
    Some additional context",

errorWithoutSource:
@"1
    should be greater than
1
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            7.ShouldBeGreaterThan(1);
        }
    }
}