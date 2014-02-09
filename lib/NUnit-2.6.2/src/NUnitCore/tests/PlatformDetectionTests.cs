// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Diagnostics;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for PlatformHelperTests.
	/// </summary>
	[TestFixture]
	public class PlatformDetectionTests
	{
		private static readonly PlatformHelper win95Helper = new PlatformHelper( 
			new OSPlatform( PlatformID.Win32Windows , new Version( 4, 0 ) ),
			new RuntimeFramework( RuntimeType.Net, new Version( 1, 1, 4322, 0 ) ) );

		private static readonly PlatformHelper winXPHelper = new PlatformHelper( 
			new OSPlatform( PlatformID.Win32NT , new Version( 5,1 ) ),
			new RuntimeFramework( RuntimeType.Net, new Version( 1, 1, 4322, 0 ) ) );

		private void CheckOSPlatforms( OSPlatform os, 
			string expectedPlatforms )
		{
			CheckPlatforms(
				new PlatformHelper( os, RuntimeFramework.CurrentFramework ),
				expectedPlatforms,
				PlatformHelper.OSPlatforms );
		}

		private void CheckRuntimePlatforms( RuntimeFramework runtimeFramework, 
			string expectedPlatforms )
		{
			CheckPlatforms(
				new PlatformHelper( OSPlatform.CurrentPlatform, runtimeFramework ),
				expectedPlatforms,
				PlatformHelper.RuntimePlatforms + ",NET-1.0,NET-1.1,NET-2.0,NET-3.0,NET-3.5,NET-4.0,NET-4.5,MONO-1.0,MONO-2.0,MONO-3.0,MONO-3.5,MONO-4.0" );
		}

		private void CheckPlatforms( PlatformHelper helper, 
			string expectedPlatforms, string checkPlatforms )
		{
			string[] expected = expectedPlatforms.Split( new char[] { ',' } );
			string[] check = checkPlatforms.Split( new char[] { ',' } );

			foreach( string testPlatform in check )
			{
				bool shouldPass = false;

				foreach( string platform in expected )
					if ( shouldPass = platform.ToLower() == testPlatform.ToLower() )
						break;

				bool didPass = helper.IsPlatformSupported( testPlatform );
				
				if ( shouldPass && !didPass )
					Assert.Fail( "Failed to detect {0}", testPlatform );
				else if ( didPass && !shouldPass )
					Assert.Fail( "False positive on {0}", testPlatform );
				else if ( !shouldPass && !didPass )
					Assert.AreEqual( "Only supported on " + testPlatform, helper.Reason );
			}
		}

		[Test]
		public void DetectWin95()
		{
			CheckOSPlatforms( 
				new OSPlatform( PlatformID.Win32Windows, new Version( 4, 0 ) ),
				"Win95,Win32Windows,Win32,Win" );
		}

		[Test]
		public void DetectWin98()
		{
			CheckOSPlatforms( 
				new OSPlatform( PlatformID.Win32Windows, new Version( 4, 10 ) ),
				"Win98,Win32Windows,Win32,Win" );
		}

		[Test]
		public void DetectWinMe()
		{
			CheckOSPlatforms( 
				new OSPlatform( PlatformID.Win32Windows, new Version( 4, 90 ) ),
				"WinMe,Win32Windows,Win32,Win" );
		}

        // WinCE isn't defined in .NET 1.0.
		[Test, Platform(Exclude="Net-1.0")]
		public void DetectWinCE()
		{
            PlatformID winCE = (PlatformID)Enum.Parse(typeof(PlatformID), "WinCE");
			CheckOSPlatforms(
                new OSPlatform(winCE, new Version(1, 0)),
				"WinCE,Win32,Win" );
		}

		[Test]
		public void DetectNT3()
		{
			CheckOSPlatforms( 
				new OSPlatform( PlatformID.Win32NT, new Version( 3, 51 ) ),
				"NT3,Win32NT,Win32,Win" );
		}

		[Test]
		public void DetectNT4()
		{
			CheckOSPlatforms( 
				new OSPlatform( PlatformID.Win32NT, new Version( 4, 0 ) ),
				"NT4,Win32NT,Win32,Win,Win-4.0" );
		}

		[Test]
		public void DetectWin2K()
		{
			CheckOSPlatforms( 
				new OSPlatform( PlatformID.Win32NT, new Version( 5, 0 ) ),
				"Win2K,NT5,Win32NT,Win32,Win,Win-5.0" );
		}

		[Test]
		public void DetectWinXP()
		{
			CheckOSPlatforms( 
				new OSPlatform( PlatformID.Win32NT, new Version( 5, 1 ) ),
				"WinXP,NT5,Win32NT,Win32,Win,Win-5.1" );
		}

		[Test]
		public void DetectWinXPProfessionalX64()
		{
			CheckOSPlatforms( 
				new OSPlatform( PlatformID.Win32NT, new Version( 5, 2 ), OSPlatform.ProductType.WorkStation ),
				"WinXP,NT5,Win32NT,Win32,Win,Win-5.1" );
		}

		[Test]
		public void DetectWin2003Server()
		{
            CheckOSPlatforms(
                new OSPlatform(PlatformID.Win32NT, new Version(5, 2), OSPlatform.ProductType.Server),
                "Win2003Server,NT5,Win32NT,Win32,Win,Win-5.2");
        }

        [Test]
        public void DetectVista()
        {
            CheckOSPlatforms(
                new OSPlatform(PlatformID.Win32NT, new Version(6, 0), OSPlatform.ProductType.WorkStation),
                "Vista,NT6,Win32NT,Win32,Win,Win-6.0");
        }

        [Test]
        public void DetectWin2008ServerOriginal()
        {
            CheckOSPlatforms(
                new OSPlatform(PlatformID.Win32NT, new Version(6, 0), OSPlatform.ProductType.Server),
                "Win2008Server,NT6,Win32NT,Win32,Win,Win-6.0");
        }

        [Test]
        public void DetectWin2008ServerR2()
        {
            CheckOSPlatforms(
                new OSPlatform(PlatformID.Win32NT, new Version(6, 1), OSPlatform.ProductType.Server),
                "Win2008Server,Win2008ServerR2,NT6,Win32NT,Win32,Win,Win-6.1");
        }

        [Test]
        public void DetectWindows7()
        {
            CheckOSPlatforms(
                new OSPlatform(PlatformID.Win32NT, new Version(6, 1), OSPlatform.ProductType.WorkStation),
                "Windows7,NT6,Win32NT,Win32,Win,Win-6.1");
        }

        [Test]
        public void DetectUnixUnderMicrosoftDotNet()
        {
            CheckOSPlatforms(
                new OSPlatform(OSPlatform.UnixPlatformID_Microsoft, new Version()),
                "UNIX,Linux");
        }

        // This throws under Microsoft .Net due to the invlaid enumeration value of 128
        [Test, Platform(Exclude="Net")]
        public void DetectUnixUnderMono()
        {
            CheckOSPlatforms(
                new OSPlatform(OSPlatform.UnixPlatformID_Mono, new Version()),
                "UNIX,Linux");
        }

#if (CLR_2_0 || CLR_4_0) && !NETCF
        [Test]
        public void DetectXbox()
        {
            CheckOSPlatforms(
                new OSPlatform(PlatformID.Xbox, new Version(0, 0)),
                "Xbox");
        }

        [Test]
        public void DetectMacOSX()
        {
            CheckOSPlatforms(
                new OSPlatform(PlatformID.MacOSX, new Version(0, 0)),
                "MacOSX");
        }
#endif

        [Test]
		public void DetectNet10()
		{
			CheckRuntimePlatforms(
				new RuntimeFramework( RuntimeType.Net, new Version( 1, 0, 3705, 0 ) ),
				"NET,NET-1.0" );
		}

		[Test]
		public void DetectNet11()
		{
			CheckRuntimePlatforms(
				new RuntimeFramework( RuntimeType.Net, new Version( 1, 1, 4322, 0 ) ),
				"NET,NET-1.1" );
		}

        [Test]
        public void DetectNet20()
        {
            CheckRuntimePlatforms(
                new RuntimeFramework(RuntimeType.Net, new Version(2, 0, 50727, 0)),
                "Net,Net-2.0");
        }

        [Test]
        public void DetectNet30()
        {
            CheckRuntimePlatforms(
                new RuntimeFramework(RuntimeType.Net, new Version(3, 0)),
                "Net,Net-2.0,Net-3.0");
        }

        [Test]
        public void DetectNet35()
        {
            CheckRuntimePlatforms(
                new RuntimeFramework(RuntimeType.Net, new Version(3, 5)),
                "Net,Net-2.0,Net-3.0,Net-3.5");
        }

        [Test]
        public void DetectNet40()
        {
            CheckRuntimePlatforms(
                new RuntimeFramework(RuntimeType.Net, new Version(4, 0, 30319, 0)),
                "Net,Net-4.0");
        }

		[Test]
		public void DetectNet45()
		{
			CheckRuntimePlatforms(
				new RuntimeFramework(RuntimeType.Net, new Version(4, 5)),
				"Net,Net-4.0,Net-4.5");
		}

		[Test]
		public void DetectNetCF()
		{
			CheckRuntimePlatforms(
				new RuntimeFramework( RuntimeType.NetCF, new Version( 1, 1, 4322, 0 ) ),
				"NetCF" );
		}

		[Test]
		public void DetectSSCLI()
		{
			CheckRuntimePlatforms(
				new RuntimeFramework( RuntimeType.SSCLI, new Version( 1, 0, 3, 0 ) ),
				"SSCLI,Rotor" );
		}

		[Test]
		public void DetectMono10()
		{
			CheckRuntimePlatforms(
				new RuntimeFramework( RuntimeType.Mono, new Version( 1, 1, 4322, 0 ) ),
				"Mono,Mono-1.0" );
		}

        [Test]
        public void DetectMono20()
        {
            CheckRuntimePlatforms(
                new RuntimeFramework(RuntimeType.Mono, new Version(2, 0, 50727, 0)),
                "Mono,Mono-2.0");
        }

        [Test]
        public void DetectMono30()
        {
            CheckRuntimePlatforms(
                new RuntimeFramework(RuntimeType.Mono, new Version(3, 0)),
                "Mono,Mono-2.0,Mono-3.0");
        }

        [Test]
        public void DetectMono35()
        {
            CheckRuntimePlatforms(
                new RuntimeFramework(RuntimeType.Mono, new Version(3, 5)),
                "Mono,Mono-2.0,Mono-3.0,Mono-3.5");
        }

        [Test]
        public void DetectMono40()
        {
            CheckRuntimePlatforms(
                new RuntimeFramework(RuntimeType.Mono, new Version(4, 0, 30319)),
                "Mono,Mono-4.0");
        }

        [Test]
		public void DetectExactVersion()
		{
			Assert.IsTrue( winXPHelper.IsPlatformSupported( "net-1.1.4322" ) );
			Assert.IsTrue( winXPHelper.IsPlatformSupported( "net-1.1.4322.0" ) );
			Assert.IsFalse( winXPHelper.IsPlatformSupported( "net-1.1.4323.0" ) );
			Assert.IsFalse( winXPHelper.IsPlatformSupported( "net-1.1.4322.1" ) );
		}

		[Test]
		public void ArrayOfPlatforms()
		{
			string[] platforms = new string[] { "NT4", "Win2K", "WinXP" };
			Assert.IsTrue( winXPHelper.IsPlatformSupported( platforms ) );
			Assert.IsFalse( win95Helper.IsPlatformSupported( platforms ) );
		}

		[Test]
		public void PlatformAttribute_Include()
		{
			PlatformAttribute attr = new PlatformAttribute( "Win2K,WinXP,NT4" );
			Assert.IsTrue( winXPHelper.IsPlatformSupported( attr ) );
			Assert.IsFalse( win95Helper.IsPlatformSupported( attr ) );
			Assert.AreEqual("Only supported on Win2K,WinXP,NT4", win95Helper.Reason);
		}

		[Test]
		public void PlatformAttribute_Exclude()
		{
			PlatformAttribute attr = new PlatformAttribute();
			attr.Exclude = "Win2K,WinXP,NT4";
			Assert.IsFalse( winXPHelper.IsPlatformSupported( attr ) );
			Assert.AreEqual( "Not supported on Win2K,WinXP,NT4", winXPHelper.Reason );
			Assert.IsTrue( win95Helper.IsPlatformSupported( attr ) );
		}

		[Test]
		public void PlatformAttribute_IncludeAndExclude()
		{
			PlatformAttribute attr = new PlatformAttribute( "Win2K,WinXP,NT4" );
			attr.Exclude = "Mono";
			Assert.IsFalse( win95Helper.IsPlatformSupported( attr ) );
			Assert.AreEqual( "Only supported on Win2K,WinXP,NT4", win95Helper.Reason );
			Assert.IsTrue( winXPHelper.IsPlatformSupported( attr ) );
			attr.Exclude = "Net";
			Assert.IsFalse( win95Helper.IsPlatformSupported( attr ) );
			Assert.AreEqual( "Only supported on Win2K,WinXP,NT4", win95Helper.Reason );
			Assert.IsFalse( winXPHelper.IsPlatformSupported( attr ) );
			Assert.AreEqual( "Not supported on Net", winXPHelper.Reason );
		}

		[Test]
		public void PlatformAttribute_InvalidPlatform()
		{
			PlatformAttribute attr = new PlatformAttribute( "Net-1.0,Net11,Mono" );
			Assert.IsFalse( winXPHelper.IsPlatformSupported( attr ) );
			Assert.AreEqual( "Invalid platform name: Net11", winXPHelper.Reason );
		}
	}
}
