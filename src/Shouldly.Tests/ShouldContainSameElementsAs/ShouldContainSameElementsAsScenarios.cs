namespace Shouldly.Tests.ShouldContainSameElementsAs;

public class ShouldContainSameElementsAsScenarios
{
    [Fact]
    public void ShouldContainSameElementsAs_ShouldFail() =>
        Verify.ShouldFail(() =>
                new List<int> { 1, 4, 2 }.ShouldContainSameElementsAs([1, 2, 3], "Some additional context"),

            errorWithSource:
            """
            new List<int> { 1, 4, 2 }
                should contain same elements as (ignoring order)
            [1, 2, 3]
                but
            new List<int> { 1, 4, 2 }
                is missing
            [3]
                and
            [1, 2, 3]
                is missing
            [4]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 4, 2]
                should contain same elements as (ignoring order)
            [1, 2, 3]
                but
            [1, 4, 2]
                is missing
            [3]
                and
            [1, 2, 3]
                is missing
            [4]

            Additional Info:
                Some additional context
            """);

    [Fact]
    public void ShouldPass() =>
        new List<int> { 1, 3, 2 }.ShouldContainSameElementsAs([1, 2, 3]);

    [Fact]
    public void ComparerScenario_ShouldPass() =>
        new[] { "A", "b" }.ShouldContainSameElementsAs(new[] { "b", "a" }, StringComparer.OrdinalIgnoreCase);

    [Fact]
    public void Duplicates_WithMatchingCounts_DifferentOrder_ShouldPass() =>
        new List<int> { 1, 2, 1 }.ShouldContainSameElementsAs([1, 1, 2]);

    [Fact]
    public void MultiplicityMismatch_ActualHasExtra_ShouldFail() =>
        Should.Throw<ShouldAssertException>(() =>
            new List<int> { 1, 1, 2 }.ShouldContainSameElementsAs([1, 2], "Some additional context"));

    [Fact]
    public void BothEmpty_ShouldPass() =>
        new List<int>().ShouldContainSameElementsAs(Array.Empty<int>());

    [Fact]
    public void EmptyActual_ShouldFail() =>
        Should.Throw<ShouldAssertException>(() =>
            new List<int>().ShouldContainSameElementsAs([1], "Some additional context"));

    [Fact]
    public void NullElements_Equal_ShouldPass() =>
        new string?[] { null, "a" }.ShouldContainSameElementsAs(new string?[] { "a", null });

    [Fact]
    public void NullPresenceMismatch_ShouldFail() =>
        Should.Throw<ShouldAssertException>(() =>
            new string?[] { "a", null }.ShouldContainSameElementsAs(new string?[] { "a" }, "Some additional context"));

    [Fact]
    public void ComparerMultiplicityMismatch_ShouldFail() =>
        Should.Throw<ShouldAssertException>(() =>
            new[] { "a", "A" }.ShouldContainSameElementsAs(new[] { "a" }, StringComparer.OrdinalIgnoreCase, "Some additional context"));

    [Fact]
    public void LazyEnumerables_ShouldPass()
    {
        IEnumerable<int> Yield(params int[] items)
        {
            foreach (var i in items) yield return i;
        }

        Yield(1, 2, 3).ShouldContainSameElementsAs(Yield(3, 2, 1));
    }

    [Fact]
    public void NumericSpecials_ShouldPass() =>
        new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity }
            .ShouldContainSameElementsAs(new[] { double.NegativeInfinity, double.NaN, double.PositiveInfinity });

    [Fact]
    public void Regression_ShouldContain_UnchangedGenerator_ShouldFail() =>
        Should.Throw<ShouldAssertException>(() =>
            new[] { 1, 2 }.ShouldContain(3, "Some additional context"));
}