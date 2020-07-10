namespace Shouldly.Configuration.DiffTools
{
    internal sealed class WinMerge : DiffTool
    {
        public WinMerge() : base("WinMerge", new DiffToolConfig
        {
            WindowsPath = @"WinMerge\WinMergeU.exe"
        }, WinMergeArgs) { }

        private static string WinMergeArgs(string received, string approved)
        {
            CreateEmptyFileIfNotExists(approved);

            return $"/u /wr \"{approved}\" \"{received}\" /o \"{approved}\"";
        }
    }
}
