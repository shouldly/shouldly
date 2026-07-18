namespace Shouldly.Tests.ShouldContain;

public class CollectionPredicateWithCountScenario
{
    [Fact]
    public void CollectionWithACountOtherThanTheSpecifiedCountOfMatchingPredicatesFails()
    {
        var collection = new[] { "a", "b", "c", "c" };
        Verify.ShouldFail(() =>
            collection.ShouldContain(x => x == "c", 5));
    }

    [Fact]
    public void CollectionWithACountOtherThanTheSpecifiedCountOfMatchingPredicatesFailsWithCustomMessage()
    {
        var collection = new[] { "a", "b", "c", "c" };
        Verify.ShouldFail(() =>
            collection.ShouldContain(x => x == "c", 5, "custom message"));
    }

    [Fact]
    public void CollectionWithACountOtherThanTheSpecifiedCountOfMatchingPredicatesFailsWithCustomMessageFunc()
    {
        var collection = new[] { "a", "b", "c", "c" };
        Verify.ShouldFail(() =>
            collection.ShouldContain(x => x == "c", 5, "custom message"));
    }

    [Fact]
    public void CollectionWithTheSpecifiedCountOfMatchingPredicatesSucceeds()
    {
        new[] { "a", "b", "c", "c" }.ShouldContain(x => x == "c", 2);   // collection has exactly two items that are "c"
        new[] { 1, 2, 3, 4 }.ShouldContain(i => i % 2 == 0, 2);   // collection has exactly two items that are even
    }
}