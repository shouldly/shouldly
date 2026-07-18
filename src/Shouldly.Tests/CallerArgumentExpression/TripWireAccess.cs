using System.Reflection;

namespace Shouldly.Tests.CallerArgumentExpression;

/// <summary>
/// Bridges to the internal trip-wire helpers on <see cref="ShouldlyConfiguration"/>. These
/// helpers exist purely as test infrastructure for this assembly and are deliberately not
/// part of Shouldly's public API, so we reach them via reflection rather than widening
/// access with InternalsVisibleTo.
/// </summary>
internal static class TripWireAccess
{
    private static readonly MethodInfo AssertMethod = GetMethod(nameof(AssertCallerArgumentExpressionIsUsed));
    private static readonly MethodInfo AllowMethod = GetMethod(nameof(AllowStackWalking));

    public static IDisposable AssertCallerArgumentExpressionIsUsed() =>
        (IDisposable)AssertMethod.Invoke(null, null)!;

    public static IDisposable AllowStackWalking() =>
        (IDisposable)AllowMethod.Invoke(null, null)!;

    private static MethodInfo GetMethod(string name) =>
        typeof(ShouldlyConfiguration).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic)
        ?? throw new MissingMethodException(nameof(ShouldlyConfiguration), name);
}
