using System.Collections.Generic;
using Xunit;

namespace Shouldly.Tests.ShouldBeInOrder
{
    public class UnitArrayScenario
    {
        [Fact]
        public void ShouldPass()
        {
            new[] { 5 }.ShouldBeInOrder();
        }

        [Fact]
        public void ShouldPassWhenSortDirectionIsGiven()
        {
            new[] { 5 }.ShouldBeInOrder(SortDirection.Descending);
        }

        [Fact]
        public void ShouldPassWhenSortDirectionAndCustomComparerAreGiven()
        {
            new[] { 5 }.ShouldBeInOrder(SortDirection.Ascending, Comparer<int>.Default);
        }
    }
}
