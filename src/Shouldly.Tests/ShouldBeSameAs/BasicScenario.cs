namespace Shouldly.Tests.ShouldBeSameAs
{
    public class BasicScenario
    {
        [Fact]
        public void BasicScenarioShouldFail()
        {
            var zulu = new object();
            var tutsie = new object();
            Verify.ShouldFail(() =>
zulu.ShouldBeSameAs(tutsie, "Some additional context"),

errorWithSource:
@"zulu
    should be same as
System.Object (000000)
    but was
System.Object (000000)

Additional Info:
    Some additional context",

errorWithoutSource:
@"System.Object (000000)
    should be same as
System.Object (000000)
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var zulu = new object();

            zulu.ShouldBeSameAs(zulu);
        }
    }
}