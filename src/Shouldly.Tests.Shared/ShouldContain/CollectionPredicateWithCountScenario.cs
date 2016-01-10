using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.Shared.ShouldContain
{
    public class CollectionPredicateWithCountScenario
    {
        [Fact]
        public void CollectionWithACountOtherThanTheSpecifiedCountOfMatchingPredicatesFails()
        {
            var collection = new[] { "a", "b", "c", "c" };
            Verify.ShouldFail(() =>
            collection.ShouldContainMatchingCount(x => x == "c", 5),

errorWithSource:
@"collection
    should contain matching count
5
    but was actually
[""a"", ""b"", ""c"", ""c""]",
errorWithoutSource:
@"[""a"", ""b"", ""c"", ""c""]
    should contain matching count
5
    but did not");
        }

        [Fact]
        public void CollectionWithTheSpecifiedCountOfMatchingPredicatesSucceeds()
        {
            new[] { "a","b","c","c"}.ShouldContainMatchingCount(x => x == "c", 2);
        }
    }
}
