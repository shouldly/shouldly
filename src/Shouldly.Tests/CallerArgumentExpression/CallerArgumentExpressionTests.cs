namespace Shouldly.Tests.CallerArgumentExpression;

public class CallerArgumentExpressionTests
{
    [Fact]
    public void ShouldBeTrue_uses_caller_expression()
    {
        var widget = new { IsReady = false };
        var ex = Should.Throw<ShouldAssertException>(() => widget.IsReady.ShouldBeTrue());
        ex.Message.ShouldContain("widget.IsReady");
    }

    [Fact]
    public void ShouldBeNull_uses_caller_expression()
    {
        var name = "not null";
        var ex = Should.Throw<ShouldAssertException>(() => name.ShouldBeNull());
        ex.Message.ShouldContain("name");
    }

    [Fact]
    public void Lambda_invocation_uses_caller_expression()
    {
        Func<int, bool> isPositive = x => x > 0;
        var ex = Should.Throw<ShouldAssertException>(() => isPositive(-5).ShouldBeTrue());
        ex.Message.ShouldContain("isPositive(-5)");
    }

    [Fact]
    public void Works_without_pdbs_via_DisableSourceInErrors_compat()
    {
        var ready = false;
        using (ShouldlyConfiguration.DisableSourceInErrors())
        {
            var ex = Should.Throw<ShouldAssertException>(() => ready.ShouldBeTrue());
            ex.Message.ShouldNotContain("ready");
        }
    }

    [Fact]
    public void ShouldBeEquivalentTo_uses_caller_expression()
    {
        var widget = new { Name = "left" };
        var other = new { Name = "right" };
        var ex = Should.Throw<ShouldAssertException>(() => widget.ShouldBeEquivalentTo(other));
        ex.Message.ShouldContain("widget");
    }
}
