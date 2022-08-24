namespace Shouldly.Tests.ShouldBeOneOf;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
                one.ShouldBeOneOf(4, 5, 6),

            errorWithSource:
            @"one
    should be one of
[4, 5, 6]
    but was
1",

            errorWithoutSource:
            @"1
    should be one of
[4, 5, 6]
    but was not");
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeOneOf(1, 2, 3);
    }
}