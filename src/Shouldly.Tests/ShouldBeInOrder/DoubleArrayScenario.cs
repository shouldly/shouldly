using System.Collections.Generic;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeInOrder
{
    public class DoubleArrayScenario
    {
        private readonly double[] _ascendingTarget = { 1.1, 1.2, 1.3, 1.4, 1.5 };
        private readonly double[] _descendingTarget = { 1.5, 1.4, 1.3, 1.2, 1.1 };

        [Fact]
        public void ShouldFailWithDefaultDirection()
        {
            Verify.ShouldFail(() =>
_descendingTarget.ShouldBeInOrder("Some additional context"),

errorWithSource:
@"_descendingTarget
    should be in ascending order
    but item at index 1 was not.

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1.5d, 1.4d, 1.3d, 1.2d, 1.1d]
    should be in ascending order
    but item at index 1 was not.

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldFailWhenAscendingIsSpecified()
        {
            Verify.ShouldFail(() =>
_descendingTarget.ShouldBeInOrder(SortDirection.Ascending, "Some additional context"),

errorWithSource:
@"_descendingTarget
    should be in ascending order
    but item at index 1 was not.

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1.5d, 1.4d, 1.3d, 1.2d, 1.1d]
    should be in ascending order
    but item at index 1 was not.

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldFailWhenDescendingIsSpecified()
        {
            Verify.ShouldFail(() =>
_ascendingTarget.ShouldBeInOrder(SortDirection.Descending, "Some additional context"),

errorWithSource:
@"_ascendingTarget
    should be in descending order
    but item at index 1 was not.

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1.1d, 1.2d, 1.3d, 1.4d, 1.5d]
    should be in descending order
    but item at index 1 was not.

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldFailWhenDescendingIsSpecifiedAndComparerIsGiven()
        {
            Verify.ShouldFail(() =>
_ascendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<double>.Default, "Some additional context"),

errorWithSource:
@"_ascendingTarget
    should be in descending order
    but item at index 1 was not.

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1.1d, 1.2d, 1.3d, 1.4d, 1.5d]
    should be in descending order
    but item at index 1 was not.

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
