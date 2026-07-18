namespace Shouldly.Tests.ShouldContain;

public class PredicateObjectScenario
{
    [Fact]
    public void PredicateObjectScenarioShouldFail()
    {
        var a = new object();
        var b = new object();
        var c = new object();

        Verify.ShouldFail(() =>
            new[] { a, b, c }.ShouldContain(o => o.GetType().FullName!.Equals(""), "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var a = new object();
        var b = new object();
        var c = new object();
        new[] { a, b, c }.ShouldContain(o => o.GetType().FullName!.Equals("System.Object"));
    }
}