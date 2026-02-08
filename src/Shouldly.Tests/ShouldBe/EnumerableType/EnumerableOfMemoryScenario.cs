namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfMemoryScenario
{
    [Fact]
    public void EnumerableOfMemoryScenarioShouldFail()
    {
        var foo = new byte[] { 1, 2, 3 }.AsMemory();
        var bar = new byte[] { 1, 2 }.AsMemory();

        Verify.ShouldFail(() =>
            foo.ShouldBe(bar, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var foo = new byte[] { 1, 2, 3 }.AsMemory();
        foo.ShouldBe(new byte[] { 1, 2, 3 });
    }
}