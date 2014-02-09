// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Util;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class SetUpFixtureTests
    {
        private static readonly string testAssembly = AssemblyHelper.GetAssemblyPath(typeof(NUnit.TestData.EmptyFixture));

        #region SetUp
        [SetUp]
        public void SetUp()
        {
            TestUtilities.SimpleEventRecorder.Clear();
        }
        #endregion SetUp

        private TestResult runTests(string nameSpace)
        {
            return runTests(nameSpace, TestFilter.Empty);
        }
        private TestResult runTests(string nameSpace, TestFilter filter)
        {
            TestSuiteBuilder builder = new TestSuiteBuilder();
			TestPackage package = new TestPackage( testAssembly );
			package.TestName = nameSpace;
            Test suite = builder.Build( package );
            return suite.Run(new NullListener(),filter);
        }

        #region Builder
        /// <summary>
        /// Tests that the TestSuiteBuilder correctly interperets a SetupFixture class as a 'virtual namespace' into which 
        /// all it's sibling classes are inserted.
        /// </summary>
        [NUnit.Framework.Test]
        public void NamespaceSetUpFixtureReplacesNamespaceNodeInTree()
        {
            string nameSpace = "NUnit.TestData.SetupFixture.Namespace1";
            TestSuiteBuilder builder = new TestSuiteBuilder();
			TestPackage package = new TestPackage( testAssembly );
			package.TestName = nameSpace;
			Test suite= builder.Build( package );

            Assert.IsNotNull(suite);

            Assert.AreEqual(testAssembly, suite.TestName.Name);
            Assert.AreEqual(1, suite.Tests.Count);

            string[] nameSpaceBits = nameSpace.Split('.');
            for (int i = 0; i < nameSpaceBits.Length; i++)
            {
                suite = suite.Tests[0] as TestSuite;
                Assert.AreEqual(nameSpaceBits[i], suite.TestName.Name);
                Assert.AreEqual(1, suite.Tests.Count);
            }

            Assert.IsInstanceOf(typeof(SetUpFixture), suite);

            suite = suite.Tests[0] as TestSuite;
            Assert.AreEqual("SomeTestFixture", suite.TestName.Name);
            Assert.AreEqual(1, suite.Tests.Count);
        }
        #endregion Builder

        #region NoNamespaceBuilder
        /// <summary>
        /// Tests that the TestSuiteBuilder correctly interperets a SetupFixture class with no parent namespace 
        /// as a 'virtual assembly' into which all it's sibling fixtures are inserted.
        /// </summary>
        [NUnit.Framework.Test]
        public void AssemblySetUpFixtureReplacesAssemblyNodeInTree()
        {
            TestSuiteBuilder builder = new TestSuiteBuilder();
            Test suite = builder.Build( new TestPackage( testAssembly ) );

            Assert.IsNotNull(suite);
            Assert.IsInstanceOf(typeof(SetUpFixture), suite);

            suite = suite.Tests[1] as TestSuite;
            Assert.AreEqual("SomeTestFixture", suite.TestName.Name);
            Assert.AreEqual(1, suite.TestCount);
        }
        #endregion NoNamespaceBuilder


        #region Simple
        [NUnit.Framework.Test]
        public void NamespaceSetUpFixtureWrapsExecutionOfSingleTest()
        {
            Assert.IsTrue(runTests("NUnit.TestData.SetupFixture.Namespace1").IsSuccess);
            TestUtilities.SimpleEventRecorder.Verify("NamespaceSetup",
                                    "FixtureSetup", "Setup", "Test", "TearDown", "FixtureTearDown",
                                  "NamespaceTearDown");
        }
        #endregion Simple

        #region Static
        [Test]
        public void NamespaceSetUpMethodsMayBeStatic()
        {
            Assert.IsTrue(runTests("NUnit.TestData.SetupFixture.Namespace5").IsSuccess);
            TestUtilities.SimpleEventRecorder.Verify("NamespaceSetup",
                                    "FixtureSetup", "Setup", "Test", "TearDown", "FixtureTearDown",
                                  "NamespaceTearDown");
        }
        #endregion

        #region TwoTestFixtures
        [NUnit.Framework.Test]
        public void NamespaceSetUpFixtureWrapsExecutionOfTwoTests()
        {
            Assert.IsTrue(runTests("NUnit.TestData.SetupFixture.Namespace2").IsSuccess);
            TestUtilities.SimpleEventRecorder.Verify("NamespaceSetup",
                                    "FixtureSetup", "Setup", "Test", "TearDown", "FixtureTearDown",
                                    "FixtureSetup", "Setup", "Test", "TearDown", "FixtureTearDown",
                                  "NamespaceTearDown");
        }
        #endregion TwoTestFixtures

        #region SubNamespace
        [NUnit.Framework.Test]
        public void NamespaceSetUpFixtureWrapsNestedNamespaceSetUpFixture()
        {
            Assert.IsTrue(runTests("NUnit.TestData.SetupFixture.Namespace3").IsSuccess);
            TestUtilities.SimpleEventRecorder.Verify("NamespaceSetup",
                                    "FixtureSetup", "Setup", "Test", "TearDown", "FixtureTearDown",
                                    "SubNamespaceSetup",
                                        "FixtureSetup", "Setup", "Test", "TearDown", "FixtureTearDown",
                                    "SubNamespaceTearDown",
                                  "NamespaceTearDown");
        }
        #endregion SubNamespace

        #region TwoSetUpFixtures
        [NUnit.Framework.Test]
        public void WithTwoSetUpFixtuesOnlyOneIsUsed()
        {
            Assert.IsTrue(runTests("NUnit.TestData.SetupFixture.Namespace4").IsSuccess);
            TestUtilities.SimpleEventRecorder.Verify("NamespaceSetup2",
                                    "FixtureSetup", "Setup", "Test", "TearDown", "FixtureTearDown",
                                  "NamespaceTearDown2");
        }
        #endregion TwoSetUpFixtures

        #region NoNamespaceSetupFixture
        [NUnit.Framework.Test]
        public void AssemblySetupFixtureWrapsExecutionOfTest()
        {
            TestResult result = runTests(null, new Filters.SimpleNameFilter("SomeTestFixture"));
            ResultSummarizer summ = new ResultSummarizer(result);
            Assert.AreEqual(1, summ.TestsRun);
            Assert.IsTrue(result.IsSuccess);
            TestUtilities.SimpleEventRecorder.Verify("RootNamespaceSetup",
                                    "Test",
                                  "RootNamespaceTearDown");
        }
        #endregion NoNamespaceSetupFixture
    }
}












