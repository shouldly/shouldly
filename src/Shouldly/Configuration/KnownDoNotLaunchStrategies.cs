#if ShouldMatchApproved
using JetBrains.Annotations;

namespace Shouldly.Configuration
{
    public class KnownDoNotLaunchStrategies
    {
        [UsedImplicitly]
        public readonly IShouldNotLaunchDiffTool NCrunch = new DoNotLaunchWhenEnvVariableIsPresent("NCRUNCH");
        [UsedImplicitly]
        public readonly IShouldNotLaunchDiffTool TeamCity = new DoNotLaunchWhenEnvVariableIsPresent("TeamCity");
        [UsedImplicitly]
        public readonly IShouldNotLaunchDiffTool AppVeyor = new DoNotLaunchWhenEnvVariableIsPresent("AppVeyor");
    }
}
#endif
