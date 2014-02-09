// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

//#define DEFAULT_APPLIES_TO_TESTCASE
#if CLR_2_0 || CLR_4_0
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.TestData.ActionAttributeTests;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class ActionAttributeTests
    {
        private class ActionAttributeFixtureFilter : TestFilter
        {
            public override bool Match(ITest test)
            {
                return test.TestName.FullName.StartsWith(typeof(ActionAttributeFixture).FullName);
            }
        }

        private TestResult _result = null;
        private readonly string[] _suiteSites = new string[]
        {
            "Assembly",
            "BaseSetupFixture",
            "SetupFixture",
            "BaseInterface",
            "BaseFixture",
            "Interface",
            "Fixture"
        };

        private readonly string[] _parameterizedTestOutput = new string[]
        {
            "SomeTest-Case1",
            "SomeTest-Case2"
        };

        private readonly string[] _testOutput = new string[]
        {
            "SomeTestNotParameterized",
        };

        [TestFixtureSetUp]
        public void Setup()
        {
            ActionAttributeFixture.Results = new List<string>();

            TestSuiteBuilder builder = new TestSuiteBuilder();
            TestPackage package = new TestPackage(AssemblyHelper.GetAssemblyPath(typeof(ActionAttributeFixture)));
            package.TestName = typeof(ActionAttributeFixture).Namespace;

            Test suite = builder.Build(package);
            _result = suite.Run(new NullListener(), new ActionAttributeFixtureFilter());
        }

        [Test]
        public void TestsRunsSuccessfully()
        {
            Assert.IsTrue(_result.IsSuccess, "Test run was not successful.");

            Console.WriteLine("{prefix}.{phase}.{hasFixture}.{hasMethod}");            
            foreach(string message in ActionAttributeFixture.Results)
                Console.WriteLine(message);
        }

        private void AssertResultEquals(List<string> input, int index, string expected)
        {
            Assert.IsTrue(input[index].Equals(expected), string.Format("Did not find '{0}' at index {1}; instead '{2}'", expected, index, input[index]));
        }

        [Test]
        public void ExpectedOutput_InCorrectOrder()
        {
            string[] expectedResults = new string[] {
                "AssemblySuite.Before.false.false",
                "AssemblySite.Before.false.false",
                "BaseSetupFixtureSuite.Before.true.false",
                "BaseSetupFixtureSite.Before.true.false",
                "SetupFixtureSuite.Before.true.false",
                "SetupFixtureSite.Before.true.false",
                "BaseInterfaceSuite.Before.true.false",
                "BaseInterfaceSite.Before.true.false",
                "BaseFixtureSuite.Before.true.false",
                "BaseFixtureSite.Before.true.false",
                "InterfaceSuite.Before.true.false",
                "InterfaceSite.Before.true.false",
                "FixtureSuite.Before.true.false",
                "FixtureSite.Before.true.false",
                "ParameterizedSuite.Before.true.false",
#if DEFAULT_APPLIES_TO_TESTCASE
                "ParameterizedSite.Before.true.false",
#endif
                "AssemblyTest.Before.true.true",
                "BaseSetupFixtureTest.Before.true.true",
                "SetupFixtureTest.Before.true.true",
                "BaseInterfaceTest.Before.true.true",
                "BaseFixtureTest.Before.true.true",
                "InterfaceTest.Before.true.true",
                "FixtureTest.Before.true.true",
                "ParameterizedTest.Before.true.true",
#if !DEFAULT_APPLIES_TO_TESTCASE
                "ParameterizedSite.Before.true.true",
#endif
                "SomeTest",
#if !DEFAULT_APPLIES_TO_TESTCASE
                "ParameterizedSite.After.true.true",
#endif
                "ParameterizedTest.After.true.true",
                "FixtureTest.After.true.true",
                "InterfaceTest.After.true.true",
                "BaseFixtureTest.After.true.true",
                "BaseInterfaceTest.After.true.true",
                "SetupFixtureTest.After.true.true",
                "BaseSetupFixtureTest.After.true.true",
                "AssemblyTest.After.true.true",
                "AssemblyTest.Before.true.true",
                "BaseSetupFixtureTest.Before.true.true",
                "SetupFixtureTest.Before.true.true",
                "BaseInterfaceTest.Before.true.true",
                "BaseFixtureTest.Before.true.true",
                "InterfaceTest.Before.true.true",
                "FixtureTest.Before.true.true",
                "ParameterizedTest.Before.true.true",
#if !DEFAULT_APPLIES_TO_TESTCASE
                "ParameterizedSite.Before.true.true",
#endif
                "SomeTest",
#if !DEFAULT_APPLIES_TO_TESTCASE
                "ParameterizedSite.After.true.true",
#endif
                "ParameterizedTest.After.true.true",
                "FixtureTest.After.true.true",
                "InterfaceTest.After.true.true",
                "BaseFixtureTest.After.true.true",
                "BaseInterfaceTest.After.true.true",
                "SetupFixtureTest.After.true.true",
                "BaseSetupFixtureTest.After.true.true",
                "AssemblyTest.After.true.true",
#if DEFAULT_APPLIES_TO_TESTCASE
                "ParameterizedSite.After.true.false",
#endif
                "ParameterizedSuite.After.true.false",
                "AssemblyTest.Before.true.true",
                "BaseSetupFixtureTest.Before.true.true",
                "SetupFixtureTest.Before.true.true",
                "BaseInterfaceTest.Before.true.true",
                "BaseFixtureTest.Before.true.true",
                "InterfaceTest.Before.true.true",
                "FixtureTest.Before.true.true",
                "MethodTest.Before.true.true",
                "MethodSite.Before.true.true",
                "SomeTestNotParameterized",
                "MethodSite.After.true.true",
                "MethodTest.After.true.true",
                "FixtureTest.After.true.true",
                "InterfaceTest.After.true.true",
                "BaseFixtureTest.After.true.true",
                "BaseInterfaceTest.After.true.true",
                "SetupFixtureTest.After.true.true",
                "BaseSetupFixtureTest.After.true.true",
                "AssemblyTest.After.true.true",
                "FixtureSite.After.true.false",
                "FixtureSuite.After.true.false",
                "InterfaceSite.After.true.false",
                "InterfaceSuite.After.true.false",
                "BaseFixtureSite.After.true.false",
                "BaseFixtureSuite.After.true.false",
                "BaseInterfaceSite.After.true.false",
                "BaseInterfaceSuite.After.true.false",
                "SetupFixtureSite.After.true.false",
                "SetupFixtureSuite.After.true.false",
                "BaseSetupFixtureSite.After.true.false",
                "BaseSetupFixtureSuite.After.true.false",
                "AssemblySite.After.false.false",
                "AssemblySuite.After.false.false"
            };

            Assert.AreEqual(expectedResults, ActionAttributeFixture.Results);
        }
    }
}
#endif