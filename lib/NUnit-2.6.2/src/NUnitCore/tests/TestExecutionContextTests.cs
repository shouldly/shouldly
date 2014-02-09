// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Security.Principal;
using System.Threading;
using System.Globalization;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	/// <summary>
    /// Summary description for TestExecutionContextTests.
	/// </summary>
	[TestFixture][Property("Question", "Why?")]
    public class TestExecutionContextTests
	{
        TestExecutionContext fixtureContext;
        TestExecutionContext setupContext;

		string currentDirectory;
		CultureInfo currentCulture;
        CultureInfo currentUICulture;
        IPrincipal currentPrincipal;

        [TestFixtureSetUp]
        public void OneTimeSetUp()
        {
            fixtureContext = TestExecutionContext.CurrentContext;
        }

        [TestFixtureTearDown]
        public void OneTimeTearDown()
        {
            // TODO: We put some tests in one time teardown to verify that
            // the context is still valid. It would be better if these tests
            // were placed in a second-level test, invoked from this test class.
            TestExecutionContext ec = TestExecutionContext.CurrentContext;
            Assert.That(ec.CurrentTest.TestName.Name, Is.EqualTo("TestExecutionContextTests"));
            Assert.That(ec.CurrentTest.TestName.FullName,
                Is.EqualTo("NUnit.Core.Tests.TestExecutionContextTests"));
            Assert.That(ec.CurrentTest.Properties["Question"], Is.EqualTo("Why?"));
        }

        /// <summary>
		/// Since we are testing the mechanism that saves and
		/// restores contexts, we save manually here
		/// </summary>
		[SetUp]
		public void SaveContext()
		{
            setupContext = TestExecutionContext.CurrentContext;

			currentDirectory = Environment.CurrentDirectory;
			currentCulture = CultureInfo.CurrentCulture;
            currentUICulture = CultureInfo.CurrentUICulture;
            currentPrincipal = Thread.CurrentPrincipal;
		}

		[TearDown]
		public void RestoreContext()
		{
			Environment.CurrentDirectory = currentDirectory;
			Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentUICulture;
            Thread.CurrentPrincipal = currentPrincipal;

            Assert.That(
                TestExecutionContext.CurrentContext.CurrentTest.TestName.FullName,
                Is.EqualTo(setupContext.CurrentTest.TestName.FullName),
                "Context at TearDown failed to match that saved from SetUp");
        }

        [Test]
        public void FixtureSetUpCanAccessFixtureName()
        {
            Assert.That(fixtureContext.CurrentTest.TestName.Name, Is.EqualTo("TestExecutionContextTests"));
        }

        [Test]
        public void FixtureSetUpCanAccessFixtureFullName()
        {
            Assert.That(fixtureContext.CurrentTest.TestName.FullName,
                Is.EqualTo("NUnit.Core.Tests.TestExecutionContextTests"));
        }

        [Test]
        public void FixtureSetUpCanAccessFixtureProperties()
        {
            Assert.That(fixtureContext.CurrentTest.Properties["Question"], Is.EqualTo("Why?"));
        }

        [Test]
        public void SetUpCanAccessTestName()
        {
            Assert.That(setupContext.CurrentTest.TestName.Name, Is.EqualTo("SetUpCanAccessTestName"));
        }

        [Test]
        public void SetUpCanAccessTestFullName()
        {
            Assert.That(setupContext.CurrentTest.TestName.FullName,
                Is.EqualTo("NUnit.Core.Tests.TestExecutionContextTests.SetUpCanAccessTestFullName"));
        }

        [Test]
        [Property("Answer", 42)]
        public void SetUpCanAccessTestProperties()
        {
            Assert.That(setupContext.CurrentTest.Properties["Answer"], Is.EqualTo(42));
        }

        [Test]
        public void TestCanAccessItsOwnName()
        {
            Assert.That(TestExecutionContext.CurrentContext.CurrentTest.TestName.Name, Is.EqualTo("TestCanAccessItsOwnName"));
        }

        [Test]
        public void TestCanAccessItsOwnFullName()
        {
            Assert.That(TestExecutionContext.CurrentContext.CurrentTest.TestName.FullName,
                Is.EqualTo("NUnit.Core.Tests.TestExecutionContextTests.TestCanAccessItsOwnFullName"));
        }

        [Test]
        [Property("Answer", 42)]
        public void TestCanAccessItsOwnProperties()
        {
            Assert.That(TestExecutionContext.CurrentContext.CurrentTest.Properties["Answer"], Is.EqualTo(42));
        }

        [Test]
		public void SetAndRestoreCurrentDirectory()
		{
            Assert.AreEqual(currentDirectory, TestExecutionContext.CurrentContext.CurrentDirectory, "Directory not in initial context");

            TestExecutionContext.Save();

            try
            {
                string otherDirectory = System.IO.Path.GetTempPath();
                if (otherDirectory[otherDirectory.Length - 1] == System.IO.Path.DirectorySeparatorChar)
                    otherDirectory = otherDirectory.Substring(0, otherDirectory.Length - 1);
                TestExecutionContext.CurrentContext.CurrentDirectory = otherDirectory;
                Assert.AreEqual(otherDirectory, Environment.CurrentDirectory, "Directory was not set");
                Assert.AreEqual(otherDirectory, TestExecutionContext.CurrentContext.CurrentDirectory, "Directory not in new context");
            }
            finally
            {
                TestExecutionContext.Restore();
            }

			Assert.AreEqual( currentDirectory, Environment.CurrentDirectory, "Directory was not restored" );
            Assert.AreEqual(currentDirectory, TestExecutionContext.CurrentContext.CurrentDirectory, "Directory not in final context");
		}
		
		[Test]
		public void SetAndRestoreCurrentCulture()
		{
            Assert.AreEqual(currentCulture, TestExecutionContext.CurrentContext.CurrentCulture, "Culture not in initial context");

            TestExecutionContext.Save();

            try
            {
                CultureInfo otherCulture =
                    new CultureInfo(currentCulture.Name == "fr-FR" ? "en-GB" : "fr-FR");
                TestExecutionContext.CurrentContext.CurrentCulture = otherCulture;
                Assert.AreEqual(otherCulture, CultureInfo.CurrentCulture, "Culture was not set");
                Assert.AreEqual(otherCulture, TestExecutionContext.CurrentContext.CurrentCulture, "Culture not in new context");
            }
            finally
            {
                TestExecutionContext.Restore();
            }

			Assert.AreEqual( currentCulture, CultureInfo.CurrentCulture, "Culture was not restored" );
            Assert.AreEqual(currentCulture, TestExecutionContext.CurrentContext.CurrentCulture, "Culture not in final context");
		}

        [Test]
        public void SetAndRestoreCurrentUICulture()
        {
            Assert.AreEqual(currentUICulture, TestExecutionContext.CurrentContext.CurrentUICulture, "UICulture not in initial context");

            TestExecutionContext.Save();

            try
            {
                CultureInfo otherCulture =
                    new CultureInfo(currentUICulture.Name == "fr-FR" ? "en-GB" : "fr-FR");
                TestExecutionContext.CurrentContext.CurrentUICulture = otherCulture;
                Assert.AreEqual(otherCulture, CultureInfo.CurrentUICulture, "UICulture was not set");
                Assert.AreEqual(otherCulture, TestExecutionContext.CurrentContext.CurrentUICulture, "UICulture not in new context");
            }
            finally
            {
                TestExecutionContext.Restore();
            }

            Assert.AreEqual(currentUICulture, CultureInfo.CurrentUICulture, "UICulture was not restored");
            Assert.AreEqual(currentUICulture, TestExecutionContext.CurrentContext.CurrentUICulture, "UICulture not in final context");
        }

        [Test]
        public void SetAndRestoreCurrentPrincipal()
        {
            Assert.AreEqual(currentPrincipal, TestExecutionContext.CurrentContext.CurrentPrincipal, "Principal not in initial context");

            TestExecutionContext.Save();

            try
            {
                GenericIdentity identity = new GenericIdentity("foo");
                TestExecutionContext.CurrentContext.CurrentPrincipal = new GenericPrincipal(identity, new string[0]);
                Assert.AreEqual("foo", Thread.CurrentPrincipal.Identity.Name, "Principal was not set");
                Assert.AreEqual("foo", TestExecutionContext.CurrentContext.CurrentPrincipal.Identity.Name, "Principal not in new context");
            }
            finally
            {
                TestExecutionContext.Restore();
            }

            Assert.AreEqual(currentPrincipal, Thread.CurrentPrincipal, "Principal was not restored");
            Assert.AreEqual(currentPrincipal, TestExecutionContext.CurrentContext.CurrentPrincipal, "Principal not in final context");
        }

        [Test, Explicit("Run this test manually with PrincipalPolicy set to WindowsPrincipal in the advanced loader settings")]
        public void CanSetPrincipalPolicy()
        {
            Assert.That(Thread.CurrentPrincipal is WindowsPrincipal);
        }
    }
}
