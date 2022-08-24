namespace Shouldly.Tests.ShouldBeInOrder
{
    public class EmptyArrayScenario
    {
        [Fact]
        public void ShouldPass()
        {
            new int[0].ShouldBeInOrder();
        }

        [Fact]
        public void ShouldPassWhenSortDirectionIsGiven()
        {
            new int[0].ShouldBeInOrder(SortDirection.Descending);
        }

        [Fact]
        public void ShouldPassWhenSortDirectionAndCustomComparerAreGiven()
        {
            new int[0].ShouldBeInOrder(SortDirection.Ascending, Comparer<int>.Default);
        }
    }
}
