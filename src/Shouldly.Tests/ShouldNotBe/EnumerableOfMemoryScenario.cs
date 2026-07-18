namespace Shouldly.Tests.ShouldNotBe;

public class EnumerableOfMemoryScenario
{
    [Fact]
    public void EnumerableOfMemoryScenarioShouldFail()
    {
        var foo = new byte[] { 1, 2, 3 }.AsMemory();
        var bar = new byte[] { 1, 2, 3 }.AsMemory();

        Verify.ShouldFail(() =>
            foo.ShouldNotBe(bar, "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenContentDiffers()
    {
        var foo = new byte[] { 1, 2, 3 }.AsMemory();
        foo.ShouldNotBe(new byte[] { 1, 2 }.AsMemory());
    }

    [Fact]
    public void ShouldPassWhenLengthDiffers()
    {
        var foo = new byte[] { 1, 2, 3 }.AsMemory();
        foo.ShouldNotBe(new byte[] { 1, 2, 3, 4 }.AsMemory());
    }
}
