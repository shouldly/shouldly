namespace Shouldly.Tests.ShouldSatisfyAllConditions;

// ShouldSatisfyAllConditions is the obsolete 4.3.0-shaped alias for ShouldSatisfy. These tests
// deliberately exercise the obsolete params overloads to prove they still collect every failure
// and honour the custom message. They assert on message fragments rather than a full approved
// file because, without a CallerArgumentExpression, the rendered code part differs by target
// framework (stack-walked source on netstandard2.0/net48 versus the value elsewhere).
#pragma warning disable CS0618 // Type or member is obsolete

public class ObsoleteShouldSatisfyAllConditionsScenario
{
    [Fact]
    public void ParamsOverloadCollectsAllFailures()
    {
        var result = 4;
        var ex = Should.Throw<ShouldAssertException>(() =>
            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeOfType<float>(),
                () => result.ShouldBeGreaterThan(5)));

        ex.Message.ShouldContain("should satisfy all the conditions specified, but does not");
        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
    }

    [Fact]
    public void CustomMessageOverloadIncludesCustomMessage()
    {
        var result = 4;
        var ex = Should.Throw<ShouldAssertException>(() =>
            result.ShouldSatisfyAllConditions(
                "Some additional context",
                () => result.ShouldBeGreaterThan(5)));

        ex.Message.ShouldContain("Some additional context");
    }

    [Fact]
    public void GenericParamsOverloadCollectsAllFailures()
    {
        var result = 4;
        var ex = Should.Throw<ShouldAssertException>(() =>
            result.ShouldSatisfyAllConditions(
                r => r.ShouldBeOfType<float>(),
                r => r.ShouldBeGreaterThan(5)));

        ex.Message.ShouldContain("Error 1");
        ex.Message.ShouldContain("Error 2");
    }

    [Fact]
    public void GenericCustomMessageOverloadIncludesCustomMessage()
    {
        var result = 4;
        var ex = Should.Throw<ShouldAssertException>(() =>
            result.ShouldSatisfyAllConditions(
                "Some additional context",
                r => r.ShouldBeGreaterThan(5)));

        ex.Message.ShouldContain("Some additional context");
    }

    [Fact]
    public void AllConditionsPassDoesNotThrow()
    {
        var result = 4;
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeOfType<int>(),
            () => result.ShouldBeGreaterThan(3));
    }

    [Fact]
    public void GenericAllConditionsPassDoesNotThrow()
    {
        var result = 4;
        result.ShouldSatisfyAllConditions(
            r => r.ShouldBeOfType<int>(),
            r => r.ShouldBeGreaterThan(3));
    }
}

#pragma warning restore CS0618
