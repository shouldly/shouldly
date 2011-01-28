using System;
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
                foundCurrent |= RuntimeFramework.CurrentFramework.Matches(framework);
            }
            Assert.That(foundCurrent, "CurrentFramework not listed");
        }

        [TestCaseSource("frameworkData")]
        public void CanCreateNewRuntimeFramework(FrameworkData data)
        {
            RuntimeFramework framework = new RuntimeFramework(data.runtime, data.frameworkVersion);
            Assert.AreEqual(data.runtime, framework.Runtime);
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

        [TestCaseSource("matchData")]
        public bool CanMatchRuntimes(RuntimeFramework f1, RuntimeFramework f2)
        {
            return f1.Matches(f2);
        }

        static TestCaseData[] matchData = new TestCaseData[] {
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0)), 
                new RuntimeFramework(RuntimeType.Net, new Version(2,0))) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0)), 
                new RuntimeFramework(RuntimeType.Net, new Version(2,0,50727))) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0,50727)), 
                new RuntimeFramework(RuntimeType.Net, new Version(2,0))) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0,50727)), 
                new RuntimeFramework(RuntimeType.Net, new Version(2,0))) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(3,5)), 
                new RuntimeFramework(RuntimeType.Net, new Version(2,0))) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0)), 
                new RuntimeFramework(RuntimeType.Mono, new Version(2,0))) 
                .Returns(false),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0)), 
                new RuntimeFramework(RuntimeType.Net, new Version(1,1))) 
                .Returns(false),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0,50727)), 
                new RuntimeFramework(RuntimeType.Net, new Version(2,0,40607))) 
                .Returns(false),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Mono, new Version(1,1)), // non-existent version but it works
                new RuntimeFramework(RuntimeType.Mono, new Version(1,0))) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Mono, new Version(2,0)),
                new RuntimeFramework(RuntimeType.Any, new Version(2,0)))
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Any, new Version(2,0)),
                new RuntimeFramework(RuntimeType.Mono, new Version(2,0)))
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Any, new Version(2,0)),
                new RuntimeFramework(RuntimeType.Any, new Version(2,0)))
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Any, new Version(2,0)),
                new RuntimeFramework(RuntimeType.Any, new Version(4,0)))
                .Returns(false),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, RuntimeFramework.DefaultVersion), 
                new RuntimeFramework(RuntimeType.Net, new Version(2,0))) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0)),
                new RuntimeFramework(RuntimeType.Net, RuntimeFramework.DefaultVersion)) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Any, RuntimeFramework.DefaultVersion), 
                new RuntimeFramework(RuntimeType.Net, new Version(2,0))) 
                .Returns(true),
            new TestCaseData(
                new RuntimeFramework(RuntimeType.Net, new Version(2,0)),
                new RuntimeFramework(RuntimeType.Any, RuntimeFramework.DefaultVersion)) 
                .Returns(true)
            };

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
                return string.Format("<{0}-{1}>", this.runtime, this.frameworkVersion);
            }
        }

        private FrameworkData[] frameworkData = new FrameworkData[] {
            new FrameworkData(RuntimeType.Net, new Version(1,0), new Version(1,0), "net-1.0", "Net 1.0"),
            new FrameworkData(RuntimeType.Net, new Version(1,0,3705), new Version(1,0,3705), "net-1.0.3705", "Net 1.0.3705"),
            new FrameworkData(RuntimeType.Net, new Version(1,1), new Version(1,1), "net-1.1", "Net 1.1"),
            new FrameworkData(RuntimeType.Net, new Version(1,1,4322), new Version(1,1,4322), "net-1.1.4322", "Net 1.1.4322"),
            new FrameworkData(RuntimeType.Net, new Version(2,0), new Version(2,0), "net-2.0", "Net 2.0"),
            new FrameworkData(RuntimeType.Net, new Version(2,0,40607), new Version(2,0,40607), "net-2.0.40607", "Net 2.0.40607"),
            new FrameworkData(RuntimeType.Net, new Version(2,0,50727), new Version(2,0,50727), "net-2.0.50727", "Net 2.0.50727"),
            new FrameworkData(RuntimeType.Net, new Version(3,0), new Version(2,0,50727), "net-3.0", "Net 3.0"),
            new FrameworkData(RuntimeType.Net, new Version(3,5), new Version(2,0,50727), "net-3.5", "Net 3.5"),
            new FrameworkData(RuntimeType.Net, new Version(4,0), new Version(4,0), "net-4.0", "Net 4.0"),
            new FrameworkData(RuntimeType.Net, RuntimeFramework.DefaultVersion, RuntimeFramework.DefaultVersion, "net", "Net"),
            new FrameworkData(RuntimeType.Mono, new Version(1,0), new Version(1,1,4322), "mono-1.0", "Mono 1.0"),
            new FrameworkData(RuntimeType.Mono, new Version(2,0), new Version(2,0), "mono-2.0", "Mono 2.0"),
            new FrameworkData(RuntimeType.Mono, new Version(2,0,50727), new Version(2,0,50727), "mono-2.0.50727", "Mono 2.0.50727"),
            new FrameworkData(RuntimeType.Mono, new Version(3,5), new Version(2,0,50727), "mono-3.5", "Mono 3.5"),
            new FrameworkData(RuntimeType.Mono, RuntimeFramework.DefaultVersion, RuntimeFramework.DefaultVersion, "mono", "Mono"),
            new FrameworkData(RuntimeType.Any, new Version(1,1), new Version(1,1), "v1.1", "v1.1"),
            new FrameworkData(RuntimeType.Any, new Version(2,0), new Version(2,0), "v2.0", "v2.0"),
            new FrameworkData(RuntimeType.Any, new Version(2,0,50727), new Version(2,0,50727), "v2.0.50727", "v2.0.50727"),
            new FrameworkData(RuntimeType.Any, new Version(3,5), new Version(2,0,50727), "v3.5", "v3.5"),
            new FrameworkData(RuntimeType.Any, new Version(4,0), new Version(4,0), "v4.0", "v4.0"),
            new FrameworkData(RuntimeType.Any, RuntimeFramework.DefaultVersion, RuntimeFramework.DefaultVersion, "any", "Any")
        };
    }
}
