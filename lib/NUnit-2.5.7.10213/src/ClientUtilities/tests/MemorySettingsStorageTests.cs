// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class MemorySettingsStorageTests
	{
		MemorySettingsStorage storage;

		[SetUp]
		public void Init()
		{
			storage = new MemorySettingsStorage();
		}

		[TearDown]
		public void Cleanup()
		{
			storage.Dispose();
		}

		[Test]
		public void MakeStorage()
		{
			Assert.IsNotNull( storage );
		}

		[Test]
		public void SaveAndLoadSettings()
		{
			Assert.IsNull( storage.GetSetting( "X" ), "X is not null" );
			Assert.IsNull( storage.GetSetting( "NAME" ), "NAME is not null" );

			storage.SaveSetting("X", 5);
			storage.SaveSetting("NAME", "Charlie");

			Assert.AreEqual( 5, storage.GetSetting("X") );
			Assert.AreEqual( "Charlie", storage.GetSetting("NAME") );
		}

		[Test]
		public void RemoveSettings()
		{
			storage.SaveSetting("X", 5);
			storage.SaveSetting("NAME", "Charlie");

			storage.RemoveSetting( "X" );
			Assert.IsNull( storage.GetSetting( "X" ), "X not removed" );
			Assert.AreEqual( "Charlie", storage.GetSetting( "NAME" ) );

			storage.RemoveSetting( "NAME" );
			Assert.IsNull( storage.GetSetting( "NAME" ), "NAME not removed" );
		}

		[Test]
		public void MakeSubStorages()
		{
			ISettingsStorage sub1 = storage.MakeChildStorage( "Sub1" );
			ISettingsStorage sub2 = storage.MakeChildStorage( "Sub2" );

			Assert.IsNotNull( sub1, "Sub1 is null" );
			Assert.IsNotNull( sub2, "Sub2 is null" );
		}

		[Test]
		public void SubstorageSettings()
		{
			ISettingsStorage sub = storage.MakeChildStorage( "Sub" );

			sub.SaveSetting( "X", 5 );
			sub.SaveSetting( "NAME", "Charlie" );

			Assert.AreEqual( 5, sub.GetSetting( "X" ) );
			Assert.AreEqual( "Charlie", sub.GetSetting( "NAME" ) );

			sub.RemoveSetting( "X" );
			Assert.IsNull( sub.GetSetting( "X" ), "X not removed" );
			
			Assert.AreEqual( "Charlie", sub.GetSetting( "NAME" ) );

			sub.RemoveSetting( "NAME" );
			Assert.IsNull( sub.GetSetting( "NAME" ), "NAME not removed" );
		}

		[Test]
		public void TypeSafeSettings()
		{
			storage.SaveSetting( "X", 5);
			storage.SaveSetting( "Y", "17" );
			storage.SaveSetting( "NAME", "Charlie");

			Assert.AreEqual( 5, storage.GetSetting("X") );
			Assert.AreEqual( "17", storage.GetSetting( "Y" ) );
			Assert.AreEqual( "Charlie", storage.GetSetting( "NAME" ) );
		}
	}
}
