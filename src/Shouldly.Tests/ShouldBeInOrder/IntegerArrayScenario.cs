namespace Shouldly.Tests.ShouldBeInOrder;

public class IntegerArrayScenario
{
    private readonly int[] _ascendingTarget = [1, 2, 3, 4, 5];
    private readonly int[] _descendingTarget = [5, 4, 3, 2, 1];

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
            _ascendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<int>.Default, "Some additional context"));
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
        _descendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<int>.Default);
    }
}