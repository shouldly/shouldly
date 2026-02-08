namespace Shouldly.Tests.ShouldNotContain;

public class PredicateObjectScenario
{
    [Fact]
    public void PredicateObjectScenarioShouldFail()
    {
        var a = new object();
        var b = new object();
        var c = new object();
        Verify.ShouldFail(() =>
            new[] { a, b, c }.ShouldNotContain(o => o.GetType().FullName!.Equals("System.Object"),
                "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var a = new object();
        var b = new object();
        var c = new object();
        new[] { a, b, c }.ShouldNotContain(o => o.GetType().FullName!.Equals(""));
    }
}