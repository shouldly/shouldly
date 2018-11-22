using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Shouldly.Tests.ConventionTests
{
    public sealed class IgnoreOnAppVeyorLinuxFact : FactAttribute
    {
        public IgnoreOnAppVeyorLinuxFact() {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && IsAppVeyor()) {
                Skip = "Ignored on Linux";
            }
        }
        
        private bool IsAppVeyor()
            => Environment.GetEnvironmentVariable("APPVEYOR") != null;
    }
}