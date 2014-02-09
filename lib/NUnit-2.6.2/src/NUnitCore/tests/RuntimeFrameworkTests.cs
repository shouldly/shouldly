// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Diagnostics;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class RuntimeFrameworkTests
    {
        static RuntimeType currentRuntime = 
            Type.GetType("Mono.Runtime", false) != null ? RuntimeType.Mono : RuntimeType.Net;

        [Test]
        public void CanGetCurrentFramework()
        {
            RuntimeFramework framework = RuntimeFramework.CurrentFramework;

            Console.WriteLine("Running under {0}", framework.DisplayName);
			Assert.That(framework.Runtime, Is.EqualTo(currentRuntime));
            Assert.That(framework.ClrVersion, Is.EqualTo(Environment.Version));
        }

        [Test]
        public void CurrentFrameworkHasBuildSpecified()
        {
            Assert.That(RuntimeFramework.CurrentFramework.ClrVersion.Build, Is.GreaterThan(0));
        }

        [Test]
        public void CurrentFrameworkMustBeAvailable()
        {
            Assert.That(RuntimeFramework.CurrentFramework.IsAvailable);
        }

        [Test]
        public void CanListAvailableFrameworks()
        {
            RuntimeFramework[] available = RuntimeFramework.AvailableFrameworks;
            Assert.That(available, Has.Length.GreaterThan(0) );
            bool foundCurrent = false;
            foreach (RuntimeFramework framework in available)
            {
                Console.WriteLine("Available: {0}", framework.DisplayName);
                foundCurrent |= RuntimeFramework.CurrentFramework.Supports(framework);
            }
            Assert.That(foundCurrent, "CurrentFramework not listed");
        }

        [TestCaseSource("frameworkData")]
        public void CanCreateUsingFrameworkVersion(FrameworkData data)
        {
            RuntimeFramework framework = new RuntimeFramework(data.runtime, data.frameworkVersion);
            Assert.AreEqual(data.runtime, framework.Runtime);
            Assert.AreEqual(data.frameworkVersion, framework.FrameworkVersion);
            Assert.AreEqual(data.clrVersion, framework.ClrVersion);
        }

        [TestCaseSource("frameworkData")]
        public void CanCreateUsingClrVersion(FrameworkData data)
        {
            // Can't create using CLR version if we expect version 3.0, 3.5 or 4.5
            Assume.That(data.frameworkVersion.Major != 3);
            if (data.frameworkVersion.Major == 4)
                Assume.That(data.frameworkVersion.Minor != 5);

            RuntimeFramework framework = new RuntimeFramework(data.runtime, data.clrVersion);
            Assert.AreEqual(data.runtime, framework.Runtime);
            Assert.AreEqual(data.frameworkVersion, framework.FrameworkVersion);
            Assert.AreEqual(data.clrVersion, framework.ClrVersion);
        }

        [TestCaseSource("frameworkData")]
        public void CanParseRuntimeFramework(FrameworkData data)
        {
            RuntimeFramework framework = RuntimeFramework.Parse(data.representation);
            Assert.AreEqual(data.runtime, framework.Runtime);
            Assert.AreEqual(data.clrVersion, framework.ClrVersion);
        }

        [TestCaseSource("frameworkData")]
        public void CanDisplayFrameworkAsString(FrameworkData data)
        {
            RuntimeFramework framework = new RuntimeFramework(data.runtime, data.frameworkVersion);
            Assert.AreEqual(data.representation, framework.ToString());
            Assert.AreEqual(data.displayName, framework.DisplayName);
        }

        [TestCaseSource("AllFrameworks")]
        public void AnyFrameworkSupportsItself(RuntimeFramework framework)
        {
            Assert.That(framework.Supports(framework));
        }

        [Test]
        public void Mono11IsSynonymousWithMono10()
        {
            RuntimeFramework mono10 = new RuntimeFramework(RuntimeType.Mono, new Version(1, 0));
            RuntimeFramework mono11 = new RuntimeFramework(RuntimeType.Mono, new Version(1, 1));

            Assert.That(mono11.ToString(), Is.EqualTo("mono-1.0"));
            Assert.That(mono11.DisplayName, Is.EqualTo("Mono 1.0"));
            Assert.That(mono11.Runtime, Is.EqualTo(RuntimeType.Mono));
            Assert.That(mono11.FrameworkVersion, Is.EqualTo(new Version(1, 0)));

            Assert.That(mono11.Supports(mono10));
            Assert.That(mono10.Supports(mono11));
        }

        [TestCase("net-1.0.3705")]
        [TestCase("net-1.1.4322")]
        [TestCase("net-2.0.50727")]
        [TestCase("net-4.0.30319")]
        public void WellKnownClrVersions_SupportEquivalentFrameworkVersions(string s)
        {
            RuntimeFramework f1 = RuntimeFramework.Parse(s);
            RuntimeFramework f2 = new RuntimeFramework(f1.Runtime, f1.FrameworkVersion);

            Assert.That(f1.Runtime, Is.EqualTo(f2.Runtime));
            Assert.That(f1.FrameworkVersion, Is.EqualTo(f2.FrameworkVersion));
            Assert.That(f1.ClrVersion, Is.EqualTo(f2.ClrVersion));

            Assert.That(f1.Supports(f2));
            Assert.That(f2.Supports(f1));
        }

        [TestCaseSource("LowHigh")]
        public void SameRuntime_HigherVersion_NotSupported(RuntimeFramework f1, RuntimeFramework f2)
        {
            Assert.False(f1.Supports(f2));
        }

        [TestCaseSource("HighLow_SameCLR")]
        public void SameRuntime_LowerVersion_SameCLR_Supported(RuntimeFramework f1, RuntimeFramework f2)
        {
            Assert.That(f1.Supports(f2));
        }

        [TestCaseSource("HighLow_DifferentCLR")]
        public void SameRuntime_LowerVersion_DifferentCLR_NotSupported(RuntimeFramework f1, RuntimeFramework f2)
        {
            Assert.False(f1.Supports(f2));
        }

        [TestCaseSource("MonoFrameworks")]
        public void DifferentRuntimes_NotSupported(RuntimeFramework f1)
        {
            RuntimeFramework f2 = new RuntimeFramework(RuntimeType.Net, f1.FrameworkVersion);

            Assert.False(f1.Supports(f2));
            Assert.False(f2.Supports(f1));
        }

        [Test]
        public void SameRuntimes_DifferentBuilds_NotSupported()
        {
            RuntimeFramework f1 = new RuntimeFramework(RuntimeType.Net, new Version(2, 0, 50727));
            RuntimeFramework f2 = new RuntimeFramework(RuntimeType.Net, new Version(2, 0, 40607));

            Assert.False(f1.Supports(f2));
            Assert.False(f2.Supports(f1));
        }

        [TestCaseSource("MicrosoftFrameworks")]
        [TestCaseSource("MonoFrameworks")]
        public void UnspecifiedRuntime_SameVersion_Supported(RuntimeFramework f1)
        {
            // NOTE: Mono 1.0 has a 1.1 ClrVersion, so this doesn't work.
            // Since we're phasing out support for 1.x versions, we
            // aren't planning to fix this.
            Assume.That(f1.ToString() != "mono-1.0");

            RuntimeFramework f2 = new RuntimeFramework(RuntimeType.Any, f1.FrameworkVersion);
            Assert.That(f1.Supports(f2));
            Assert.That(f2.Supports(f1));
        }

        [TestCaseSource("MicrosoftFrameworks")]
        [TestCaseSource("MonoFrameworks")]
        public void UnspecifiedVersion_SameRuntime_Supported(RuntimeFramework f1)
        {
            RuntimeFramework f2 = new RuntimeFramework(f1.Runtime, RuntimeFramework.DefaultVersion);
            Assert.That(f1.Supports(f2));
            Assert.That(f2.Supports(f1));
        }

        [TestCaseSource("MicrosoftFrameworks")]
        [TestCaseSource("MonoFrameworks")]
        public void UnspecifiedRuntimeAndVersion_Supported(RuntimeFramework f1)
        {
            RuntimeFramework f2 = new RuntimeFramework(RuntimeType.Any, RuntimeFramework.DefaultVersion);
            Assert.That(f1.Supports(f2));
            Assert.That(f2.Supports(f1));
        }

        #region TestCaseSources

        static TestCaseData[] LowHigh
        {
            get { return GetFrameworkPairs(false); }
        }

        static TestCaseData[] HighLow_SameCLR
        {
            get { return GetFrameworkPairs(true, true, false); }
        }

        static TestCaseData[] HighLow_DifferentCLR
        {
            get { return GetFrameworkPairs(true, false, true); }
        }

        static RuntimeFramework[] AllFrameworks
        {
            get
            {
                RuntimeFramework[] frameworks =
                    new RuntimeFramework[MicrosoftFrameworks.Length + MonoFrameworks.Length + UnspecifiedFrameworks.Length];

                int index = 0;
                foreach (RuntimeFramework[] array in _frameworkArrays)
                    foreach (RuntimeFramework framework in array)
                        frameworks[index++] = framework;

                return frameworks;
            }
        }

        static readonly RuntimeFramework[] MicrosoftFrameworks = new RuntimeFramework[] {
            new RuntimeFramework(RuntimeType.Net, new Version(1,0)),
            new RuntimeFramework(RuntimeType.Net, new Version(1,1)),
            new RuntimeFramework(RuntimeType.Net, new Version(2,0)),
            new RuntimeFramework(RuntimeType.Net, new Version(3,0)),
            new RuntimeFramework(RuntimeType.Net, new Version(3,5)),
            new RuntimeFramework(RuntimeType.Net, new Version(4,0)),
            new RuntimeFramework(RuntimeType.Net, new Version(4,5))
        };

        static readonly RuntimeFramework[] MonoFrameworks = new RuntimeFramework[] {
            new RuntimeFramework(RuntimeType.Mono, new Version(1,0)),
            new RuntimeFramework(RuntimeType.Mono, new Version(2,0)),
            new RuntimeFramework(RuntimeType.Mono, new Version(3,0)),
            new RuntimeFramework(RuntimeType.Mono, new Version(3,5)),
            new RuntimeFramework(RuntimeType.Mono, new Version(4,0)),
            new RuntimeFramework(RuntimeType.Mono, new Version(4,5))
        };

        static readonly RuntimeFramework[] UnspecifiedFrameworks = new RuntimeFramework[] {
            new RuntimeFramework(RuntimeType.Any, new Version(1,0)),
            new RuntimeFramework(RuntimeType.Any, new Version(1,1)),
            new RuntimeFramework(RuntimeType.Any, new Version(2,0)),
            new RuntimeFramework(RuntimeType.Any, new Version(3,0)),
            new RuntimeFramework(RuntimeType.Any, new Version(3,5)),
            new RuntimeFramework(RuntimeType.Any, new Version(4,0)),
            new RuntimeFramework(RuntimeType.Any, new Version(4,5))
        };

        static readonly RuntimeFramework[][] _frameworkArrays =
            new RuntimeFramework[][] { MicrosoftFrameworks, MonoFrameworks, UnspecifiedFrameworks };

        public struct FrameworkData
        {
            public RuntimeType runtime;
            public Version frameworkVersion;
            public Version clrVersion;
            public string representation;
            public string displayName;

            public FrameworkData(RuntimeType runtime, Version frameworkVersion, Version clrVersion,
                string representation, string displayName)
            {
                this.runtime = runtime;
                this.frameworkVersion = frameworkVersion;
                this.clrVersion = clrVersion;
                this.representation = representation;
                this.displayName = displayName;
            }

            public override string ToString()
            {
                return string.Format("<{0},{1},{2}>", this.runtime, this.frameworkVersion, this.clrVersion);
            }
        }

        internal FrameworkData[] frameworkData = new FrameworkData[] {
            new FrameworkData(RuntimeType.Net, new Version(1,0), new Version(1,0,3705), "net-1.0", "Net 1.0"),
            new FrameworkData(RuntimeType.Net, new Version(1,1), new Version(1,1,4322), "net-1.1", "Net 1.1"),
            new FrameworkData(RuntimeType.Net, new Version(2,0), new Version(2,0,50727), "net-2.0", "Net 2.0"),
            new FrameworkData(RuntimeType.Net, new Version(3,0), new Version(2,0,50727), "net-3.0", "Net 3.0"),
            new FrameworkData(RuntimeType.Net, new Version(3,5), new Version(2,0,50727), "net-3.5", "Net 3.5"),
            new FrameworkData(RuntimeType.Net, new Version(4,0), new Version(4,0,30319), "net-4.0", "Net 4.0"),
            new FrameworkData(RuntimeType.Net, new Version(4,5), new Version(4,0,30319), "net-4.5", "Net 4.5"),
            new FrameworkData(RuntimeType.Net, RuntimeFramework.DefaultVersion, RuntimeFramework.DefaultVersion, "net", "Net"),
            new FrameworkData(RuntimeType.Mono, new Version(1,0), new Version(1,1,4322), "mono-1.0", "Mono 1.0"),
            new FrameworkData(RuntimeType.Mono, new Version(2,0), new Version(2,0,50727), "mono-2.0", "Mono 2.0"),
            new FrameworkData(RuntimeType.Mono, new Version(3,5), new Version(2,0,50727), "mono-3.5", "Mono 3.5"),
            new FrameworkData(RuntimeType.Mono, new Version(4,0), new Version(4,0,30319), "mono-4.0", "Mono 4.0"),
            new FrameworkData(RuntimeType.Mono, RuntimeFramework.DefaultVersion, RuntimeFramework.DefaultVersion, "mono", "Mono"),
            new FrameworkData(RuntimeType.Any, new Version(1,1), new Version(1,1,4322), "v1.1", "v1.1"),
            new FrameworkData(RuntimeType.Any, new Version(2,0), new Version(2,0,50727), "v2.0", "v2.0"),
            new FrameworkData(RuntimeType.Any, new Version(3,5), new Version(2,0,50727), "v3.5", "v3.5"),
            new FrameworkData(RuntimeType.Any, new Version(4,0), new Version(4,0,30319), "v4.0", "v4.0"),
            new FrameworkData(RuntimeType.Any, RuntimeFramework.DefaultVersion, RuntimeFramework.DefaultVersion, "any", "Any")
        };
        
        #endregion

        #region Helper Methods

        static TestCaseData[] GetFrameworkPairs(bool highToLow)
        {
            return GetFrameworkPairs(highToLow, true, true);
        }

        static TestCaseData[] GetFrameworkPairs(bool highToLow, bool sameCLR, bool differentCLR)
        {
            int fullCount = PairCount(MicrosoftFrameworks) + PairCount(MonoFrameworks) + PairCount(UnspecifiedFrameworks);
            int count = 0;
            if (sameCLR && differentCLR)
                count = fullCount;
            else if (sameCLR)
                count = 12;
            else if (differentCLR)
                count = fullCount - 12;

            TestCaseData[] cases = new TestCaseData[count];

            int index = 0;
            foreach (RuntimeFramework[] farray in _frameworkArrays)
                for (int i = 0; i < farray.Length - 1; i++)
                    for (int j = i + 1; j < farray.Length; j++)
                    {
                        RuntimeFramework f1 = farray[i];
                        RuntimeFramework f2 = farray[j];

                        if (sameCLR && f1.ClrVersion == f2.ClrVersion || differentCLR && f1.ClrVersion != f2.ClrVersion)
                            cases[index++] = highToLow
                                ? new TestCaseData(f2, f1)
                                : new TestCaseData(f1, f2);
                    }

            return cases;
        }

        static int PairCount(RuntimeFramework[] array)
        {
            return array.Length * (array.Length - 1) / 2;
        }

        private RuntimeFramework Framework(string representation)
        {
            return RuntimeFramework.Parse(representation);
        }

        #endregion
    }
}
