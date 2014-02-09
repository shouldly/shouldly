// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	// TODO: Make this work for .NET 2.0
	[TestFixture]
	public class CallContextTests
	{
		const string CONTEXT_DATA = "MyContextData";
//		IPrincipal savedPrincipal;

//		[SetUp]
//		public void SaveCurrentPrincipal()
//		{
//			savedPrincipal = System.Threading.Thread.CurrentPrincipal;
//		}
//
//		[TearDown]
//		public void RestoreCurrentPrincipal()
//		{
//			System.Threading.Thread.CurrentPrincipal = savedPrincipal;
//			CallContext.FreeNamedDataSlot(CONTEXT_DATA);
//		}

        [TestFixtureTearDown]
        public void FreeCallContextDataSlot()
        {
            // NOTE: We don't want possible side effects on other cross context tests.
            CallContext.FreeNamedDataSlot(CONTEXT_DATA);
        }

		[Test]
		public void ILogicalThreadAffinativeTest()
		{	
#if CLR_2_0 || CLR_4_0
            CallContext.LogicalSetData(CONTEXT_DATA, new EmptyCallContextData());
#else
            CallContext.SetData(CONTEXT_DATA, new EmptyCallContextData());
#endif
		}

		[Test]
		public void ILogicalThreadAffinativeTestConsole()
		{	
#if CLR_2_0 || CLR_4_0
			CallContext.LogicalSetData( CONTEXT_DATA, new EmptyCallContextData() );
#else
			CallContext.SetData( CONTEXT_DATA, new EmptyCallContextData() );
#endif
            // TODO: make this Assertable
			//Console.WriteLine("ILogicalThreadAffinativeTest");
			Console.Out.Flush();
		}

		[Test]
		public void GenericPrincipalTest()
		{
			GenericIdentity ident = new GenericIdentity("Bob");
			GenericPrincipal prpal = new GenericPrincipal(ident, 
				new string[] {"Level1"});

#if CLR_2_0 || CLR_4_0
			CallContext.LogicalSetData( CONTEXT_DATA, new PrincipalCallContextData( prpal ) );
#else
			CallContext.SetData( CONTEXT_DATA, new PrincipalCallContextData( prpal ) );
#endif
		}

		[Test]
		public void SetGenericPrincipalOnThread()
		{
			GenericIdentity ident = new GenericIdentity("Bob");
			GenericPrincipal prpal = new GenericPrincipal(ident, 
				new string[] {"Level1"});

			System.Threading.Thread.CurrentPrincipal = prpal;
		}

		[Test]
		public void SetCustomPrincipalOnThread()
		{
			MyPrincipal prpal = new MyPrincipal();

			System.Threading.Thread.CurrentPrincipal = prpal;
		}

		[Test]
		public void UseCustomIdentity()
		{
			TestIdentity ident = new TestIdentity( "test" );
			GenericPrincipal principal = new GenericPrincipal( ident, new string[] { "Level1" } );
		
			System.Threading.Thread.CurrentPrincipal = principal;
		}
	}

	/// <summary>
	/// Helper class that implements ILogicalThreadAffinative
	/// but holds no data at all
	/// </summary>
	[Serializable]
	public class EmptyCallContextData : ILogicalThreadAffinative
	{
	}

	[Serializable]
	public class PrincipalCallContextData : ILogicalThreadAffinative
	{
		public PrincipalCallContextData( IPrincipal principal )
		{
		}
	}

	[Serializable]
	public class MyPrincipal : IPrincipal
	{
		public IIdentity Identity
		{
			get
			{
				// TODO:  Add MyPrincipal.Identity getter implementation
				return null;
			}
		}

		public bool IsInRole(string role)
		{
			// TODO:  Add MyPrincipal.IsInRole implementation
			return false;
		}
	}

	[Serializable]
	public class TestIdentity : GenericIdentity
	{
		public TestIdentity( string name ) : base( name ) { }
	}

}
