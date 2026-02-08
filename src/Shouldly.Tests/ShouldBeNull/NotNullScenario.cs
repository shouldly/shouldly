namespace Shouldly.Tests.ShouldBeNull;

public class NotNullScenario
{
    [Fact]
    public void ShouldFailForNullReference()
    {
        string? myNullRef = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
            myNullRef.ShouldNotBeNull("Some additional context"));
    }

    [Fact]
    public void ShouldPassForNonNullReference()
    {
        var returnValue = "Hello World".ShouldNotBeNull();
        returnValue.ShouldBe("Hello World");
    }

    [Fact]
    public void ShouldFailForSystemNullableWithoutValue()
    {
        int? myNullRef = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
            myNullRef.ShouldNotBeNull("Some additional context"));
    }

    [Fact]
    public void ShouldPassForSystemNullableWithValue()
    {
        var returnValue = ((int?)0).ShouldNotBeNull();
        returnValue.ShouldBe(0);
    }
}