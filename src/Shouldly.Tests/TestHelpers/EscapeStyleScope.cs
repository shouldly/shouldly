namespace Shouldly.Tests.TestHelpers;

// Test-only IDisposable wrapper around the EscapeStyle save/restore dance.
// EscapeStyle is AsyncLocal-backed in Shouldly, so this naturally doesn't
// race with parallel readers — the override only applies to the current
// logical call context.
static class EscapeStyleScope
{
    public static IDisposable For(EscapeStyle escapeStyle)
    {
        var previous = ShouldlyConfiguration.EscapeStyle;
        ShouldlyConfiguration.EscapeStyle = escapeStyle;
        return new Restore(previous);
    }

    private sealed class Restore(EscapeStyle previous) : IDisposable
    {
        public void Dispose() => ShouldlyConfiguration.EscapeStyle = previous;
    }
}
