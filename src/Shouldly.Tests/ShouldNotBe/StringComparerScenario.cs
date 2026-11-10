namespace Shouldly.Tests.ShouldNotBe;

public class StringComparerScenario
{
    [Fact]
    public void StringComparerScenarioShouldFail()
    {
        const string upper = "A";
        Verify.ShouldFail(() =>
            upper.ShouldNotBe("a", StringComparer.OrdinalIgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "A".ShouldNotBe("b", StringComparer.OrdinalIgnoreCase);
    }
}
