namespace Shouldly.Tests.ShouldSatisfyAllConditions;

// Should.Satisfy is the static form, for a group of unrelated assertions that share no common
// subject. When the assertions all hang off one value, use the value.ShouldSatisfy(...) extension
// overload instead (see GenericMultipleConditionsScenario).
public class MultipleConditionsScenario
{
    [Fact]
    public void MultipleConditionsScenarioShouldFail()
    {
        var name = "Homer";
        var age = 4;
        Verify.ShouldFail(() =>
            Should.Satisfy(
                [
                    () => name.ShouldBe("Marge", "Some additional context"),
                    () => age.ShouldBeGreaterThan(5, "Some additional context")
                ],
                "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var name = "Homer";
        var age = 4;
        Should.Satisfy(
        [
            () => name.ShouldBe("Homer"),
            () => age.ShouldBeGreaterThan(3)
        ]);
    }
}
