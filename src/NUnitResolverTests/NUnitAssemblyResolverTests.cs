using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace NUnitResolverTests
{
    [TestFixture]
    public class NUnitAssemblyResolverTests
    {
        [Test]
        [Ignore(
            @"This test is here as an example of what happens if a different version of NUnit is referenced
                  than the version Shouldly references.

                  We can't run this test because it will step on the toes of the other (more important) test above.

                  i.e. If the other test runs first, then the different version of NUnit is
                       loaded without error and this test fails.
                       But, if this test runs first then .NET fails to load the NUnit assembly and won't fire
                       the AppDomain.AssemblyResolve event that we depend on to load whatever version of NUnit is available."
            )]
        public void WhenNotWiredUp_And_WhenDifferentVersionOfNUnitIsReferenced_ShouldThrowFileLoadException()
        {
            //  act, assert
            Assert.Throws<FileLoadException>(() => string.Empty.ShouldNotContain("anything"));
        }

        /// <summary>
        /// This test has to be in its own assembly because we need to purposefully
        /// reference a different version of NUnit than what the Shouldly project
        /// references.
        /// 
        /// Calling NUnitAssemblyResolver.WireUp() hooks into the AppDomain.AssemblyResolve
        /// event and attempts to resolve any missing NUnit references with whatever version
        /// of the requested dll is present in the AppDomain.BaseDirectory.
        /// </summary>
        [Test]
        public void WhenWiredUp_And_WhenDifferentVersionOfNUnitIsReferenced_ShouldNotThrow()
        {
            //  arrange
            NUnitAssemblyResolver.WireUp();

            //  act, assert
            //  (any shouldy assert will do, this will trigger the nunit assembly referenced
            //   by shouldly to be resolved and this will throw an exception if the
            //   NUnitAssemblyResolver does not intercept the failure and load
            //   a different version of NUnit)
            string.Empty.ShouldNotContain("anything");
        }
    }
}