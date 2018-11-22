#if ShouldMatchApproved
using System;
using System.Diagnostics;

namespace Shouldly.Configuration
{
    public class DoNotLaunchWhenEnvVariableIsPresent : IShouldNotLaunchDiffTool
    {
        readonly string _environmentalVariable;

        public DoNotLaunchWhenEnvVariableIsPresent(string environmentalVariable)
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
}
#endif