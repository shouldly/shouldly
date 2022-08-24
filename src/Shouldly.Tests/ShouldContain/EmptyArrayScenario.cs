namespace Shouldly.Tests.ShouldContain
{
    public class EmptyArrayScenario
    {
        [Fact]
        public void EmptyArrayScenarioShouldFail()
        {
            var target = new int[0];
            Verify.ShouldFail(() =>
target.ShouldContain(1, "Some additional context"),

errorWithSource:
@"target
    should contain
1
    but was actually
[]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[]
    should contain
1
    but did not

Additional Info:
    Some additional context");
        }
    }
}