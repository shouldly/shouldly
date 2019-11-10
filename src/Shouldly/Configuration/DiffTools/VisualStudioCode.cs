#if ShouldMatchApproved
namespace Shouldly.Configuration.DiffTools
{
    internal sealed class VisualStudioCode : DiffTool
    {
        public VisualStudioCode() : base("Visual Studio Code", new DiffToolConfig
        {
            WindowsPath = @"%ProgramFiles%\Microsoft VS Code\code",
            MacPath = "Visual Studio Code.app/Contents/MacOS/Electron"
        }, VsCodeDiffArgs) { }

        private static string VsCodeDiffArgs(string received, string approved, bool approvedExists)
        {
            CreateEmptyFileIfNotExists(approved);

            return $"--new-window --diff \"{approved}\" \"{received}\"";
        }
    }
}
#endif