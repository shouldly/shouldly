namespace Shouldly.Tests.ShouldBe.EnumerableType;

/// <summary>
/// Memories inside a sequence compare by their contents, as a memory compared directly does, whatever the sequence's
/// element type: <c>Memory&lt;T&gt;.Equals</c> compares which array a memory wraps, not what it holds.
/// </summary>
public class SequenceOfMemoriesScenario
{
    [Fact]
    public void TypedSequenceComparesContents()
    {
        new List<Memory<int>> { new[] { 1, 2 } }.ShouldBe([new[] { 1, 2 }]);
        new List<ReadOnlyMemory<int>> { new[] { 1, 2 } }.ShouldBe([new[] { 1, 2 }]);
        Should.Throw<ShouldAssertException>(() => new List<Memory<int>> { new[] { 1, 2 } }.ShouldBe([new[] { 1, 3 }]));
    }

    [Fact]
    public void ObjectSequenceComparesContents()
    {
        new List<object> { new Memory<int>([1, 2]) }.ShouldBe([new Memory<int>([1, 2])]);
        Should.Throw<ShouldAssertException>(() =>
            new List<object> { new Memory<int>([1, 2]) }.ShouldBe([new Memory<int>([1, 3])]));
    }

    [Fact]
    public void SingleValueComparesContents()
    {
        // Through the generic ShouldBe<T>, not the Memory<T> overload.
        new Memory<int>([1, 2]).ShouldBe<Memory<int>>(new Memory<int>([1, 2]));
        ((object)new Memory<int>([1, 2])).ShouldBe(new Memory<int>([1, 2]));
    }
}
