namespace Shouldly.Tests.Strings.ShouldBeEmpty;

public class ActualIsNull
{
    [Fact]
    public void ActualIsNullShouldFail()
    {
        var str = (string?)null;

        Verify.ShouldFail(() =>
            str.ShouldBeEmpty("Some additional context"));
    }
}