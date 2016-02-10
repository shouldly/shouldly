#if !PORTABLE
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shouldly.Configuration
{
    public class DiffToolConfiguration
    {
        readonly List<IShouldNotLaunchDiffTool> _knownShouldNotLaunchDiffToolReasons;
        readonly List<DiffTool> _diffToolPriority = new List<DiffTool>();
        readonly List<DiffTool> _diffTools;

        public KnownDiffTools KnownDiffTools { get; } = KnownDiffTools.Instance;
        public KnownDoNotLaunchStrategies KnownDoNotLaunchStrategies { get; } = new KnownDoNotLaunchStrategies();

        public DiffToolConfiguration()
        {
            _diffTools = typeof (KnownDiffTools).GetFields().Select(f => (DiffTool) f.GetValue(KnownDiffTools)).ToList();
            _knownShouldNotLaunchDiffToolReasons = typeof (KnownDoNotLaunchStrategies).GetFields()
                .Select(f => (IShouldNotLaunchDiffTool) f.GetValue(KnownDoNotLaunchStrategies)).ToList();
        }

        public void RegisterDiffTool(DiffTool diffTool)
        {
            _diffTools.Add(diffTool);
        }

        public void SetDiffToolPriorities(params DiffTool[] diffTools)
        {
            var notRegistered = diffTools.Except(_diffTools);
            if (notRegistered.Any())
            {
                var notRegisteredNames = string.Join(", ", notRegistered.Select(r => r.Name).ToArray());
                throw new InvalidOperationException($"The following diff tools are not registed: {notRegisteredNames}");
            }
            _diffToolPriority.Clear();
            _diffToolPriority.AddRange(diffTools);
        }

        public void AddDoNotLaunchStrategy(IShouldNotLaunchDiffTool shouldNotlaunchStrategy)
        {
            _knownShouldNotLaunchDiffToolReasons.Add(shouldNotlaunchStrategy);
        }

        public bool ShouldOpenDiffTool()
        {
            return !_knownShouldNotLaunchDiffToolReasons.Any(r => r.ShouldNotLaunch());
        }

        public DiffTool GetDiffTool()
        {
            var diffTool = _diffToolPriority.FirstOrDefault(d => d.Exists()) ??
                           _diffTools.FirstOrDefault(d => d.Exists());
            if (diffTool == null)
            {
                throw new ShouldAssertException(@"Cannot find a difftool to use, please open an issue or a PR to add support for your difftool.

In the meantime use 'ShouldlyConfiguration.DiffTools.RegisterDiffTool()' to add your own");
            }

            return diffTool;
        }
    }
}
#endif