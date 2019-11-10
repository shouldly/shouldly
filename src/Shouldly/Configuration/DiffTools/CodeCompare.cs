#if ShouldMatchApproved
namespace Shouldly.Configuration.DiffTools
{
    internal sealed class CodeCompare : DiffTool
    {
        public CodeCompare() : base("Code Compare", new DiffToolConfig
        {
            WindowsPath = @"Devart\Code Compare\CodeMerge.exe"
        }, CodeCompareArgs) { }

        private static string CodeCompareArgs(string received, string approved, bool approvedExists)
        {
            return $"/BF=\"{approved}\" /TF=\"{approved}\" /MF=\"{received}\" /RF=\"{approved}\"";
        }
    }
}
#endif