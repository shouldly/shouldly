using System.Runtime.InteropServices;

namespace Shouldly
{
    internal static class ShouldlyEnvironmentContext
    {
        public static bool IsWindows()
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMac()
            => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }
}