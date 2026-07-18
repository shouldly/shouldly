namespace Shouldly.Tests.ShouldBeInOrder;

public class DoubleArrayScenario
{
    private readonly double[] _ascendingTarget = [1.1, 1.2, 1.3, 1.4, 1.5];
    private readonly double[] _descendingTarget = [1.5, 1.4, 1.3, 1.2, 1.1];

    [Fact]
    [UseCulture("en-US")]
    public void ShouldFailWithDefaultDirection()
    {
        Verify.ShouldFail(() =>
            _descendingTarget.ShouldBeInOrder("Some additional context"));
    }

    [Fact]
    [UseCulture("en-US")]
    public void ShouldFailWhenAscendingIsSpecified()
    {
        Verify.ShouldFail(() =>
            _descendingTarget.ShouldBeInOrder(SortDirection.Ascending, "Some additional context"));
    }

    [Fact]
    [UseCulture("en-US")]
    public void ShouldFailWhenDescendingIsSpecified()
    {
        Verify.ShouldFail(() =>
            _ascendingTarget.ShouldBeInOrder(SortDirection.Descending, "Some additional context"));
    }

    [Fact]
    [UseCulture("en-US")]
    public void ShouldFailWhenDescendingIsSpecifiedAndComparerIsGiven()
    {
        Verify.ShouldFail(() =>
            _ascendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<double>.Default, "Some additional context"));
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
        _descendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<double>.Default);
    }
}