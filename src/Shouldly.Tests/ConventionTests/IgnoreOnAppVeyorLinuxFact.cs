using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Shouldly.Tests.ConventionTests
{
    public sealed class IgnoreOnAppVeyorLinuxFact : FactAttribute
    {
        public IgnoreOnAppVeyorLinuxFact()
        {
            if (IsAppVeyor())
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Skip = "Ignored on Linux";
                }
            }
        }

        private static bool IsAppVeyor()
            => Environment.GetEnvironmentVariable("APPVEYOR") != null;
    }
}