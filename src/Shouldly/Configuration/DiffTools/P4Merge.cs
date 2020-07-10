namespace Shouldly.Configuration.DiffTools
{
    internal sealed class P4Merge : DiffTool
    {
        public P4Merge() : base("P4Merge", new DiffToolConfig
        {
            WindowsPath = @"Perforce\p4merge.exe"
        }, P4MergeArgs) { }

        private static string P4MergeArgs(string received, string approved)
        {
            CreateEmptyFileIfNotExists(approved);

            return $"\"{approved}\" \"{received}\"";
        }
    }
}
