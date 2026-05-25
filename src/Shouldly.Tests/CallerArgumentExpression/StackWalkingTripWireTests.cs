namespace Shouldly.Tests.CallerArgumentExpression;

/// <summary>
/// Verifies the trip-wire armed by <see cref="ModuleInitializer"/> (via the internal
/// <c>AssertCallerArgumentExpressionIsUsed</c> helper on <see cref="ShouldlyConfiguration"/>)
/// fires when expected and stays silent on the deliberate opt-outs.
/// </summary>
public class StackWalkingTripWireTests
{
    [Fact]
    public void Trip_wire_fires_when_actualExpression_is_null()
    {
        // Construct the context directly with null actualExpression, simulating an assertion
        // that failed to thread CAE through to the message constructor.
        var ex = Should.Throw<InvalidOperationException>(() =>
            new ShouldlyAssertionContext("FakeAssertionMethod", expected: 1, actual: 2, actualExpression: null));

        ex.Message.ShouldContain("FakeAssertionMethod");
        ex.Message.ShouldContain("AssertCallerArgumentExpressionIsUsed");
    }

    [Fact]
    public void Trip_wire_is_suppressed_inside_AllowStackWalking_scope()
    {
        using (TripWireAccess.AllowStackWalking())
        {
            // No throw — the scope opts out of the trip-wire for code paths that legitimately
            // cannot use CAE (e.g. dynamic dispatch).
            _ = new ShouldlyAssertionContext("FakeAssertionMethod", expected: 1, actual: 2, actualExpression: null);
        }
    }

    [Fact]
    public void Trip_wire_is_suppressed_when_source_is_disabled()
    {
        using (ShouldlyConfiguration.DisableSourceInErrors())
        {
            // No throw — when the user has deliberately turned off source-in-errors, falling
            // back to the value-only message is the expected behaviour.
            _ = new ShouldlyAssertionContext("FakeAssertionMethod", expected: 1, actual: 2, actualExpression: null);
        }
    }

    [Fact]
    public void Trip_wire_stays_silent_when_actualExpression_is_supplied()
    {
        // The whole point: when CAE supplies the expression, no stack-walking happens, no throw.
        var context = new ShouldlyAssertionContext("FakeAssertionMethod", expected: 1, actual: 2, actualExpression: "someVariable");
        context.CodePart.ShouldBe("someVariable");
    }
}
