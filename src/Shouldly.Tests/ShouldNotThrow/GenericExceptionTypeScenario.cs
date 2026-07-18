namespace Shouldly.Tests.ShouldNotThrow;

public class GenericExceptionTypeScenario
{
    [Fact]
    public void ShouldPassWhenNoExceptionIsThrown()
    {
        Should.NotThrow<InvalidOperationException>(() => { });

        var action = new Action(() => { });
        action.ShouldNotThrow<InvalidOperationException>();
    }

    [Fact]
    public void OtherExceptionTypesPropagateToTheCaller()
    {
        var original = new ArgumentException("original");
        var action = new Action(() => throw original);

        Should.Throw<ArgumentException>(() => Should.NotThrow<InvalidOperationException>(action))
            .ShouldBeSameAs(original);

        Should.Throw<ArgumentException>(() => action.ShouldNotThrow<InvalidOperationException>())
            .ShouldBeSameAs(original);
    }

    [Fact]
    [UseCulture("en-US")]
    public void StaticMethodShouldFailWhenExceptionTypeIsThrown()
    {
        var action = new Action(() => throw new InvalidOperationException());
        Verify.ShouldFail(() =>
            Should.NotThrow<InvalidOperationException>(action, "Some additional context"));
    }

    [Fact]
    [UseCulture("en-US")]
    public void ExtensionMethodShouldFailWhenExceptionTypeIsThrown()
    {
        var action = new Action(() => throw new InvalidOperationException());
        Verify.ShouldFail(() =>
            action.ShouldNotThrow<InvalidOperationException>("Some additional context"));
    }

    [Fact]
    [UseCulture("en-US")]
    public void ShouldFailWhenDerivedExceptionTypeIsThrown()
    {
        var action = new Action(() => throw new InvalidOperationException("the operation failed"));
        Verify.ShouldFail(() =>
            Should.NotThrow<SystemException>(action, "Some additional context"));
    }
}
