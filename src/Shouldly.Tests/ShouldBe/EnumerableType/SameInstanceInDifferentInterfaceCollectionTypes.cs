namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class SameInstanceInDifferentInterfaceCollectionTypes
{
    [Fact]
    public void EnumerableShouldBeArrayUsesCollectionComparison()
    {
        var actual = new[] { typeof(int), typeof(string) }.Select(x => x);

        actual.ShouldBe(new[] { typeof(int), typeof(string) });
    }
}
