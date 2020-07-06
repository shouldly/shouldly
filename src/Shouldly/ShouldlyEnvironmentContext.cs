namespace Shouldly
{
    #if NETSTANDARD
    internal static class ShouldlyEnvironmentContext
    {
        public static bool IsWindows()
           => System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);

        public static bool IsMac()
            => System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX);

        public static bool IsLinux()
           => System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux);
    }
    #else
    internal static class ShouldlyEnvironmentContext
    {
        public static bool IsWindows()
            => System.Environment.OSVersion.Platform == System.PlatformID.Win32NT;

        public static bool IsMac()
            => System.Environment.OSVersion.Platform == System.PlatformID.MacOSX;

        public static bool IsLinux()
            => System.Environment.OSVersion.Platform == System.PlatformID.Unix;
    }
    #endif
}