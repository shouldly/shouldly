#if ShouldMatchApproved
using System.IO;
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
        [UsedImplicitly]
        public readonly DiffTool CodeCompare = new DiffTool("Code Compare", @"Devart\Code Compare\CodeMerge.exe", CodeCompareArgs);
        [UsedImplicitly]
        public readonly DiffTool P4Merge = new DiffTool("P4Merge", @"Perforce\p4merge.exe", P4MergeArgs);
        [UsedImplicitly]
        public readonly DiffTool TortoiseGitMerge = new DiffTool("Tortoise Git Merge", @"TortoiseGit\bin\TortoiseGitMerge.exe", TortoiseGitMergeArgs);
        [UsedImplicitly]
        public readonly DiffTool WinMerge = new DiffTool("WinMerge", @"WinMerge\WinMergeU.exe", WinMergeArgs);
        [UsedImplicitly]
        public readonly DiffTool CurrentVisualStudio = new CurrentlyRunningVisualStudioDiffTool();

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

        static string CodeCompareArgs(string received, string approved, bool approvedExists)
        {
            return $"/BF=\"{approved}\" /TF=\"{approved}\" /MF=\"{received}\" /RF=\"{approved}\"";
        }

        static string P4MergeArgs(string received, string approved, bool approvedExists)
        {
            if (!approvedExists)
                File.AppendAllText(approved, string.Empty);

            return $"\"{approved}\" \"{approved}\" \"{received}\" \"{approved}\"";
        }

        static string TortoiseGitMergeArgs(string received, string approved, bool approvedExists)
        {
            if (!approvedExists)
                File.AppendAllText(approved, string.Empty);

            return $"\"{received}\" \"{approved}\"";
        }

        static string WinMergeArgs(string received, string approved, bool approvedExists)
        {
            if (!approvedExists)
                File.AppendAllText(approved, string.Empty);

            return $"/u /wl \"{received}\" \"{approved}\" \"{approved}\"";
        }
    }
}
#endif
