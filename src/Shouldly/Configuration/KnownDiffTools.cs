#if ShouldMatchApproved
using JetBrains.Annotations;

namespace Shouldly.Configuration
{
    public class KnownDiffTools
    {
        [UsedImplicitly]
        public readonly DiffTool KDiff3 = new DiffTool("KDiff3", @"KDiff3\kdiff3.exe", KDiffArgs);
        [UsedImplicitly]
        public readonly DiffTool BeyondCompare3 = new DiffTool("Beyond Compare 3", @"Beyond Compare 3\BCompare.exe", BeyondCompareArgs);
        [UsedImplicitly]
        public readonly DiffTool BeyondCompare4 = new DiffTool("Beyond Compare 4", @"Beyond Compare 4\BCompare.exe", BeyondCompareArgs);

        public static KnownDiffTools Instance { get; } = new KnownDiffTools();

        static string BeyondCompareArgs(string received, string approved, bool approvedExists)
        {
            return approvedExists
                ? $"\"{received}\" \"{approved}\" /mergeoutput=\"{approved}\""
                : $"\"{received}\" /mergeoutput=\"{approved}\"";
        }

        static string KDiffArgs(string received, string approved, bool approvedExists)
        {
            return approvedExists
                ? $"\"{received}\" \"{approved}\" -o \"{approved}\""
                : $"\"{received}\" -o \"{approved}\"";
        }
    }
}
#endif
