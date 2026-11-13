using System.Numerics;

namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// When the two sides of a <c>ShouldBe</c> failure read the same, or two sequences first differ where it takes
/// finding, the message closes with a line that says why they are unequal (#1432). The explanations themselves are
/// tested in <c>ExplanationWordingTests</c>; these show the assertions asking for one, and where it goes in the message.
/// </summary>
public class ValuesThatReadTheSameScenarios
{
    [Fact]
    public void RecordWithCollectionMemberShouldFail()
    {
        var order = new Order(1, ["a"]);

        Verify.ShouldFail(() =>
            order.ShouldBe(new(1, ["a"]), "Some additional context"));
    }

    [Fact]
    public void DifferentTypesShouldFail()
    {
        object count = new BigInteger(5);

        Verify.ShouldFail(() =>
            count.ShouldBe(5));
    }

    [Fact]
    public void ReferenceEqualityShouldFail()
    {
        var thing = new Thing();

        Verify.ShouldFail(() =>
            thing.ShouldBe(new()));
    }

    [Fact]
    public void EqualityRuleShouldFail()
    {
        var bag = new IdBag(1, 1, 2, 3);

        Verify.ShouldFail(() =>
            bag.ShouldBe(new IdBag(2, 1, 2, 3)));
    }

    [Fact]
    public void CustomComparerShouldFail()
    {
        var actual = new Named("thing");

        Verify.ShouldFail(() =>
            actual.ShouldBe(new("thing"), new NeverEqual<Named>()));
    }

    [Fact]
    public void ItemsThatReadTheSameShouldFail()
    {
        Order[] orders = [new(1, ["a"]), new(2, ["b"])];

        Verify.ShouldFail(() =>
            orders.ShouldBe([new(1, ["a"]), new(2, ["b"])], "Some additional context"));
    }

    [Fact]
    public void LongSequenceShouldFail()
    {
        var numbers = Enumerable.Range(0, 20).ToArray();
        var expected = Enumerable.Range(0, 20).Select(n => n == 15 ? -1 : n).ToArray();

        Verify.ShouldFail(() =>
            numbers.ShouldBe(expected));
    }

    [Fact]
    public void LongSequenceOfStringsShouldFail()
    {
        var lines = Enumerable.Range(0, 12).Select(n => $"line {n}").ToArray();
        var expected = Enumerable.Range(0, 12).Select(n => n == 11 ? "LINE 11" : $"line {n}").ToArray();

        Verify.ShouldFail(() =>
            lines.ShouldBe(expected));
    }

    [Fact]
    public void CaseInsensitiveSequenceOfStringsShouldFail()
    {
        // Strings that differ only in case were equal as the assertion compared them, so the first difference is
        // the one after them.
        var lines = Enumerable.Range(0, 12).Select(n => $"line {n}").ToArray();
        var expected = Enumerable.Range(0, 12).Select(n => n == 11 ? "other" : $"LINE {n}").ToArray();

        Verify.ShouldFail(() =>
            lines.ShouldBe(expected, Case.Insensitive));
    }

    [Fact]
    public void DifferencePastTheItemsShownShouldFail()
    {
        var numbers = Enumerable.Range(0, 10).ToArray();
        var expected = Enumerable.Range(0, 10).Select(n => n == 7 ? -1 : n).ToArray();

        using (MaxEnumerableItemsScope.Of(3))
        {
            Verify.ShouldFail(() => numbers.ShouldBe(expected));
        }
    }

    [Fact]
    public void ShouldNotBeIsNotExplained()
    {
        // The values were equal, so there is no inequality to explain.
        var actual = new Named("thing");

        Verify.ShouldFail(() =>
            actual.ShouldNotBe(actual));
    }

    [Fact]
    public void CustomAssertionNamedShouldBeIsNotExplained()
    {
        // A custom assertion states its values, not how it compared them, so Shouldly can't say why they differ.
        new ExpectedActualShouldlyMessage(new Named("thing"), new Named("thing"), null, "ShouldBe", "actual").ToString()
            .ShouldBe(
                """
                actual
                    should be
                thing
                    but was
                thing
                """, StringCompareShould.IgnoreLineEndings);
    }

    private sealed record Order(int Id, List<string> Lines);

    private sealed class Thing
    {
        public int Size { get; set; }
    }

    private sealed class Named(string name)
    {
        public override string ToString() => name;
    }

    private sealed class NeverEqual<T> : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y) => false;

        public int GetHashCode(T obj) => 0;
    }
}
