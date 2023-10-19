public class ArrayScenario
{
    [Fact]
    public void ArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 1 }.ShouldBeEmpty("Some additional context"),

            errorWithSource:
            @"new[] { 1 }
    should be empty but had
1
    item and was
[1]

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[1]
    should be empty but had
1
    item and was not empty

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new int[0].ShouldBeEmpty();
    }
}