namespace Shouldly.Tests.ShouldThrowExactly;

public class ActionDelegateScenario
{
    [Fact]
    public void ShouldPassWhenExactTypeIsThrown()
    {
        var expected = new ArgumentException();
        var action = new Action(() => throw expected);

        ArgumentException ex = action.ShouldThrowExactly<ArgumentException>();

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void ShouldPassWhenExactTypeIsThrown_Static()
    {
        var expected = new ArgumentException();

        var ex = Should.ThrowExactly<ArgumentException>(() => throw expected);

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void DerivedExceptionShouldFail()
    {
        var action = new Action(() => throw new ArgumentNullException());
        Verify.ShouldFail(() =>
            action.ShouldThrowExactly<ArgumentException>("Some additional context"));
    }

    [Fact]
    public void DerivedExceptionShouldFail_Static()
    {
        Verify.ShouldFail(() =>
            Should.ThrowExactly<ArgumentException>(() => throw new ArgumentNullException()));
    }

    [Fact]
    public void DerivedExceptionShouldFailWithThrownExceptionAsInnerException()
    {
        var thrown = new ArgumentNullException();
        var action = new Action(() => throw thrown);

        var ex = Should.Throw<ShouldAssertException>(() => action.ShouldThrowExactly<ArgumentException>());

        ex.InnerException.ShouldBeSameAs(thrown);
    }

    [Fact]
    public void UnrelatedExceptionShouldFail()
    {
        var action = new Action(() => throw new RankException());
        Verify.ShouldFail(() =>
            action.ShouldThrowExactly<InvalidOperationException>());
    }

    [Fact]
    public void NotThrowingShouldFail()
    {
        var action = () => { };
        Verify.ShouldFail(() =>
            action.ShouldThrowExactly<NotImplementedException>("Some additional context"));
    }
}
