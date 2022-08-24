namespace Shouldly.Tests.ShouldBeOfType;

public class BasicTypesScenario
{
    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
                one.ShouldBeOfType<string>("Some additional context"),

            errorWithSource:
            @"one
    should be of type
System.String
    but was
System.Int32

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"1
    should be of type
System.String
    but was
System.Int32

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeOfType<int>();
    }
}