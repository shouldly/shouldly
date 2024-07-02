namespace Shouldly.Tests;

internal static class CommonWaitDurations
{
    public static TimeSpan ShortWait
    {
        get
        {
            if (Environment.GetEnvironmentVariable("CI") == "true")
            {
                return TimeSpan.FromSeconds(0.5);
            }
            return TimeSpan.FromSeconds(0.2);
        }
    }

    public static TimeSpan LongWait
    {
        get
        {
            if (Environment.GetEnvironmentVariable("CI") == "true")
            {
                return TimeSpan.FromSeconds(15);
            }
            return TimeSpan.FromSeconds(5);
        }
    }

    public static TimeSpan ImmediateTaskTimeout
    {
        get
        {
            if (Environment.GetEnvironmentVariable("CI") == "true")
            {
                return TimeSpan.FromSeconds(2);    
            }
            return TimeSpan.FromSeconds(0.1);
        }
    }
}