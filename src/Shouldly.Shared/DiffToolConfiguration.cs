#if !PORTABLE
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shouldly
{
    public class DiffToolConfiguration
    {
        readonly List<IShouldNotLaunchDiffTool> _knownShouldNotLaunchDiffToolReasons;
        readonly List<DiffTool> _diffToolPriority = new List<DiffTool>();
        readonly List<DiffTool> _diffTools;

        public static class KnownDiffTools
        {
            public static readonly DiffTool KDiff3 = new DiffTool("KDiff3", @"C:\Program Files\KDiff3\kdiff3.exe", (received, approved) => $"\"{received}\" \"{approved}\" -o \"{approved}\"");
        }

        public static class KnownDoNoLaunchStrategies
        {
            public static readonly IShouldNotLaunchDiffTool NCrunch = new DoNotLaunchEnvVariable("NCRUNCH");
            public static readonly IShouldNotLaunchDiffTool TeamCity = new DoNotLaunchEnvVariable("TeamCity");
            public static readonly IShouldNotLaunchDiffTool AppVeyor = new DoNotLaunchEnvVariable("AppVeyor");
        }

        public DiffToolConfiguration()
        {
            _diffTools = new List<DiffTool>
            {
                KnownDiffTools.KDiff3
            };
            _knownShouldNotLaunchDiffToolReasons = new List<IShouldNotLaunchDiffTool>
            {
                KnownDoNoLaunchStrategies.AppVeyor,
                KnownDoNoLaunchStrategies.NCrunch,
                KnownDoNoLaunchStrategies.TeamCity
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
            // TODO Fallback?
            return _diffToolPriority.FirstOrDefault() ?? _diffTools.FirstOrDefault();
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