// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Threading;
using System.Globalization;
using NUnit.Framework;
using NUnit.TestData.CultureAttributeTests;
using NUnit.TestUtilities;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for CultureDetectionTests.
	/// </summary>
	[TestFixture]
	public class CultureSettingAndDetectionTests
	{
		private CultureDetector detector = new CultureDetector("fr-FR");

		private void ExpectMatch( string culture )
		{
			if ( !detector.IsCultureSupported( culture ) )
				Assert.Fail( string.Format( "Failed to match \"{0}\"" , culture ) );
		}

		private void ExpectMatch( CultureAttribute attr )
		{
			if ( !detector.IsCultureSupported( attr ) )
				Assert.Fail( string.Format( "Failed to match attribute with Include=\"{0}\",Exclude=\"{1}\"", attr.Include, attr.Exclude ) );
		}

		private void ExpectFailure( string culture )
		{
			if ( detector.IsCultureSupported( culture ) )
				Assert.Fail( string.Format( "Should not match \"{0}\"" , culture ) );
			Assert.AreEqual( "Only supported under culture " + culture, detector.Reason );
		}

		private void ExpectFailure( CultureAttribute attr, string msg )
		{
			if ( detector.IsCultureSupported( attr ) )
				Assert.Fail( string.Format( "Should not match attribute with Include=\"{0}\",Exclude=\"{1}\"",
					attr.Include, attr.Exclude ) );
			Assert.AreEqual( msg, detector.Reason );
		}

		[Test]
		public void CanMatchStrings()
		{
			ExpectMatch( "fr-FR" );
			ExpectMatch( "fr" );
			ExpectMatch( "fr-FR,fr-BE,fr-CA" );
			ExpectMatch( "en,de,fr,it" );
			ExpectFailure( "en-GB" );
			ExpectFailure( "en" );
			ExpectFailure( "fr-CA" );
			ExpectFailure( "fr-BE,fr-CA" );
			ExpectFailure( "en,de,it" );
		}

		[Test]
		public void CanMatchAttributeWithInclude()
		{
			ExpectMatch( new CultureAttribute( "fr-FR" ) );
			ExpectMatch( new CultureAttribute( "fr-FR,fr-BE,fr-CA" ) );
			ExpectFailure( new CultureAttribute( "en" ), "Only supported under culture en" );
			ExpectFailure( new CultureAttribute( "en,de,it" ), "Only supported under culture en,de,it" );
		}

		[Test]
		public void CanMatchAttributeWithExclude()
		{
			CultureAttribute attr = new CultureAttribute();
			attr.Exclude = "en";
			ExpectMatch( attr );
			attr.Exclude = "en,de,it";
			ExpectMatch( attr );
			attr.Exclude = "fr";
			ExpectFailure( attr, "Not supported under culture fr");
			attr.Exclude = "fr-FR,fr-BE,fr-CA";
			ExpectFailure( attr, "Not supported under culture fr-FR,fr-BE,fr-CA" );
		}

		[Test]
		public void CanMatchAttributeWithIncludeAndExclude()
		{
			CultureAttribute attr = new CultureAttribute( "en,fr,de,it" );
			attr.Exclude="fr-CA,fr-BE";
			ExpectMatch( attr );
			attr.Exclude = "fr-FR";
			ExpectFailure( attr, "Not supported under culture fr-FR" );
		}

		[Test,SetCulture("fr-FR")]
		public void LoadWithFrenchCulture()
		{
			Assert.AreEqual( "fr-FR", CultureInfo.CurrentCulture.Name, "Culture not set correctly" );
			TestSuite fixture = TestBuilder.MakeFixture( typeof( FixtureWithCultureAttribute ) );
			Assert.AreEqual( RunState.Runnable, fixture.RunState, "Fixture" );
			foreach( Test test in fixture.Tests )
			{
				RunState expected = test.TestName.Name == "FrenchTest" ? RunState.Runnable : RunState.Skipped;
				Assert.AreEqual( expected, test.RunState, test.TestName.Name );
			}
		}

		[Test,SetCulture("fr-CA")]
		public void LoadWithFrenchCanadianCulture()
		{
			Assert.AreEqual( "fr-CA", CultureInfo.CurrentCulture.Name, "Culture not set correctly" );
			TestSuite fixture = TestBuilder.MakeFixture( typeof( FixtureWithCultureAttribute ) );
			Assert.AreEqual( RunState.Runnable, fixture.RunState, "Fixture" );
			foreach( Test test in fixture.Tests )
			{
				RunState expected = test.TestName.Name.StartsWith( "French" ) ? RunState.Runnable : RunState.Skipped;
				Assert.AreEqual( expected, test.RunState, test.TestName.Name );
			}
		}

		[Test,SetCulture("ru-RU")]
		public void LoadWithRussianCulture()
		{
			Assert.AreEqual( "ru-RU", CultureInfo.CurrentCulture.Name, "Culture not set correctly" );
			TestSuite fixture = TestBuilder.MakeFixture( typeof( FixtureWithCultureAttribute ) );
			Assert.AreEqual( RunState.Skipped, fixture.RunState, "Fixture" );
			foreach( Test test in fixture.Tests )
				Assert.AreEqual( RunState.Skipped, test.RunState, test.TestName.Name );
		}

		[Test]
		public void SettingInvalidCultureGivesError()
		{
			TestResult result = TestBuilder.RunTestCase( typeof( InvalidCultureFixture ), "InvalidCultureSet" );
			Assert.AreEqual( ResultState.Error, result.ResultState );

            Assert.That(result.Message, Text.StartsWith("System.ArgumentException").Or.StartsWith("System.Globalization.CultureNotFoundException"));
            Assert.That(result.Message, Text.Contains("xx-XX").IgnoreCase);
		}

		[TestFixture, SetCulture("en-GB")]
		public class NestedFixture
		{
			[Test]
			public void CanSetCultureOnFixture()
			{
				Assert.AreEqual( "en-GB", CultureInfo.CurrentCulture.Name );
			}
		}

#if CLR_2_0 || CLR_4_0
        [Test, SetCulture("de-DE")]
        [TestCase(Result="01.06.2010 00:00:00")]
        public string UseWithParameterizedTest()
        {
            return new DateTime(2010, 6, 1).ToString();
        }
#endif
	}
}
