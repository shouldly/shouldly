#if ShouldMatchApproved
namespace Shouldly.Configuration.DiffTools
{
    internal sealed class TortoiseGitMerge : DiffTool
    {
        public TortoiseGitMerge() : base("Tortoise Git Merge", new DiffToolConfig
        {
            WindowsPath = @"TortoiseGit\bin\TortoiseGitMerge.exe"
        }, TortoiseGitMergeArgs) { }

        private static string TortoiseGitMergeArgs(string received, string approved, bool approvedExists)
        {
            CreateEmptyFileIfNotExists(approved);

            return $"/mine:\"{approved}\" /base:\"{received}\"";
        }
    }
}
#endif