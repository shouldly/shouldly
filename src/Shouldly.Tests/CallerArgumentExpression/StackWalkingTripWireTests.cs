namespace Shouldly.Tests.CallerArgumentExpression;

/// <summary>
/// Verifies the trip-wire armed by <see cref="ModuleInitializer"/> (via
/// <see cref="ShouldlyConfiguration.AssertCallerArgumentExpressionIsUsed"/>)
/// fires when expected and stays silent on the deliberate opt-outs.
/// </summary>
public class StackWalkingTripWireTests
{
    [Fact]
    public void Trip_wire_fires_when_actualExpression_is_null()
    {
        // Construct a message with null actualExpression, simulating an assertion that failed to
        // thread CAE through to the message constructor.
        var ex = Should.Throw<InvalidOperationException>(() =>
            new ExpectedActualShouldlyMessage(1, 2, null, "FakeAssertionMethod", actualExpression: null));

        ex.Message.ShouldContain("FakeAssertionMethod");
        ex.Message.ShouldContain("AssertCallerArgumentExpressionIsUsed");
    }

    [Fact]
    public void Trip_wire_is_suppressed_inside_AllowStackWalking_scope()
    {
        using (ShouldlyConfiguration.AllowStackWalking())
        {
            // No throw — the scope opts out of the trip-wire for code paths that legitimately
            // cannot use CAE (e.g. dynamic dispatch).
            _ = new ExpectedActualShouldlyMessage(1, 2, null, "FakeAssertionMethod", actualExpression: null);
        }
    }

    [Fact]
    public void Trip_wire_is_suppressed_when_source_is_disabled()
    {
        using (ShouldlyConfiguration.DisableSourceInErrors())
        {
            // No throw — the user has deliberately opted out of stack-walking, so falling back
            // to the value-only message is the expected behaviour.
            _ = new ExpectedActualShouldlyMessage(1, 2, null, "FakeAssertionMethod", actualExpression: null);
        }
    }

    [Fact]
    public void Trip_wire_stays_silent_when_actualExpression_is_supplied()
    {
        // The whole point: when CAE supplies the expression, no stack-walking happens, no throw.
        var message = new ExpectedActualShouldlyMessage(1, 2, null, "FakeAssertionMethod", actualExpression: "someVariable");
        message.ToString().ShouldStartWith("someVariable");
    }

    [Fact]
    public void AllowStackWalking_nests_correctly()
    {
        using (ShouldlyConfiguration.AllowStackWalking())
        {
            using (ShouldlyConfiguration.AllowStackWalking())
            {
                // Inner scope active — no throw.
                _ = new ExpectedActualShouldlyMessage(1, 2, null, "FakeAssertionMethod", actualExpression: null);
            }

            // Inner scope disposed, outer scope still active — must still suppress the trip-wire.
            _ = new ExpectedActualShouldlyMessage(1, 2, null, "FakeAssertionMethod", actualExpression: null);
        }

        // Both scopes disposed — the trip-wire is armed again.
        Should.Throw<InvalidOperationException>(() =>
            new ExpectedActualShouldlyMessage(1, 2, null, "FakeAssertionMethod", actualExpression: null));
    }
}
