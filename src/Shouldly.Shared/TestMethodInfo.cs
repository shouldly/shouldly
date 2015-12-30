using System.Diagnostics;
using System.IO;

#if !PORTABLE
namespace Shouldly
{
    public class TestMethodInfo
    {
        public TestMethodInfo(StackFrame callingFrame)
        {
            SourceFileDirectory = Path.GetDirectoryName(callingFrame.GetFileName());
            MethodName = callingFrame.GetMethod().Name;
        }

        public string SourceFileDirectory { get; private set; }
        public string MethodName { get; private set; }
    }
}
#endif