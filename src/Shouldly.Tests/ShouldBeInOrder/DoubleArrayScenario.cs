using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeInOrder
{
    public class DoubleArrayScenario
    {
        private readonly double[] _ascendingTarget = { 1.1, 1.2, 1.3, 1.4, 1.5 };
        private readonly double[] _descendingTarget = { 1.5, 1.4, 1.3, 1.2, 1.1 };

        [Fact]
        [UseCulture("en-US")]
        public void ShouldFailWithDefaultDirection()
        {
            Verify.ShouldFail(() =>
_descendingTarget.ShouldBeInOrder("Some additional context"),

errorWithSource:
@"_descendingTarget
    should be in ascending order but was not.
    The first out-of-order item was found at index 1:
1.4

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1.5d, 1.4d, 1.3d, 1.2d, 1.1d]
    should be in ascending order but was not.
    The first out-of-order item was found at index 1:
1.4

Additional Info:
    Some additional context");
        }

        [Fact]
        [UseCulture("en-US")]
        public void ShouldFailWhenAscendingIsSpecified()
        {
            Verify.ShouldFail(() =>
_descendingTarget.ShouldBeInOrder(SortDirection.Ascending, "Some additional context"),

errorWithSource:
@"_descendingTarget
    should be in ascending order but was not.
    The first out-of-order item was found at index 1:
1.4

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1.5d, 1.4d, 1.3d, 1.2d, 1.1d]
    should be in ascending order but was not.
    The first out-of-order item was found at index 1:
1.4

Additional Info:
    Some additional context");
        }

        [Fact]
        [UseCulture("en-US")]
        public void ShouldFailWhenDescendingIsSpecified()
        {
            Verify.ShouldFail(() =>
_ascendingTarget.ShouldBeInOrder(SortDirection.Descending, "Some additional context"),

errorWithSource:
@"_ascendingTarget
    should be in descending order but was not.
    The first out-of-order item was found at index 1:
1.2

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1.1d, 1.2d, 1.3d, 1.4d, 1.5d]
    should be in descending order but was not.
    The first out-of-order item was found at index 1:
1.2

Additional Info:
    Some additional context");
        }

        [Fact]
        [UseCulture("en-US")]
        public void ShouldFailWhenDescendingIsSpecifiedAndComparerIsGiven()
        {
            Verify.ShouldFail(() =>
_ascendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<double>.Default, "Some additional context"),

errorWithSource:
@"_ascendingTarget
    should be in descending order but was not.
    The first out-of-order item was found at index 1:
1.2

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1.1d, 1.2d, 1.3d, 1.4d, 1.5d]
    should be in descending order but was not.
    The first out-of-order item was found at index 1:
1.2

Additional Info:
    Some additional context");
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
}
