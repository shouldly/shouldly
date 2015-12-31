#if !PORTABLE
using System.Diagnostics;

namespace Shouldly.Configuration
{
    public interface ITestMethodFinder
    {
        TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0);
    }
}
#endif