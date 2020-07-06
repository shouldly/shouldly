namespace Shouldly.Configuration
{
    public class DoNotLaunchWhenPlatformIsNotWindows : IShouldNotLaunchDiffTool
    {
        public bool ShouldNotLaunch()
        {
            return !ShouldlyEnvironmentContext.IsWindows();
        }
    }
}