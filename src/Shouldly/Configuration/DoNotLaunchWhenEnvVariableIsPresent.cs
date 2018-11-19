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
            bool res = (
                Environment.GetEnvironmentVariable(_environmentalVariable) ??
                Environment.GetEnvironmentVariable(_environmentalVariable, EnvironmentVariableTarget.User) ??
                Environment.GetEnvironmentVariable(_environmentalVariable, EnvironmentVariableTarget.Machine)
                ) != null;
            
            Trace.WriteLine("Joe - environment var is present:" + res.ToString());

            return res;
        }
    }
}
#endif