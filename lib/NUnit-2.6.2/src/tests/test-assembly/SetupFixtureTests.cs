// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Text;
using NUnit.Framework;

namespace NUnit.TestUtilities
{
    /// <summary>
    /// A helper to Verify that Setup/Teardown 'events' occur, and that they are in the correct order...
    /// </summary>
    public class SimpleEventRecorder
    {
        private static System.Collections.Queue _events;

        /// <summary>
        /// Initializes the <see cref="T:EventRegistrar"/> 'static' class.
        /// </summary>
        static SimpleEventRecorder()
        {
            _events = new System.Collections.Queue();
        }

        /// <summary>
        /// Registers an event.
        /// </summary>
        /// <param name="evnt">The event to register.</param>
        public static void RegisterEvent(string evnt)
        {
            _events.Enqueue(evnt);
        }


        /// <summary>
        /// Verifies the specified expected events occurred and that they occurred in the specified order.
        /// </summary>
        /// <param name="expectedEvents">The expected events.</param>
        public static void Verify(params string[] expectedEvents)
        {
            foreach (string expected in expectedEvents)
            {
                string actual = _events.Count > 0 ? _events.Dequeue() as string : null;
				Assert.AreEqual( expected, actual );
            }
        }

        /// <summary>
        /// Clears any unverified events.
        /// </summary>
        public static void Clear()
        {
            _events.Clear();
        }
    }
}

namespace NUnit.TestData.SetupFixture
{
    namespace Namespace1
    {
        #region SomeTestFixture
        [TestFixture]
        public class SomeTestFixture
        {
            [TestFixtureSetUp]
            public void FixtureSetup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureSetup");
            }

            [SetUp]
            public void Setup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Setup");
            }

