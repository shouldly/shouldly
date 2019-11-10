#if ShouldMatchApproved
namespace Shouldly.Configuration.DiffTools
{
    internal abstract class BeyondCompare : DiffTool
    {
        public BeyondCompare(string name, DiffToolConfig config, ArgumentGenerator argGenerator)
            : base(name, config, argGenerator) { }

        protected static string BeyondCompareArgs(string received, string approved)
        {
            CreateEmptyFileIfNotExists(approved);

            return $"\"{approved}\" \"{received}\" " + (ShouldlyEnvironmentContext.IsWindows() ? "/" : "-") + $"mergeoutput=\"{approved}\"";
        }
    }

    internal sealed class BeyondCompare3 : BeyondCompare
    {
        public BeyondCompare3() : base("Beyond Compare 3", new DiffToolConfig
        {
            WindowsPath = @"Beyond Compare 3\BCompare.exe",
            LinuxPath = @"bcompare"
        }, BeyondCompareArgs) { }
    }

    internal sealed class BeyondCompare4 : BeyondCompare
    {
        public BeyondCompare4() : base("Beyond Compare 4", new DiffToolConfig
        {
            WindowsPath = @"Beyond Compare 4\BCompare.exe",
            MacPath = @"Beyond Compare.app/Contents/MacOS/bcomp",
            LinuxPath = @"bcompare"
        }, BeyondCompareArgs) { }
    }
}
#endif