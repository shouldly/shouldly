#if ShouldMatchApproved
using System;

namespace Shouldly.Configuration
{
    public class DoNotLaunchWhenPlatformIsNotWindows : IShouldNotLaunchDiffTool
    {
        public bool ShouldNotLaunch()
        {
            return System.Environment.OSVersion.Platform != PlatformID.Win32NT;
        }
    }
}
#endif