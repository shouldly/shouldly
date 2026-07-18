namespace Shouldly.Tests.ShouldNotBe;

public class EnumerableOfReadOnlyMemoryScenario
{
    [Fact]
    public void EnumerableOfReadOnlyMemoryScenarioShouldFail()
    {
        ReadOnlyMemory<byte> foo = new byte[] { 1, 2, 3 }.AsMemory();
        ReadOnlyMemory<byte> bar = new byte[] { 1, 2, 3 }.AsMemory();

        Verify.ShouldFail(() =>
            foo.ShouldNotBe(bar, "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenContentDiffers()
    {
        ReadOnlyMemory<byte> foo = new byte[] { 1, 2, 3 }.AsMemory();
        ReadOnlyMemory<byte> bar = new byte[] { 1, 2 }.AsMemory();
        foo.ShouldNotBe(bar);
    }

    [Fact]
    public void ShouldPassWhenLengthDiffers()
    {
        ReadOnlyMemory<byte> foo = new byte[] { 1, 2, 3 }.AsMemory();
        ReadOnlyMemory<byte> bar = new byte[] { 1, 2, 3, 4 }.AsMemory();
        foo.ShouldNotBe(bar);
    }
}
