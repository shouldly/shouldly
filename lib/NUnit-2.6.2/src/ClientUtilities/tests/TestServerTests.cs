// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NUnit.Framework;
using NUnit.Core;

namespace NUnit.Util.Tests
{
	/// <summary>
	/// Summary description for TestServerTests.
	/// </summary>
	// Exclude for release [TestFixture,Platform(Exclude="Mono")]
    //public class TestServerTests
    //{
    //    private string serverPath = "nunit-server.exe";

    //    [TestFixtureSetUp]
    //    public void SetServerPath()
    //    {
    //        if ( File.Exists(serverPath) )
    //            return;

    //        DirectoryInfo cwd = new DirectoryInfo( Environment.CurrentDirectory );
    //        if( cwd.Parent.Name == "bin" )
    //        {
    //            string filePath = cwd.Parent.Parent.Parent.Parent.FullName;
    //            filePath = Path.Combine( filePath, "NUnitTestServer" );
    //            filePath = Path.Combine( filePath, "nunit-server-exe" );
    //            filePath = Path.Combine( filePath, "bin" );
    //            filePath = Path.Combine( filePath, cwd.Name );
    //            filePath = Path.Combine( filePath, "nunit-server.exe" );
    //            if( File.Exists( filePath ) )
    //            {
    //                serverPath = filePath;
    //                return;
    //            }
    //        }

    //        Assert.Fail( "Unable to find server" );
    //    }

    //    [Test]
    //    public void CanConnect()
    //    {
    //        using( TestServer server = new TestServer( "TestServer", 9000 ) )
    //        {
    //            server.Start();
    //            object obj = Activator.GetObject( typeof(TestRunner), ServerUtilities.MakeUrl("TestServer", 9000) );
    //            Assert.IsNotNull( obj, "Unable to connect" );
    //        }
    //    }

    //    [Test]
    //    public void CanConnectOutOfProcess()
    //    {
    //        Process process = null;
    //        try
    //        {
    //            process = Process.Start( serverPath, "TestServer" );
    //            System.Threading.Thread.Sleep( 1000 );
    //            TestServer server = (TestServer)Activator.GetObject( typeof(TestRunner), "tcp://localhost:9000/TestServer" );
    //            Assert.IsNotNull( server, "Unable to connect" );
    //            server.Stop();
    //        }
    //        finally
    //        {
    //            if ( process != null && !process.HasExited )
    //                process.Kill();
    //        }
    //    }
    //}
}
