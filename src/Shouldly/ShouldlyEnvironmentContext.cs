namespace Shouldly;

static class ShouldlyEnvironmentContext
{
    public static bool IsWindows()
        => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static bool IsMac()
        => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    public static bool IsLinux()
        => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
}