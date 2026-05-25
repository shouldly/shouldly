namespace Shouldly;

// Internal test-infrastructure surface: a process-wide trip-wire that guarantees Shouldly
// assertions use their compile-time-captured CallerArgumentExpression value instead of
// falling back to stack-trace parsing. Reached from the test assembly via reflection
// (see Shouldly.Tests/CallerArgumentExpression/TripWireAccess) so it stays off the public API.
public static partial class ShouldlyConfiguration
{
    private static int _assertCallerArgumentExpressionIsUsedCount;

    /// <summary>
    /// Arms a process-wide trip-wire that throws <see cref="InvalidOperationException"/> if
    /// Shouldly ever falls back to stack-trace parsing to recover an assertion's call-site
    /// expression. Use to prove that
    /// <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/> capture is
    /// wired all the way through to the assertion message on a given test run.
    /// Stack-walking that is deliberately requested (via <see cref="DisableSourceInErrors"/>) or
    /// suppressed (via <see cref="AllowStackWalking"/>) does not trigger the trip-wire.
    /// </summary>
    internal static IDisposable AssertCallerArgumentExpressionIsUsed()
    {
        System.Threading.Interlocked.Increment(ref _assertCallerArgumentExpressionIsUsedCount);
        return new DisarmTripWireDisposable();
    }

    /// <summary>
    /// Scoped opt-out of the trip-wire armed by
    /// <see cref="AssertCallerArgumentExpressionIsUsed"/>. Use this around code paths that
    /// legitimately cannot use <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/>
    /// — for example, calls that go through dynamic dispatch (where CAE doesn't fire) or
    /// obsolete <c>params</c>-array overloads.
    /// </summary>
    internal static IDisposable AllowStackWalking()
    {
        // Capture the prior value so nested scopes compose correctly: when an inner scope
        // disposes it restores the outer scope's "true" rather than clearing the flag.
        var prior = (bool?)CallContext.LogicalGetData("ShouldlyAllowStackWalking");
        CallContext.LogicalSetData("ShouldlyAllowStackWalking", true);
        return new AllowStackWalkingDisposable(prior);
    }

    internal static bool IsCallerArgumentExpressionRequired()
    {
        if (System.Threading.Volatile.Read(ref _assertCallerArgumentExpressionIsUsedCount) == 0) return false;
        if ((bool?)CallContext.LogicalGetData("ShouldlyAllowStackWalking") == true) return false;
        if (IsSourceDisabledInErrors()) return false;
        return true;
    }

    private class DisarmTripWireDisposable : IDisposable
    {
        public void Dispose() => System.Threading.Interlocked.Decrement(ref _assertCallerArgumentExpressionIsUsedCount);
    }

    private class AllowStackWalkingDisposable(bool? prior) : IDisposable
    {
        public void Dispose() => CallContext.LogicalSetData("ShouldlyAllowStackWalking", prior);
    }
}
