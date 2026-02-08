namespace Shouldly.Tests.ShouldBeEmpty;

public class GuidScenario
{
    [Fact]
    public void ShouldFail()
    {
        Guid myGuid = new Guid(1,2,3,4,5,6,7,8,9,10,11);

        Verify.ShouldFail(() =>
            myGuid.ShouldBeEmpty("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        Guid.Empty.ShouldBeEmpty();
    }
}