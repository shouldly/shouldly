// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Util;

namespace NUnit.Tests.TimingTests
{
    [TestFixture]
    public class LoadTimingTests
    {
        private TestRunner runner;
        private TestLoader loader;

        [TestFixtureSetUp]
        public void InstallServices()
        {
            if (Services.ProjectService == null)
                ServiceManager.Services.AddService(new ProjectService());
            if (Services.DomainManager == null)
                ServiceManager.Services.AddService(new DomainManager());
        }

        [TearDown]
        public void UnloadTests()
        {
            if (runner != null)
                runner.Unload();
            if (loader != null)
                loader.UnloadProject();
        }

        [Test]
        public void Load1000TestsInSameDomain()
        {
            runner = new SimpleTestRunner();
            int start = Environment.TickCount;
            Assert.IsTrue(runner.Load(new TestPackage("loadtest-assembly.dll")));
            ITest test = runner.Test;
            Assert.AreEqual(2050, test.TestCount);
            int ms = Environment.TickCount - start;
            Console.WriteLine("Loaded in {0}ms", ms);
            Assert.LessOrEqual(ms, 4000);
        }

        [Test]
        public void Load1000TestsInTestDomain()
        {
            runner = new TestDomain();
            int start = Environment.TickCount;
            Assert.IsTrue(runner.Load(new TestPackage("loadtest-assembly.dll")));
            ITest test = runner.Test;
            Assert.AreEqual(2050, test.TestCount);
            int ms = Environment.TickCount - start;
            Console.WriteLine("Loaded in {0}ms", ms);
            Assert.LessOrEqual(ms, 4000);
        }

        [Test]
        public void Load1000TestsUsingTestLoader()
        {
            loader = new TestLoader();
            int start = Environment.TickCount;
            loader.LoadProject("loadtest-assembly.dll");
            Assert.IsTrue(loader.IsProjectLoaded);
            loader.LoadTest();
            Assert.IsTrue(loader.IsTestLoaded);
            Assert.AreEqual(2050, loader.TestCount);
            int ms = Environment.TickCount - start;
            Console.WriteLine("Loaded in {0}ms", ms);
            Assert.LessOrEqual(ms, 4000);
        }
    }
}
