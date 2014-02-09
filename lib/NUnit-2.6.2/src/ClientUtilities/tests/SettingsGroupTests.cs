// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using Microsoft.Win32;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class SettingsGroupTests
	{
		private SettingsGroup testGroup;

		[SetUp]
		public void BeforeEachTest()
		{
			MemorySettingsStorage storage = new MemorySettingsStorage();
			testGroup = new SettingsGroup( storage );
		}

		[TearDown]
		public void AfterEachTest()
		{
			testGroup.Dispose();
		}

		[Test]
		public void TopLevelSettings()
		{
			testGroup.SaveSetting( "X", 5 );
			testGroup.SaveSetting( "NAME", "Charlie" );
			Assert.AreEqual( 5, testGroup.GetSetting( "X" ) );
			Assert.AreEqual( "Charlie", testGroup.GetSetting( "NAME" ) );

			testGroup.RemoveSetting( "X" );
			Assert.IsNull( testGroup.GetSetting( "X" ), "X not removed" );
			Assert.AreEqual( "Charlie", testGroup.GetSetting( "NAME" ) );

			testGroup.RemoveSetting( "NAME" );
			Assert.IsNull( testGroup.GetSetting( "NAME" ), "NAME not removed" );
		}

		[Test]
		public void SubGroupSettings()
		{
			SettingsGroup subGroup = new SettingsGroup( testGroup.Storage );
			Assert.IsNotNull( subGroup );
			Assert.IsNotNull( subGroup.Storage );

			subGroup.SaveSetting( "X", 5 );
			subGroup.SaveSetting( "NAME", "Charlie" );
			Assert.AreEqual( 5, subGroup.GetSetting( "X" ) );
			Assert.AreEqual( "Charlie", subGroup.GetSetting( "NAME" ) );

			subGroup.RemoveSetting( "X" );
			Assert.IsNull( subGroup.GetSetting( "X" ), "X not removed" );
			Assert.AreEqual( "Charlie", subGroup.GetSetting( "NAME" ) );

			subGroup.RemoveSetting( "NAME" );
			Assert.IsNull( subGroup.GetSetting( "NAME" ), "NAME not removed" );
		}

		[Test]
		public void TypeSafeSettings()
		{
			testGroup.SaveSetting( "X", 5);
			testGroup.SaveSetting( "Y", "17" );
			testGroup.SaveSetting( "NAME", "Charlie");

			Assert.AreEqual( 5, testGroup.GetSetting("X") );
			Assert.AreEqual( "17", testGroup.GetSetting( "Y" ) );
			Assert.AreEqual( "Charlie", testGroup.GetSetting( "NAME" ) );
		}

		[Test]
		public void DefaultSettings()
		{
			Assert.IsNull( testGroup.GetSetting( "X" ) );
			Assert.IsNull( testGroup.GetSetting( "NAME" ) );

			Assert.AreEqual( 5, testGroup.GetSetting( "X", 5 ) );
			Assert.AreEqual( 6, testGroup.GetSetting( "X", 6 ) );
			Assert.AreEqual( "7", testGroup.GetSetting( "X", "7" ) );

			Assert.AreEqual( "Charlie", testGroup.GetSetting( "NAME", "Charlie" ) );
			Assert.AreEqual( "Fred", testGroup.GetSetting( "NAME", "Fred" ) );
		}

		[Test]
		public void BadSetting()
		{
			testGroup.SaveSetting( "X", "1y25" );
			Assert.AreEqual( 12, testGroup.GetSetting( "X", 12 ) );
		}
	}
}
