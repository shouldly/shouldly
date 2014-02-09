// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using NUnit.Core;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
    [TestFixture]
    public class TestRunnerFactoryTests
    {
        private RuntimeFramework currentFramework = RuntimeFramework.CurrentFramework;
        private string testDll = "/test.dll";
        private DefaultTestRunnerFactory factory;
        private TestPackage package;

        [SetUp]
        public void Init()
        {
            factory = new DefaultTestRunnerFactory();
            package = new TestPackage(testDll);
        }

        [Test]
        public void SameFrameworkUsesTestDomain()
        {
            package.Settings["RuntimeFramework"] = currentFramework;
            Assert.That( factory.MakeTestRunner(package), Is.TypeOf(typeof(TestDomain)));
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public void DifferentRuntimeUsesProcessRunner()
        {
            RuntimeType runtime = currentFramework.Runtime == RuntimeType.Net
                ? RuntimeType.Mono : RuntimeType.Net;
            package.Settings["RuntimeFramework"] = new RuntimeFramework(runtime, currentFramework.ClrVersion);
            Assert.That(factory.MakeTestRunner(package), Is.TypeOf(typeof(ProcessRunner)));
        }

        [Test]
        public void DifferentVersionUsesProcessRunner()
        {
            int major = currentFramework.ClrVersion.Major == 2 ? 4 : 2;
            package.Settings["RuntimeFramework"] = new RuntimeFramework(currentFramework.Runtime, new Version(major,0));
            Assert.That(factory.MakeTestRunner(package), Is.TypeOf(typeof(ProcessRunner)));
        }
#endif
    }
}
