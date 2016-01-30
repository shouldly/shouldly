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
            collection.ShouldContain(x => x == "c", 5),
#if net35
errorWithSource:
@"collection
    should contain 5 element(s) satisfying the condition
(x = ""c"")
    but does not",
errorWithoutSource:
@"[""a"", ""b"", ""c"", ""c""]
    should contain 5 element(s) satisfying the condition
(x = ""c"")
    but does not");
#else

errorWithSource:
@"collection
    should contain 5 element(s) satisfying the condition
(x == ""c"")
    but does not",
errorWithoutSource:
@"[""a"", ""b"", ""c"", ""c""]
    should contain 5 element(s) satisfying the condition
(x == ""c"")
    but does not");
#endif
        }

        [Fact]
        public void CollectionWithACountOtherThanTheSpecifiedCountOfMatchingPredicatesFailsWithCustomMessage()
        {
            var collection = new[] { "a", "b", "c", "c" };
            Verify.ShouldFail(() =>
            collection.ShouldContain(x => x == "c", 5, "custom message"),
#if net35
errorWithSource:
@"collection
    should contain 5 element(s) satisfying the condition
(x = ""c"")
    but does not

Additional Info:
    custom message",
errorWithoutSource:
@"[""a"", ""b"", ""c"", ""c""]
    should contain 5 element(s) satisfying the condition
(x = ""c"")
    but does not

Additional Info:
    custom message");
#else
errorWithSource:
@"collection
    should contain 5 element(s) satisfying the condition
(x == ""c"")
    but does not

Additional Info:
    custom message",
errorWithoutSource:
@"[""a"", ""b"", ""c"", ""c""]
    should contain 5 element(s) satisfying the condition
(x == ""c"")
    but does not

Additional Info:
    custom message");
#endif
        }

        [Fact]
        public void CollectionWithACountOtherThanTheSpecifiedCountOfMatchingPredicatesFailsWithCustomMessageFunc()
        {
            var collection = new[] { "a", "b", "c", "c" };
            Verify.ShouldFail(() =>
            collection.ShouldContain(x => x == "c", 5, () => "custom message"),
#if net35
errorWithSource:
@"collection
    should contain 5 element(s) satisfying the condition
(x = ""c"")
    but does not

Additional Info:
    custom message",
errorWithoutSource:
@"[""a"", ""b"", ""c"", ""c""]
    should contain 5 element(s) satisfying the condition
(x = ""c"")
    but does not

Additional Info:
    custom message");
#else

errorWithSource:
@"collection
    should contain 5 element(s) satisfying the condition
(x == ""c"")
    but does not

Additional Info:
    custom message",
errorWithoutSource:
@"[""a"", ""b"", ""c"", ""c""]
    should contain 5 element(s) satisfying the condition
(x == ""c"")
    but does not

Additional Info:
    custom message");
#endif
        }

        [Fact]
        public void CollectionWithTheSpecifiedCountOfMatchingPredicatesSucceeds()
        {
            new[] { "a","b","c","c"}.ShouldContain(x => x == "c", 2);   // collection has exactly two items that are "c"
            new[] { 1, 2, 3, 4 }.ShouldContain(i => (i % 2) == 0, 2);   // collection has exactly two items that are even
        }
    }
}
