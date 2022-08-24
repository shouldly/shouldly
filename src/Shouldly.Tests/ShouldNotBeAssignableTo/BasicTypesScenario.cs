namespace Shouldly.Tests.ShouldNotBeAssignableTo;

public class BasicTypesScenario
{
    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        var two = 2;
        Verify.ShouldFail(() =>
                two.ShouldNotBeAssignableTo<int>("Some additional context"),

            errorWithSource:
            @"two
    should not be assignable to
System.Int32
    but was
2

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"2
    should not be assignable to
System.Int32
    but was

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldNotBeAssignableTo<string>();
    }
}