            [Test]
            public void Test()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Test");
            }

            [TearDown]
            public void TearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("TearDown");
            }

            [TestFixtureTearDown]
            public void FixtureTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureTearDown");
            }
        }
        #endregion SomeTestFixture

        [SetUpFixture]
        public class NUnitNamespaceSetUpFixture
        {
            [SetUp]
            public void DoNamespaceSetUp()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceSetup");
            }

            [TearDown]
            public void DoNamespaceTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceTearDown");
            }
        }
    }

    namespace Namespace2
    {

        #region SomeTestFixture
        /// <summary>
        /// Summary description for SetUpFixtureTests.
        /// </summary>
        [TestFixture]
        public class SomeTestFixture
        {


            [TestFixtureSetUp]
            public void FixtureSetup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureSetup");
            }

            [SetUp]
            public void Setup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Setup");
            }

            [Test]
            public void Test()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Test");
            }

            [TearDown]
            public void TearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("TearDown");
            }

            [TestFixtureTearDown]
            public void FixtureTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureTearDown");
            }
        }
        #endregion SomeTestFixture

        #region SomeTestFixture2
        [TestFixture]
        public class SomeTestFixture2
        {


            [TestFixtureSetUp]
            public void FixtureSetup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureSetup");
            }

            [SetUp]
            public void Setup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Setup");
            }

            [Test]
            public void Test()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Test");
            }

            [TearDown]
            public void TearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("TearDown");
            }

            [TestFixtureTearDown]
            public void FixtureTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureTearDown");
            }
        }
        #endregion SomeTestFixture2

        [SetUpFixture]
        public class NUnitNamespaceSetUpFixture
        {
            [SetUp]
            public void DoNamespaceSetUp()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceSetup");
            }

            [TearDown]
            public void DoNamespaceTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceTearDown");
            }
        }
    }

    namespace Namespace3
    {
        namespace SubNamespace
        {


            #region SomeTestFixture
            [TestFixture]
            public class SomeTestFixture
            {
                [TestFixtureSetUp]
                public void FixtureSetup()
                {
                    TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureSetup");
                }

                [SetUp]
                public void Setup()
                {
                    TestUtilities.SimpleEventRecorder.RegisterEvent("Setup");
                }

                [Test]
                public void Test()
                {
                    TestUtilities.SimpleEventRecorder.RegisterEvent("Test");
                }

                [TearDown]
                public void TearDown()
                {
                    TestUtilities.SimpleEventRecorder.RegisterEvent("TearDown");
                }

                [TestFixtureTearDown]
                public void FixtureTearDown()
                {
                    TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureTearDown");
                }
            }
            #endregion SomeTestFixture

            [SetUpFixture]
            public class NUnitNamespaceSetUpFixture
            {
                [SetUp]
                public void DoNamespaceSetUp()
                {
                    TestUtilities.SimpleEventRecorder.RegisterEvent("SubNamespaceSetup");
                }

                [TearDown]
                public void DoNamespaceTearDown()
                {
                    TestUtilities.SimpleEventRecorder.RegisterEvent("SubNamespaceTearDown");
                }
            }

        }


        #region SomeTestFixture
        [TestFixture]
        public class SomeTestFixture
        {
            [TestFixtureSetUp]
            public void FixtureSetup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureSetup");
            }

            [SetUp]
            public void Setup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Setup");
            }

            [Test]
            public void Test()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Test");
            }

            [TearDown]
            public void TearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("TearDown");
            }

            [TestFixtureTearDown]
            public void FixtureTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureTearDown");
            }
        }
        #endregion SomeTestFixture

        [SetUpFixture]
        public class NUnitNamespaceSetUpFixture
        {
            [SetUp]
            public static void DoNamespaceSetUp()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceSetup");
            }

            [TearDown]
            public void DoNamespaceTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceTearDown");
            }
        }
    }

    namespace Namespace4
    {
        #region SomeTestFixture
        [TestFixture]
        public class SomeTestFixture
        {
            [TestFixtureSetUp]
            public void FixtureSetup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureSetup");
            }

            [SetUp]
            public void Setup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Setup");
            }

            [Test]
            public void Test()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Test");
            }

            [TearDown]
            public void TearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("TearDown");
            }

            [TestFixtureTearDown]
            public void FixtureTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureTearDown");
            }
        }
        #endregion SomeTestFixture

        [SetUpFixture]
        public class NUnitNamespaceSetUpFixture
        {
            [SetUp]
            public void DoNamespaceSetUp()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceSetup");
            }

            [TearDown]
            public void DoNamespaceTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceTearDown");
            }
        }

        [SetUpFixture]
        public class NUnitNamespaceSetUpFixture2
        {
            [SetUp]
            public void DoNamespaceSetUp()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceSetup2");
            }

            [TearDown]
            public void DoNamespaceTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceTearDown2");
            }
        }
    }

	namespace Namespace5
	{
		[SetUpFixture]
		public class CurrentDirectoryRecordingSetUpFixture
		{
			[SetUp]
			public void DoSetUp()
			{
				TestUtilities.SimpleEventRecorder.RegisterEvent("SetUp:" + Environment.CurrentDirectory);
			}

			[TearDown]
			public void DoTearDown()
			{
				TestUtilities.SimpleEventRecorder.RegisterEvent("TearDown:" + Environment.CurrentDirectory);
			}
		}

		[TestFixture]
		public class SomeFixture
		{
			[Test]
			public void SomeMethod() { }				
		}
	}

    namespace Namespace5
    {
        #region SomeTestFixture
        [TestFixture]
        public class SomeTestFixture
        {
            [TestFixtureSetUp]
            public void FixtureSetup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureSetup");
            }

            [SetUp]
            public void Setup()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Setup");
            }

            [Test]
            public void Test()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("Test");
            }

            [TearDown]
            public void TearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("TearDown");
            }

            [TestFixtureTearDown]
            public void FixtureTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("FixtureTearDown");
            }
        }
        #endregion SomeTestFixture

        [SetUpFixture]
        public class NUnitNamespaceSetUpFixture
        {
            [SetUp]
            public static void DoNamespaceSetUp()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceSetup");
            }

            [TearDown]
            public static void DoNamespaceTearDown()
            {
                TestUtilities.SimpleEventRecorder.RegisterEvent("NamespaceTearDown");
            }
        }
    }
}
#region NoNamespaceSetupFixture
[SetUpFixture]
public class NoNamespaceSetupFixture
{
    [SetUp]
    public void DoNamespaceSetUp()
    {
        NUnit.TestUtilities.SimpleEventRecorder.RegisterEvent("RootNamespaceSetup");
    }

    [TearDown]
    public void DoNamespaceTearDown()
    {
        NUnit.TestUtilities.SimpleEventRecorder.RegisterEvent("RootNamespaceTearDown");
    }
}

[TestFixture]
public class SomeTestFixture
{
    [Test]
    public void Test()
    {
        NUnit.TestUtilities.SimpleEventRecorder.RegisterEvent("Test");
    }
}
#endregion NoNamespaceSetupFixture
