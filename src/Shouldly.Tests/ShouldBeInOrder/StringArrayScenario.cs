namespace Shouldly.Tests.ShouldBeInOrder;

public class StringArrayScenario
{
    private readonly string[] _ascendingTarget = ["a", "b", "c", "d", "e"];
    private readonly string[] _descendingTarget = ["e", "d", "c", "b", "a"];

    [Fact]
    public void ShouldFailWithDefaultDirection()
    {
        Verify.ShouldFail(() =>
            _descendingTarget.ShouldBeInOrder("Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenAscendingIsSpecified()
    {
        Verify.ShouldFail(() =>
            _descendingTarget.ShouldBeInOrder(SortDirection.Ascending, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenDescendingIsSpecified()
    {
        Verify.ShouldFail(() =>
            _ascendingTarget.ShouldBeInOrder(SortDirection.Descending, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenDescendingIsSpecifiedAndComparerIsGiven()
    {
        Verify.ShouldFail(() =>
            _ascendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<string>.Default, "Some additional context"));
    }

    [Fact]
    public void ShouldPassWithDefaultDirection()
    {
        _ascendingTarget.ShouldBeInOrder();
    }

    [Fact]
    public void ShouldPassWhenAscendingIsSpecified()
    {
        _ascendingTarget.ShouldBeInOrder(SortDirection.Ascending);
    }

    [Fact]
    public void ShouldPassWhenDescendingIsSpecified()
    {
        _descendingTarget.ShouldBeInOrder(SortDirection.Descending);
    }

    [Fact]
    public void ShouldPassWhenDescendingIsSpecifiedAndComparerIsGiven()
    {
        _descendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<string>.Default);
    }
}