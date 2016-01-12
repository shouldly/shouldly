using NUnit.Framework;

namespace Shouldly.Tests.Strings
{
    public class ShouldContainWithoutWhitespaceSubset
    {
        [Test]
        public void CanMatchOnSubset()
        {
            "Fun   with     space and some extra stuff".ShouldContainWithoutWhitespace("Fun with space");
        }
    }
}