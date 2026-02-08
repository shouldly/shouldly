namespace Shouldly.Tests.ShouldAllBe;

public class TypeBinaryExpressionScenario
{
    [Fact]
    public void TypeBinaryExpressionScenarioShouldFail()
    {
        var objects = new List<object> { "1", 1 };

        Verify.ShouldFail(() =>
            objects.ShouldAllBe(x => x is string, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "1", "2", "3" }.ShouldAllBe(x => x is string);
    }
}