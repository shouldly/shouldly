namespace Shouldly.Configuration.DiffTools
{
    internal sealed class CodeCompare : DiffTool
    {
        public CodeCompare() : base("Code Compare", new DiffToolConfig
        {
            WindowsPath = @"Devart\Code Compare\CodeMerge.exe"
        }, CodeCompareArgs) { }

        private static string CodeCompareArgs(string received, string approved)
        {
            return $"/TF=\"{received}\" /MF=\"{approved}\" /RF=\"{approved}\"";
        }
    }
}
