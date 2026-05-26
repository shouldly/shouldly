using System.Runtime.CompilerServices;
using Shouldly.Tests.CallerArgumentExpression;
using static Shouldly.Tests.CommonWaitDurations;

namespace Shouldly.Tests;

internal static class ModuleInitializer
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        ShouldlyConfiguration.DefaultTaskTimeout = LongWait;

        // Arm the global trip-wire so that any assertion that falls back to stack-trace parsing
        // (instead of using the [CallerArgumentExpression]-supplied value) throws. Individual call
        // sites that legitimately need stack-walking (e.g. dynamic dispatch, DisableSourceInErrors)
        // opt out via TripWireAccess.AllowStackWalking() or ShouldlyConfiguration.DisableSourceInErrors().
        // The disposable is deliberately not stored — the trip-wire stays armed for the entire
        // test run.
        _ = TripWireAccess.AssertCallerArgumentExpressionIsUsed();
    }
}