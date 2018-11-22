#if ShouldMatchApproved
using System.IO;
using JetBrains.Annotations;

namespace Shouldly.Configuration
{
    public class KnownDiffTools
    {
        [UsedImplicitly]
        public readonly DiffTool KDiff3 = new DiffTool("KDiff3", new DiffToolConfig
        {
            WindowsPath = @"KDiff3\kdiff3.exe",
            MacPath = "kdiff3.app/Contents/MacOS/kdiff3"
        }, KDiffArgs);
        
        [UsedImplicitly]
        public readonly DiffTool BeyondCompare3 = new DiffTool("Beyond Compare 3", new DiffToolConfig
        {
            WindowsPath = @"Beyond Compare 3\BCompare.exe",
            LinuxPath = @"bcompare"
        }, BeyondCompareArgs);
        
        [UsedImplicitly]
        public readonly DiffTool BeyondCompare4 = new DiffTool("Beyond Compare 4", new DiffToolConfig
        { 
            WindowsPath = @"Beyond Compare 4\BCompare.exe",
            MacPath = @"Beyond Compare.app/Contents/MacOS/bcomp",
            LinuxPath = @"bcompare"
        }, BeyondCompareArgs);
        
        [UsedImplicitly]
        public readonly DiffTool CodeCompare = new DiffTool("Code Compare", new DiffToolConfig
        {
            WindowsPath = @"Devart\Code Compare\CodeMerge.exe"
        }, CodeCompareArgs);
        
        [UsedImplicitly]
        public readonly DiffTool P4Merge = new DiffTool("P4Merge", new DiffToolConfig
        {
            WindowsPath = @"Perforce\p4merge.exe"
        }, P4MergeArgs);
        
        [UsedImplicitly]
        public readonly DiffTool TortoiseGitMerge = new DiffTool("Tortoise Git Merge", new DiffToolConfig
        {
            WindowsPath = @"TortoiseGit\bin\TortoiseGitMerge.exe"
        }, TortoiseGitMergeArgs);
        
        [UsedImplicitly]
        public readonly DiffTool WinMerge = new DiffTool("WinMerge", new DiffToolConfig
        {
            WindowsPath = @"WinMerge\WinMergeU.exe"
        },  WinMergeArgs);
        
        [UsedImplicitly]
        public readonly DiffTool CurrentVisualStudio = new CurrentlyRunningVisualStudioDiffTool();

        public static KnownDiffTools Instance { get; } = new KnownDiffTools();

        static string BeyondCompareArgs(string received, string approved, bool approvedExists)
        {
            return approvedExists
                ? $"\"{received}\" \"{approved}\" " + (ShouldlyEnvironmentContext.IsWindows() ? "/" :"-") + $"mergeoutput=\"{approved}\""
                : $"\"{received}\" " + (ShouldlyEnvironmentContext.IsWindows() ? "/" :"-") + $"mergeoutput=\"{approved}\"";
        }

        private static string KDiffArgs(string received, string approved, bool approvedExists)
        {
            return approvedExists
                ? $"\"{received}\" \"{approved}\" -o \"{approved}\" --cs CreateBakFiles=0"
                : $"\"{received}\" -o \"{approved}\" --cs CreateBakFiles=0";
        }

        private static string CodeCompareArgs(string received, string approved, bool approvedExists)
        {
            return $"/BF=\"{approved}\" /TF=\"{approved}\" /MF=\"{received}\" /RF=\"{approved}\"";
        }

        private static string P4MergeArgs(string received, string approved, bool approvedExists)
        {
            if (!approvedExists)
                File.AppendAllText(approved, string.Empty);

            return $"\"{approved}\" \"{approved}\" \"{received}\" \"{approved}\"";
        }

        private static string TortoiseGitMergeArgs(string received, string approved, bool approvedExists)
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
