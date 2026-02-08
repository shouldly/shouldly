namespace Shouldly.Tests.ShouldNotContain;

public class ObjectScenario
{
    [Fact]
    public void ObjectScenarioShouldFail()
    {
        var a = new object();
        var b = new object();
        var c = new object();
        var target = new[] { a, b, c };
        Verify.ShouldFail(() =>
            target.ShouldNotContain(c, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var a = new object();
        var b = new object();
        var c = new object();
        var d = new object();
        var target = new[] { a, b, c };
        target.ShouldNotContain(d);
    }
}