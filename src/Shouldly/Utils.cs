namespace Shouldly;

internal static class Utils
{
    public static IDisposable WithSynchronizationContext(SynchronizationContext? synchronizationContext)
    {
        var originalSynchronizationContext = SynchronizationContext.Current;
        SynchronizationContext.SetSynchronizationContext(synchronizationContext);

        return On.Dispose(() =>
        {
            if (SynchronizationContext.Current == synchronizationContext)
                SynchronizationContext.SetSynchronizationContext(originalSynchronizationContext);
        });
    }
}