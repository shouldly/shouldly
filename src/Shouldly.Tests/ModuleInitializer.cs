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

        // Equality rules, registered before any test runs as a consumer would. Strange emulates JValue (an empty
        // enumerable of itself that overrides Equals); Node emulates JToken as a whole (an abstract base whose
        // subtypes compare by reference but have a static DeepEquals); CaseInsensitiveLeaf tests that the most
        // specific rule wins.
        ShouldlyConfiguration.UseEquality<Strange>((expected, actual) => expected.Equals(actual));
        ShouldlyConfiguration.UseEquality<IdBag>((expected, actual) => expected.Equals(actual));
        ShouldlyConfiguration.UseEquality<Node>(Node.DeepEquals);
        ShouldlyConfiguration.UseEquality<CaseInsensitiveLeaf>((expected, actual) =>
            string.Equals(expected.Text, actual.Text, StringComparison.OrdinalIgnoreCase));

        // Arm the global trip-wire so that any assertion that falls back to stack-trace parsing
        // (instead of using the [CallerArgumentExpression]-supplied value) throws. Individual call
        // sites that legitimately need stack-walking (e.g. dynamic dispatch) opt out via
        // ShouldlyConfiguration.AllowStackWalking(); ShouldlyConfiguration.DisableSourceInErrors() also
        // suppresses the trip-wire because it explicitly opts out of stack-walking. The disposable
        // is deliberately not stored — the trip-wire stays armed for the entire test run.
        _ = ShouldlyConfiguration.AssertCallerArgumentExpressionIsUsed();
    }
}
