namespace Shouldly.Tests.ShouldBe;

public class ShouldBeEnumerableTypeScenarios
{
    [Fact]
    public void StringListScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _thisString.ShouldBe(_thisOtherStringList, "Some additional context"),

            errorWithSource:
            """
            _thisString
                should be
            ["1", "3"]
                but was (case sensitive comparison)
            ["1", "2"]
                difference
            ["1", *"2"*]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            ["1", "2"]
                should be
            ["1", "3"]
                but was not (case sensitive comparison)
                difference
            ["1", *"2"*]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void DifferentOrderWithMissingItemFromBothIgnoringOrderScenario()
    {
        var first = new List<int> { 1, 3, 2 };
        var second = new[] { 1, 3, 4 };
        Verify.ShouldFail(() =>
                first.ShouldBe(second, true, "Some additional context"),

            errorWithSource:
            """
            first
                should be (ignoring order)
            [1, 3, 4]
                but
            first
                is missing
            [4]
                and
            [1, 3, 4]
                is missing
            [2]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 3, 2]
                should be (ignoring order)
            [1, 3, 4]
                but
            [1, 3, 2]
                is missing
            [4]
                and
            [1, 3, 4]
                is missing
            [2]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void DifferentOrderWithMissingItemFromExpectedScenario()
    {
        Verify.ShouldFail(() =>
                new List<int> { 1, 3, 2 }.ShouldBe([1, 3], true, "Some additional context"),

            errorWithSource:
            """
            new List<int> { 1, 3, 2 }
                should be (ignoring order)
            [1, 3]
                but
            [1, 3]
                is missing
            [2]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 3, 2]
                should be (ignoring order)
            [1, 3]
                but
            [1, 3]
                is missing
            [2]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void DifferentOrderWithMissingItemFromActualScenario()
    {
        Verify.ShouldFail(() =>
                new List<int> { 1, 3 }.ShouldBe([1, 2, 3], true, "Some additional context"),

            errorWithSource:
            """
            new List<int> { 1, 3 }
                should be (ignoring order)
            [1, 2, 3]
                but
            new List<int> { 1, 3 }
                is missing
            [2]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 3]
                should be (ignoring order)
            [1, 2, 3]
                but
            [1, 3]
                is missing
            [2]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void DifferentCollectionTypeScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new List<int> { 1, 2, 3 }.ShouldBe([1, 3, 2], false, "Some additional context"),

            errorWithSource:
            """
            new List<int> { 1, 2, 3 }
                should be
            [1, 3, 2]
                but was
            [1, 2, 3]
                difference
            [1, *2*, *3*]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2, 3]
                should be
            [1, 3, 2]
                but was not
                difference
            [1, *2*, *3*]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ActualIsNullScenario()
    {
        IEnumerable<int>? something = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
                something.ShouldBe([1, 2, 3], "Some additional context"),

            errorWithSource:
            """
            something
                should be
            [1, 2, 3]
                but was
            null

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            null
                should be
            [1, 2, 3]
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void DifferentEnumerableTypesShouldPass()
    {
        _thisString.ToArray().ShouldBe(_thisString);
        new List<int> { 1, 2, 3 }.ShouldBe(new[] { 1, 2, 3 });
    }

    [Fact]
    public void SameInstanceInDifferentInterfaceCollectionTypesShouldPass()
    {
        var foo = new Foo();

        IList<IFoo> a = new List<IFoo> { foo };

        a.ShouldBe([foo]);
    }

    private interface IFoo { }
    private class Foo : IFoo { }
    private readonly List<string> _thisOtherStringList = ["1", "3"];
    private readonly List<string> _thisString = ["1", "2"];
}