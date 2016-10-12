using System.Collections.Generic;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeInOrder
{
    public class IntegerArrayScenario
    {
        private readonly int[] _ascendingTarget = { 1, 2, 3, 4, 5 };
        private readonly int[] _descendingTarget = { 5, 4, 3, 2, 1 };

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
@"[5, 4, 3, 2, 1]
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
@"[5, 4, 3, 2, 1]
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
@"[1, 2, 3, 4, 5]
    should be in descending order
    but item at index 1 was not.

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldFailWhenDescendingIsSpecifiedAndComparerIsGiven()
        {
            Verify.ShouldFail(() =>
_ascendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<int>.Default, "Some additional context"),

errorWithSource:
@"_ascendingTarget
    should be in descending order
    but item at index 1 was not.

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3, 4, 5]
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
            _descendingTarget.ShouldBeInOrder(SortDirection.Descending, Comparer<int>.Default);
        }
    }
}
