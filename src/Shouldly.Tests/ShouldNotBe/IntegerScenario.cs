namespace Shouldly.Tests.ShouldNotBe;

public class IntegerScenario
{
    [Fact]
    public void IntegerScenarioShouldFail()
    {
        const int one = 1;
        Verify.ShouldFail(() =>
                one.ShouldNotBe(1),

            errorWithSource:
            @"one
    should not be
1
    but was",

            errorWithoutSource:
            @"1
    should not be
1
    but was");
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldNotBe(2);
    }
}