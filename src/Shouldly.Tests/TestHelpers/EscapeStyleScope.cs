using System.Reflection;

namespace Shouldly.Tests.TestHelpers;

// Bridges to ShouldlyConfiguration.WithEscapeStyle, which is internal so the
// scoped-override mechanism isn't part of the library's public API. Tests need
// scoped overrides to avoid racing with parallel readers of the global config;
// since Shouldly is strong-named and granting IVT to the test assembly would
// require signing it, we hop the access barrier with reflection here.
static class EscapeStyleScope
{
    private static readonly MethodInfo WithEscapeStyle =
        typeof(ShouldlyConfiguration).GetMethod(
            "WithEscapeStyle",
            BindingFlags.Static | BindingFlags.NonPublic)!;

    public static IDisposable For(EscapeStyle escapeStyle) =>
        (IDisposable)WithEscapeStyle.Invoke(null, [escapeStyle])!;
}
