// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
	/// <summary>
	/// This fixture is used to set up a distinct ServiceMangager with its
	/// own services in the test domain when testing NUnit. We start and
	/// stop services selectively. In particular, we don't want to 
	/// do a StopAllServices command, because that would cause any 
	/// changes to UserSettings to be saved. 
	/// 
	/// TODO: Refactor SettingsService so we can use it without actually
	/// touching the backup storage.
	/// </summary>
	[SetUpFixture]
	public class ServiceManagerSetUpFixture
	{
		[SetUp]
		public void CreateServicesForTestDomain()
		{
			ServiceManager.Services.AddService( new DummySettingsService() );
			ServiceManager.Services.AddService( new DomainManager() );
			ServiceManager.Services.AddService( new ProjectService() );
			ServiceManager.Services.AddService( new TestAgency( "TestDomain_TestAgency", 0 ) );
			Services.TestAgency.Start();
		}

		[TearDown]
		public void ClearServices()
		{
			Services.TestAgency.Stop();
			ServiceManager.Services.ClearServices();
		}
	}

    class DummySettingsService : SettingsGroup, NUnit.Core.IService
    {
        public DummySettingsService()
        {
            this.storage = new MemorySettingsStorage();
        }

        #region IService Members

        public void InitializeService()
        {
        }

        public void UnloadService()
        {
        }

        #endregion
    }
}
