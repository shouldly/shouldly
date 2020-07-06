using System;
using System.Linq;

namespace Shouldly.Configuration
{
    public class DoNotLaunchWhenTypeIsLoaded : IShouldNotLaunchDiffTool
    {
        readonly string _typeName;

        public DoNotLaunchWhenTypeIsLoaded(string typeName)
        {
            _typeName = typeName;
        }

        public bool ShouldNotLaunch()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Any(a => a.GetName().Name == _typeName);
        }
    }
}