// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.Mocks.Tests
{
	/// <summary>
	/// Summary description for DynamicMockTests.
	/// </summary>
	[TestFixture]
    [Obsolete("NUnit now uses NSubstitute")]
    public class DynamicMockTests
	{
		private DynamicMock mock;
		private IStuff instance;

		[SetUp]
		public void CreateMock()
		{
			mock = new DynamicMock( typeof( IStuff ) );
			instance = (IStuff)mock.MockInstance;
		}

		[Test]
		public void MockHasDefaultName()
		{
			Assert.AreEqual( "MockIStuff", mock.Name );
		}

		[Test]
		public void MockHasNonDefaultName()
		{
			DynamicMock mock2 = new DynamicMock( "MyMock", typeof( IStuff ) );
			Assert.AreEqual( "MyMock", mock2.Name );
		}

		[Test]
		public void CallMethod()
		{
			instance.DoSomething();
			mock.Verify();
		}

		[Test]
		public void CallMethodWithArgs()
		{
			instance.DoSomething( "hello" );
			mock.Verify();
		}

		[Test]
		public void ExpectedMethod()
		{
			mock.Expect( "DoSomething" );
			instance.DoSomething();
			mock.Verify();
		}

		[Test, ExpectedException( typeof(AssertionException) )]
		public void ExpectedMethodNotCalled()
		{
			mock.Expect( "DoSomething" );
			mock.Verify();
		}

		[Test]
		public void RefParameter()
		{
			int x = 7;
			mock.Expect( "Increment" );
			instance.Increment( ref x );
			mock.Verify();
		}

		[Test]
		public void MethodWithReturnValue()
		{
			mock.SetReturnValue( "GetInt", 5 );
			Assert.AreEqual( 5, instance.GetInt() );
			mock.Verify();
		}

		[Test]
		public void DefaultReturnValues()
		{
			Assert.AreEqual( 0, instance.GetInt(), "int" );
			Assert.AreEqual( 0, instance.GetSingle(), "float" );
			Assert.AreEqual( 0, instance.GetDouble(), "double" );
			Assert.AreEqual( 0, instance.GetDecimal(), "decimal" );
			Assert.AreEqual( '?', instance.GetChar(), "char" );
			mock.Verify();
		}

		[Test, ExpectedException( typeof(InvalidCastException) )]
		public void WrongReturnType()
		{
			mock.SetReturnValue( "GetInt", "hello" );
			instance.GetInt();
			mock.Verify();
		}

		[Test]
		public void OverrideMethodOnDynamicMock()
		{
			DynamicMock derivedMock = new DerivedMock();
			IStuff derivedInstance = (IStuff)derivedMock.MockInstance;
			Assert.AreEqual( 17, derivedInstance.Add( 5, 12 ) );
			derivedMock.Verify();
		}

		[Test, ExpectedException( typeof(ArgumentException) )]
		public void CreateMockForNonMBRClassFails()
		{
			DynamicMock classMock = new DynamicMock( typeof( NonMBRClass ) );
			instance = classMock.MockInstance as IStuff;
		}

		[Test]
		public void CreateMockForMBRClass()
		{
			DynamicMock classMock = new DynamicMock( typeof( MBRClass ) );
			MBRClass classInstance = (MBRClass)classMock.MockInstance;
			classMock.Expect( "SomeMethod" );
			classMock.ExpectAndReturn( "AnotherMethod", "Hello World", 5, "hello" );
			classMock.ExpectAndReturn( "MethodWithParams", 42, new object[] { new string[] { "the", "answer" } } );
			classInstance.SomeMethod();
			Assert.AreEqual( "Hello World", classInstance.AnotherMethod( 5, "hello" ) );
			Assert.AreEqual( 42, classInstance.MethodWithParams( "the", "answer" ) );
			classMock.Verify();
		}

		#region Test Interfaces and Classes

		interface IStuff
		{
			void DoSomething();
			void DoSomething( string greeting );
			int GetInt();
			float GetSingle();
			double GetDouble();
			decimal GetDecimal();
			char GetChar();
			int Add( int a, int b );
			void Increment( ref int n );
		}

		class DerivedMock : DynamicMock
		{
			public DerivedMock() : base( "Derived", typeof( IStuff ) ) { }

			public override object Call( string methodName, params object[] args )
			{
				switch( methodName )
				{
					case "Add":
						return (int)args[0] + (int)args[1];
					default:
						return base.Call( methodName, args );
				}
			}
		}

		class NonMBRClass
		{
		}

		class MBRClass : MarshalByRefObject
		{
			public void SomeMethod(){ }
			public string AnotherMethod( int a, string b ) { return b + a.ToString(); }
			public int MethodWithParams( params string[] args ) { return args.Length; }
		}

		#endregion
	}
}
