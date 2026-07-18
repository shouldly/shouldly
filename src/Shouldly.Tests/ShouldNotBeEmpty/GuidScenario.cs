namespace Shouldly.Tests.ShouldNotBeEmpty;

public class GuidScenario
{
    [Fact]
    public void ShouldFail()
    {
        Guid myGuid = Guid.Empty;

        Verify.ShouldFail(() =>
            myGuid.ShouldNotBeEmpty("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        Guid.NewGuid().ShouldNotBeEmpty();
    }
}
