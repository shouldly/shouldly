namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfReadOnlyMemoryScenario
{
    [Fact]
    public void EnumerableOfReadOnlyMemoryScenarioShouldFail()
    {
        ReadOnlyMemory<byte> foo = new byte[] { 1, 2, 3 }.AsMemory();
        ReadOnlyMemory<byte> bar = new byte[] { 1, 2 }.AsMemory();

        Verify.ShouldFail(() =>
            foo.ShouldBe(bar, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        ReadOnlyMemory<byte> foo = new byte[] { 1, 2, 3 }.AsMemory();
        foo.ShouldBe(new byte[] { 1, 2, 3 });
    }
}