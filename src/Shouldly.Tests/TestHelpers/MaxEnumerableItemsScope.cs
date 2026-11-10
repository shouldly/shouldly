namespace Shouldly.Tests.TestHelpers;

// MaxEnumerableItemsInMessages is AsyncLocal-backed, so an override here can't leak into
// tests running in parallel — it still needs restoring for later tests on this context.
static class MaxEnumerableItemsScope
{
    public static IDisposable Of(int value)
    {
        var previous = ShouldlyConfiguration.MaxEnumerableItemsInMessages;
        ShouldlyConfiguration.MaxEnumerableItemsInMessages = value;
        return new Restore(previous);
    }

    private sealed class Restore(int previous) : IDisposable
    {
        public void Dispose() => ShouldlyConfiguration.MaxEnumerableItemsInMessages = previous;
    }
}
