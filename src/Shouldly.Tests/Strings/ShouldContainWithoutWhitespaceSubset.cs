namespace Shouldly.Tests.Strings
{
    public class ShouldContainWithoutWhitespaceSubset
    {
        [Fact]
        public void CanMatchOnSubset()
        {
            "Fun   with     space and some extra stuff".ShouldContainWithoutWhitespace("Fun with space");
        }
    }
}