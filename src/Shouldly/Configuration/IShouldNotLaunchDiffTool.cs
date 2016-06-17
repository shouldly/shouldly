#if ShouldMatchApproved
namespace Shouldly.Configuration
{
    public interface IShouldNotLaunchDiffTool
    {
        bool ShouldNotLaunch();
    }
}
#endif