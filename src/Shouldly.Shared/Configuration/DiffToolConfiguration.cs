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

        public static class KnownDiffTools
        {
            public static readonly DiffTool KDiff3 = new DiffTool("KDiff3", @"KDiff3\kdiff3.exe", KDiffArgs);
            public static readonly DiffTool BeyondCompare3 = new DiffTool("Beyond Compare 3", @"Beyond Compare 3\BCompare.exe", BeyondCompareArgs);
            public static readonly DiffTool BeyondCompare4 = new DiffTool("Beyond Compare 4", @"Beyond Compare 4\BCompare.exe", BeyondCompareArgs);

            static string BeyondCompareArgs(string received, string approved, bool approvedExists)
            {
                return approvedExists
                    ? $"\"{received}\" \"{approved}\" /mergeoutput=\"{approved}\""
                    : $"\"{received}\" /mergeoutput=\"{approved}\"";
            }

            static string KDiffArgs(string received, string approved, bool approvedExists)
            {
                return approvedExists
                    ? $"\"{received}\" \"{approved}\" -o \"{approved}\""
                    : $"\"{received}\" -o \"{approved}\"";
            }
        }

        public static class KnownDoNotLaunchStrategies
        {
            public static readonly IShouldNotLaunchDiffTool NCrunch = new DoNotLaunchEnvVariable("NCRUNCH");
            public static readonly IShouldNotLaunchDiffTool TeamCity = new DoNotLaunchEnvVariable("TeamCity");
            public static readonly IShouldNotLaunchDiffTool AppVeyor = new DoNotLaunchEnvVariable("AppVeyor");
        }

        public DiffToolConfiguration()
        {
            _diffTools = new List<DiffTool>
            {
                KnownDiffTools.KDiff3,
                KnownDiffTools.BeyondCompare3,
                KnownDiffTools.BeyondCompare4
            };
            _knownShouldNotLaunchDiffToolReasons = new List<IShouldNotLaunchDiffTool>
            {
                KnownDoNotLaunchStrategies.AppVeyor,
                KnownDoNotLaunchStrategies.NCrunch,
                KnownDoNotLaunchStrategies.TeamCity
            };
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

    public class DoNotLaunchEnvVariable : IShouldNotLaunchDiffTool
    {
        readonly string _environmentalVariable;

        public DoNotLaunchEnvVariable(string environmentalVariable)
        {
            _environmentalVariable = environmentalVariable;
        }

        public bool ShouldNotLaunch()
        {
            return (
                Environment.GetEnvironmentVariable(_environmentalVariable) ??
                Environment.GetEnvironmentVariable(_environmentalVariable, EnvironmentVariableTarget.User) ??
                Environment.GetEnvironmentVariable(_environmentalVariable, EnvironmentVariableTarget.Machine)
                ) != null;
        }
    }

    public interface IShouldNotLaunchDiffTool
    {
        bool ShouldNotLaunch();
    }
}
#endif