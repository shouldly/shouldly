namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfStringIsInsensitiveIgnoreOrderScenario
{
    [Fact]
    public void EnumerableOfStringIsInsensitiveIgnoreOrderScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "A", "b", "c" }.ShouldBe(["B", "a", "d"], Case.Insensitive, ignoreOrder: true, "Some additional context"));
    }

    [Fact]
    public void DuplicateCountMismatchScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new List<string> { "a", "A", "b" }.ShouldBe(["a", "B", "b"], Case.Insensitive, ignoreOrder: true));
    }

    [Fact]
    public void CaseSensitiveIgnoreOrderScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "A", "b" }.ShouldBe(["B", "a"], Case.Sensitive, ignoreOrder: true));
    }

    [Fact]
    public void CaseInsensitiveRespectingOrderScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "A", "b" }.ShouldBe(["B", "a"], Case.Insensitive, ignoreOrder: false));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "A", "b" }.ShouldBe(["B", "a"], Case.Insensitive, ignoreOrder: true);
        new List<string> { "A", "b" }.ShouldBe(["B", "a"], Case.Insensitive, true);
        new[] { "A", "b" }.ShouldBe(["a", "B"], Case.Insensitive, ignoreOrder: false, "Some additional context");
        new[] { "A", "b" }.ShouldBe(["b", "A"], Case.Sensitive, ignoreOrder: true);
    }
}
