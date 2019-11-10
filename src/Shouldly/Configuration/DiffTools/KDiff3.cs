#if ShouldMatchApproved
namespace Shouldly.Configuration.DiffTools
{
    internal sealed class KDiff3 : DiffTool
    {
        public KDiff3() : base("KDiff3", new DiffToolConfig
        {
            WindowsPath = @"KDiff3\kdiff3.exe",
            MacPath = "kdiff3.app/Contents/MacOS/kdiff3"
        }, KDiffArgs) { }

        private static string KDiffArgs(string received, string approved)
        {
            CreateEmptyFileIfNotExists(approved);

            return $"\"{approved}\" \"{received}\" -o \"{approved}\" --cs CreateBakFiles=0";
        }
    }
}
#endif