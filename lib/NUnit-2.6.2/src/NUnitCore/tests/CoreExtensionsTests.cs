// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Text;
using System.Reflection;
#if NET_3_5 || NET_4_0 || NET_4_5
using NSubstitute;
#endif
using NUnit.Framework;
using NUnit.Core.Extensibility;

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

#if NET_3_5 || NET_4_0 || NET_4_5
        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0")]
		public void CanAddDecorator()
		{
            ITestDecorator mockDecorator = Substitute.For<ITestDecorator>();

            IExtensionPoint ep = host.GetExtensionPoint("TestDecorators");
            ep.Install(mockDecorator);

            ITestDecorator decorators = (ITestDecorator)ep;
            decorators.Decorate(null, null);

            mockDecorator.Received().Decorate(null, null);
		}

        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0")]
		public void CanAddSuiteBuilder()
		{
            ISuiteBuilder mockBuilder = Substitute.For<ISuiteBuilder>();
            mockBuilder.CanBuildFrom(Arg.Any<Type>()).Returns(true);
			
			IExtensionPoint ep = host.GetExtensionPoint("SuiteBuilders");
            ep.Install(mockBuilder);
			ISuiteBuilder builders = (ISuiteBuilder)ep;
			builders.BuildFrom( null );

            mockBuilder.Received().BuildFrom(null);
		}

        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0")]
        public void CanAddTestCaseBuilder()
        {
            ITestCaseBuilder mockBuilder = Substitute.For<ITestCaseBuilder>();
            mockBuilder.CanBuildFrom(null).Returns(true);

            IExtensionPoint ep = host.GetExtensionPoint("TestCaseBuilders");
            ep.Install(mockBuilder);
            ITestCaseBuilder builders = (ITestCaseBuilder)ep;
            builders.BuildFrom(null);

            mockBuilder.Received().BuildFrom(null);
        }

        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0")]
        public void CanAddTestCaseBuilder2()
        {
            ITestCaseBuilder2 mockBuilder = Substitute.For<ITestCaseBuilder2>();
            mockBuilder.CanBuildFrom(null, null).Returns(true);

            IExtensionPoint ep = host.GetExtensionPoint("TestCaseBuilders");
            ep.Install(mockBuilder);
            ITestCaseBuilder2 builders = (ITestCaseBuilder2)ep;
            builders.BuildFrom(null, null);

            mockBuilder.Received().BuildFrom(null, null);
        }

        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0")]
		public void CanAddEventListener()
		{
            EventListener mockListener = Substitute.For<EventListener>();

			IExtensionPoint ep = host.GetExtensionPoint("EventListeners");
			ep.Install( mockListener );
			EventListener listeners = (EventListener)ep;

			listeners.RunStarted( "test", 0 );
            mockListener.Received().RunStarted("test", 0);

			listeners.RunFinished( new TestResult( new TestInfo( new TestSuite( "test" ) ) ) );
            mockListener.Received().RunFinished(Arg.Is<TestResult>(x=>x.Name=="test"));
		}
#endif
	}
}
