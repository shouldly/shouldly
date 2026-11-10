using System.Runtime.CompilerServices;
using Shouldly.Tests.TestHelpers;
using static Shouldly.Tests.CommonWaitDurations;

namespace Shouldly.Tests;

internal static class ModuleInitializer
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        ShouldlyConfiguration.DefaultTaskTimeout = LongWait;

        // Strange emulates JToken (an enumerable of itself that overrides Equals). Registering it here,
        // before any test runs, keeps it out of the defaults Shouldly ships to consumers (issue #1410).
        ShouldlyConfiguration.CompareAsObjectTypes.Add(typeof(Strange).FullName!);
        ShouldlyConfiguration.CompareAsObjectTypes.Add(typeof(IdBag).FullName!);

        // Arm the global trip-wire so that any assertion that falls back to stack-trace parsing
        // (instead of using the [CallerArgumentExpression]-supplied value) throws. Individual call
        // sites that legitimately need stack-walking (e.g. dynamic dispatch) opt out via
        // ShouldlyConfiguration.AllowStackWalking(); ShouldlyConfiguration.DisableSourceInErrors() also
        // suppresses the trip-wire because it explicitly opts out of stack-walking. The disposable
        // is deliberately not stored — the trip-wire stays armed for the entire test run.
        _ = ShouldlyConfiguration.AssertCallerArgumentExpressionIsUsed();
    }
}
