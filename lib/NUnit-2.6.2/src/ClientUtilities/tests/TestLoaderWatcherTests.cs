// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Tests.Assemblies;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class TestLoaderWatcherTests
	{
		private readonly string assembly = MockAssembly.AssemblyPath;
		private MockAssemblyWatcher2 mockWatcher;
		private ITestLoader testLoader;
		private const string ReloadOnChangeSetting = "Options.TestLoader.ReloadOnChange";

		[SetUp]
		public void PreprareTestLoader()
		{
			// arrange
			mockWatcher = new MockAssemblyWatcher2();
			testLoader = new TestLoader(mockWatcher);
			testLoader.LoadProject(assembly);
		}

		[TearDown]
		public void CleanUpSettings()
		{
			Services.UserSettings.RemoveSetting(ReloadOnChangeSetting);
		}

		private void AssertWatcherIsPrepared()
		{
			Assert.IsTrue(mockWatcher.IsWatching);
			Assert.SamePath(assembly, mockWatcher.AssembliesToWatch[0]);
		}

		[Test]
		public void LoadShouldStartWatcher()
		{
			// act
			testLoader.LoadTest();

			// assert
			AssertWatcherIsPrepared();
            Assert.AreEqual(1, mockWatcher.DelegateCount);
        }

		[Test]
		public void ReloadShouldStartWatcher()
		{
			// arrange
			testLoader.LoadTest();
			mockWatcher.AssembliesToWatch = null;
			mockWatcher.IsWatching = false;

			// act
			testLoader.ReloadTest();

			// assert
			AssertWatcherIsPrepared();
            Assert.AreEqual(1, mockWatcher.DelegateCount);
        }

		[Test]
		public void UnloadShouldStopWatcherAndFreeResources()
		{
			// act
			testLoader.LoadTest();
			testLoader.UnloadTest();

			// assert
			Assert.IsFalse(mockWatcher.IsWatching);
			Assert.IsTrue(mockWatcher.AreResourcesFreed);
            Assert.AreEqual(0, mockWatcher.DelegateCount);
        }

		[Test]
		public void LoadShouldStartWatcherDependingOnSettings()
		{
			// arrange
			Services.UserSettings.SaveSetting(ReloadOnChangeSetting, false);
			testLoader.LoadTest();

			// assert
			Assert.IsFalse(mockWatcher.IsWatching);
            Assert.AreEqual(0, mockWatcher.DelegateCount);
        }

		[Test]
		public void ReloadShouldStartWatcherDependingOnSettings()
		{
			// arrange
			Services.UserSettings.SaveSetting(ReloadOnChangeSetting, false);
			testLoader.LoadTest();
			testLoader.ReloadTest();

			// assert
			Assert.IsFalse(mockWatcher.IsWatching);
            Assert.AreEqual(0, mockWatcher.DelegateCount);
        }
	}

	internal class MockAssemblyWatcher2 : IAssemblyWatcher
	{
		public bool IsWatching;
#if CLR_2_0 || CLR_4_0 || CLR_4_0
        public System.Collections.Generic.IList<string> AssembliesToWatch;
#else
		public System.Collections.IList AssembliesToWatch;
#endif
		public bool AreResourcesFreed;

		public void Stop()
		{
			IsWatching = false;
		}

		public void Start()
		{
			IsWatching = true;
		}

#if CLR_2_0 || CLR_4_0
		public void Setup(int delayInMs, System.Collections.Generic.IList<string> assemblies)
#else
        public void Setup(int delayInMs, System.Collections.IList assemblies)
#endif
		{
			AssembliesToWatch = assemblies;
		}

		public void Setup(int delayInMs, string assemblyFileName)
		{
			Setup(delayInMs, new string[] {assemblyFileName});
		}

		public void FreeResources()
		{
			AreResourcesFreed = true;
		}

        // This method is not used. It exists only to supress a 
        // warning about AssemblyChanged never being used
        public void FireAssemblyChanged(string path)
        {
            if (AssemblyChanged != null)
                AssemblyChanged(path);
        }

        public int DelegateCount
        {
            get 
            { 
                return AssemblyChanged == null
                    ? 0
                    : AssemblyChanged.GetInvocationList().Length; 
            }
        }

		public event AssemblyChangedHandler AssemblyChanged;
    }
}