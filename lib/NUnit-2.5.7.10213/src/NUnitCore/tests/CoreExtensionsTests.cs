// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Text;
using System.Reflection;
using NUnit.Framework;
using NUnit.Core.Extensibility;
using NUnit.Mocks;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class CoreExtensionsTests
	{
		private CoreExtensions host;

		[SetUp]
		public void CreateHost()
		{
			host = new CoreExtensions();
		}

		[Test]
		public void HasSuiteBuildersExtensionPoint()
		{
			IExtensionPoint ep = host.GetExtensionPoint( "SuiteBuilders" );
			Assert.IsNotNull( ep );
			Assert.AreEqual( "SuiteBuilders", ep.Name );
			Assert.AreEqual( typeof( SuiteBuilderCollection ), ep.GetType() );
		}

		[Test]
		public void HasTestCaseBuildersExtensionPoint()
		{
			IExtensionPoint ep = host.GetExtensionPoint( "TestCaseBuilders" );
			Assert.IsNotNull( ep );
			Assert.AreEqual( "TestCaseBuilders", ep.Name );
			Assert.AreEqual( typeof( TestCaseBuilderCollection ), ep.GetType() );
		}

		[Test]
		public void HasTestDecoratorsExtensionPoint()
		{
			IExtensionPoint ep = host.GetExtensionPoint( "TestDecorators" );
			Assert.IsNotNull( ep );
			Assert.AreEqual( "TestDecorators", ep.Name );
			Assert.AreEqual( typeof( TestDecoratorCollection ), ep.GetType() );
		}

		[Test]
		public void HasEventListenerExtensionPoint()
		{
			IExtensionPoint ep = host.GetExtensionPoint( "EventListeners" );
			Assert.IsNotNull( ep );
			Assert.AreEqual( "EventListeners", ep.Name );
			Assert.AreEqual( typeof( EventListenerCollection ), ep.GetType() );
		}

		[Test]
		public void HasTestFrameworkRegistry()
		{
			IExtensionPoint ep = host.GetExtensionPoint( "FrameworkRegistry" );
			Assert.IsNotNull( ep );
			Assert.AreEqual( "FrameworkRegistry", ep.Name );
			Assert.AreEqual( typeof( FrameworkRegistry ), ep.GetType() );
		}

		[Test]
		public void CanAddDecorator()
		{
			DynamicMock mock = new DynamicMock( typeof(ITestDecorator) );
			mock.Expect( "Decorate" );

            IExtensionPoint ep = host.GetExtensionPoint("TestDecorators");
            ep.Install(mock.MockInstance);

            ITestDecorator decorators = (ITestDecorator)ep;
            decorators.Decorate(null, null);

			mock.Verify();
		}

        class MockDecorator : ITestDecorator
        {
            private string name;
            private StringBuilder sb;

            public MockDecorator(string name, StringBuilder sb)
            {
                this.name = name;
                this.sb = sb;
            }

            public Test Decorate(Test test, MemberInfo member)
            {
                sb.Append(name);
                return test;
            }
        }

	    [Test]
        public void DecoratorsRunInOrderOfPriorities()
        {
            StringBuilder sb = new StringBuilder();

            ITestDecorator mock0 = new MockDecorator("mock0", sb);
            ITestDecorator mock1 = new MockDecorator("mock1", sb);
            ITestDecorator mock3a = new MockDecorator("mock3a", sb);
            ITestDecorator mock3b = new MockDecorator("mock3b", sb);
            ITestDecorator mock3c = new MockDecorator("mock3c", sb);
            ITestDecorator mock5a = new MockDecorator("mock5a", sb);
            ITestDecorator mock5b = new MockDecorator("mock5b", sb);
            ITestDecorator mock8 = new MockDecorator("mock8", sb);
            ITestDecorator mock9 = new MockDecorator("mock9", sb);

            IExtensionPoint2 ep = (IExtensionPoint2)host.GetExtensionPoint("TestDecorators");
            ep.Install(mock8, 8);
            ep.Install(mock5a, 5);
            ep.Install(mock1, 1);
            ep.Install(mock3a, 3);
            ep.Install(mock3b, 3);
            ep.Install(mock9, 9);
            ep.Install(mock3c, 3);
            ep.Install(mock0);
            ep.Install(mock5b, 5);

            ITestDecorator decorators = (ITestDecorator)ep;
            decorators.Decorate(null, null);
            Assert.AreEqual("mock0mock1mock3cmock3bmock3amock5bmock5amock8mock9", sb.ToString());

            sb.Remove(0, sb.Length);
	        decorators.Decorate(null, null);
            Assert.AreEqual("mock0mock1mock3cmock3bmock3amock5bmock5amock8mock9", sb.ToString());
        }

	    [Test]
		public void CanAddSuiteBuilder()
		{
			DynamicMock mock = new DynamicMock( typeof(ISuiteBuilder) );
			mock.ExpectAndReturn( "CanBuildFrom", true, null );
			mock.Expect( "BuildFrom" );
			
			IExtensionPoint ep = host.GetExtensionPoint("SuiteBuilders");
			ep.Install( mock.MockInstance );
			ISuiteBuilder builders = (ISuiteBuilder)ep;
			builders.BuildFrom( null );

			mock.Verify();
		}

        [Test]
        public void CanAddTestCaseBuilder()
        {
            DynamicMock mock = new DynamicMock(typeof(ITestCaseBuilder));
            mock.ExpectAndReturn("CanBuildFrom", true, null);
            mock.Expect("BuildFrom");

            IExtensionPoint ep = host.GetExtensionPoint("TestCaseBuilders");
            ep.Install(mock.MockInstance);
            ITestCaseBuilder builders = (ITestCaseBuilder)ep;
            builders.BuildFrom(null);

            mock.Verify();
        }

        [Test]
        public void CanAddTestCaseBuilder2()
        {
            DynamicMock mock = new DynamicMock(typeof(ITestCaseBuilder2));
            mock.ExpectAndReturn("CanBuildFrom", true, null);
            mock.Expect("BuildFrom");

            IExtensionPoint ep = host.GetExtensionPoint("TestCaseBuilders");
            ep.Install(mock.MockInstance);
            ITestCaseBuilder2 builders = (ITestCaseBuilder2)ep;
            builders.BuildFrom(null, null);

            mock.Verify();
        }

        [Test]
		public void CanAddEventListener()
		{
			DynamicMock mock = new DynamicMock( typeof(EventListener) );
			mock.Expect( "RunStarted" );
			mock.Expect( "RunFinished" );

			IExtensionPoint ep = host.GetExtensionPoint("EventListeners");
			ep.Install( mock.MockInstance );
			EventListener listeners = (EventListener)ep;
			listeners.RunStarted( "test", 0 );
			listeners.RunFinished( new TestResult( new TestInfo( new TestSuite( "test" ) ) ) );

			mock.Verify();
		}
	}
}
