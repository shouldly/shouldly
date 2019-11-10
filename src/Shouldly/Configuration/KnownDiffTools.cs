using JetBrains.Annotations;
using Shouldly.Configuration.DiffTools;

namespace Shouldly.Configuration
{
    public class KnownDiffTools
    {
        [UsedImplicitly]
        public readonly DiffTool KDiff3 = new KDiff3();

        [UsedImplicitly]
        public readonly DiffTool BeyondCompare3 = new BeyondCompare3();

        [UsedImplicitly]
        public readonly DiffTool BeyondCompare4 = new BeyondCompare4();

        [UsedImplicitly]
        public readonly DiffTool CodeCompare = new CodeCompare();

        [UsedImplicitly]
        public readonly DiffTool P4Merge = new P4Merge();

        [UsedImplicitly]
        public readonly DiffTool TortoiseGitMerge = new TortoiseGitMerge();

        [UsedImplicitly]
        public readonly DiffTool WinMerge = new WinMerge();

        [UsedImplicitly]
        public readonly DiffTool VisualStudioCode = new VisualStudioCode();

        [UsedImplicitly]
        public readonly DiffTool VimDiff = new VimDiff();

        [UsedImplicitly]
        public readonly DiffTool CurrentVisualStudio = new CurrentlyRunningVisualStudio();

        public static KnownDiffTools Instance { get; } = new KnownDiffTools();
    }
}