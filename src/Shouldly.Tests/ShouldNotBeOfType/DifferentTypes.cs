namespace Shouldly.Tests.ShouldNotBeOfType;

public class BasicTypesScenario
{
    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
                one.ShouldNotBeOfType<int>("Some additional context"),

            errorWithSource:
            @"one
    should not be of type
System.Int32
    but was
1

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"1
    should not be of type
System.Int32
    but was

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldNotBeOfType<string>();
    }
}