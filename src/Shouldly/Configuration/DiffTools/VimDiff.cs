namespace Shouldly.Configuration.DiffTools
{
    internal sealed class VimDiff : DiffTool
    {
        public VimDiff() : base("VimDiff", new DiffToolConfig
        {
            WindowsPath = @"vim.bat",
        }, VimDiffArgs) { }

        private static string VimDiffArgs(string received, string approved)
        {
            return $"-d \"{approved}\" \"{received}\"";
        }
    }
}
