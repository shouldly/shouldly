// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics;
using System.Reflection;
using NUnit.TestData.ActionAttributeTests;

[assembly: SampleAction("AssemblySuite", ActionTargets.Suite)]
[assembly: SampleAction("AssemblyTest", ActionTargets.Test)]
[assembly: SampleAction("AssemblySite")]

namespace NUnit.TestData.ActionAttributeTests
{
    [SetUpFixture]
    [SampleAction("SetupFixtureSuite", ActionTargets.Suite)]
    [SampleAction("SetupFixtureTest", ActionTargets.Test)]
    [SampleAction("SetupFixtureSite")]
    public class SetupFixture : BaseSetupFixture
    {
    }

    [SampleAction("BaseSetupFixtureSuite", ActionTargets.Suite)]
    [SampleAction("BaseSetupFixtureTest", ActionTargets.Test)]
    [SampleAction("BaseSetupFixtureSite")]
    public abstract class BaseSetupFixture
    {
    }

    [TestFixture]
    [SampleAction("FixtureSuite", ActionTargets.Suite)]
    [SampleAction("FixtureTest", ActionTargets.Test)]
    [SampleAction("FixtureSite")]
    public class ActionAttributeFixture : BaseActionAttributeFixture, IWithAction
    {
        private static List<string> _Results = null;
        public static List<string> Results
        {
            get { return _Results; }
            set { _Results = value; }
        }

        List<string> IWithAction.Results { get { return Results; } }

        // NOTE: Both test cases use the same message because
        // order of execution is indeterminate.
        [Test, TestCase("SomeTest"), TestCase("SomeTest")]
        [SampleAction("ParameterizedSuite", ActionTargets.Suite)]
        [SampleAction("ParameterizedTest", ActionTargets.Test)]
        [SampleAction("ParameterizedSite")]
        public void SomeTest(string message)
        {
            ((IWithAction)this).Results.Add(message);
        }

        [Test]
        [SampleAction("MethodSuite", ActionTargets.Suite)] // should never get invoked
        [SampleAction("MethodTest", ActionTargets.Test)]
        [SampleAction("MethodSite")]
        public void SomeTestNotParameterized()
        {
            ((IWithAction)this).Results.Add("SomeTestNotParameterized");
        }
    }

    [SampleAction("BaseFixtureSuite", ActionTargets.Suite)]
    [SampleAction("BaseFixtureTest", ActionTargets.Test)]
    [SampleAction("BaseFixtureSite")]
    public abstract class BaseActionAttributeFixture : IBaseWithAction
    {
    }

    [SampleAction("InterfaceSuite", ActionTargets.Suite)]
    [SampleAction("InterfaceTest", ActionTargets.Test)]
    [SampleAction("InterfaceSite")]
    public interface IWithAction
    {
        List<string> Results { get; }
    }

    [SampleAction("BaseInterfaceSuite", ActionTargets.Suite)]
    [SampleAction("BaseInterfaceTest", ActionTargets.Test)]
    [SampleAction("BaseInterfaceSite")]
    public interface IBaseWithAction
    {
    }

    public class SampleActionAttribute : TestActionAttribute
    {
        private readonly string _Prefix = null;
        private readonly ActionTargets _Targets = ActionTargets.Default;

        public SampleActionAttribute(string prefix)
        {
            _Prefix = prefix;
        }

        public SampleActionAttribute(string prefix, ActionTargets targets)
        {
            _Prefix = prefix;
            _Targets = targets;
        }

        public override void BeforeTest(TestDetails testDetails)
        {
            AddResult("Before", testDetails);
        }

        public override void AfterTest(TestDetails testDetails)
        {
            AddResult("After", testDetails);
        }

        public override ActionTargets Targets
        {
            get { return _Targets; }
        }

        private void AddResult(string phase, TestDetails testDetails)
        {
            string message = string.Format("{0}.{1}.{2}.{3}",
                                           _Prefix,
                                           phase,
                                           testDetails.Fixture != null ? "true" : "false",
                                           testDetails.Method != null ? "true" : "false");

            if(ActionAttributeFixture.Results != null)
                ActionAttributeFixture.Results.Add(message);
        }
    }
}
#endif
