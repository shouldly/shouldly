namespace Shouldly.Tests;

internal static class CommonWaitDurations
{
    private static readonly bool IsRunningOnContinuousIntegration =
        Environment.GetEnvironmentVariable("CI")
            ?.Equals("true", StringComparison.OrdinalIgnoreCase)
        ?? false;

    public static TimeSpan ShortWait => 
        TimeSpan.FromSeconds(IsRunningOnContinuousIntegration ? 0.5 : 0.2);

    public static TimeSpan LongWait =>
        TimeSpan.FromSeconds(IsRunningOnContinuousIntegration ? 15 : 5);

    public static TimeSpan ImmediateTaskTimeout =>
        TimeSpan.FromSeconds(IsRunningOnContinuousIntegration ? 2 : 0.1);
}