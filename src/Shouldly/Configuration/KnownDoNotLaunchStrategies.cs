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
        public readonly IShouldNotLaunchDiffTool AppVeyor = new DoNotLaunchWhenEnvVariableIsPresent("APPVEYOR");
        [UsedImplicitly]
        public readonly IShouldNotLaunchDiffTool VSTS = new DoNotLaunchWhenEnvVariableIsPresent("TF_BUILD");
        [UsedImplicitly]
        public readonly IShouldNotLaunchDiffTool GitLabCI = new DoNotLaunchWhenEnvVariableIsPresent("GITLAB_CI");
        [UsedImplicitly]
        public readonly IShouldNotLaunchDiffTool Jenkins = new DoNotLaunchWhenEnvVariableIsPresent("JENKINS_URL");
        [UsedImplicitly]
        public readonly IShouldNotLaunchDiffTool MyGet = new DoNotLaunchWhenEnvVariableIsPresent("BuildRunner");
        [UsedImplicitly]
        public readonly IShouldNotLaunchDiffTool TravisCI = new DoNotLaunchWhenEnvVariableIsPresent("TRAVIS");
    }
}
#endif
