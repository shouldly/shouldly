// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;
using System.Collections;

namespace NUnit.Core
{
    /// <summary>
    /// Class to implement an NUnit test fixture
    /// </summary>
    public class NUnitTestFixture : TestFixture
    {
        public NUnitTestFixture(Type fixtureType)
            : this(fixtureType, null) { }

        public NUnitTestFixture(Type fixtureType, object[] arguments)
            : base(fixtureType, arguments)
        {
            this.fixtureSetUpMethods =
                Reflect.GetMethodsWithAttribute(fixtureType, NUnitFramework.FixtureSetUpAttribute, true);
            this.fixtureTearDownMethods =
                Reflect.GetMethodsWithAttribute(fixtureType, NUnitFramework.FixtureTearDownAttribute, true);
            this.setUpMethods = 
                Reflect.GetMethodsWithAttribute(this.FixtureType, NUnitFramework.SetUpAttribute, true);
            this.tearDownMethods = 
                Reflect.GetMethodsWithAttribute(this.FixtureType, NUnitFramework.TearDownAttribute, true);

#if CLR_2_0 || CLR_4_0
            this.actions = ActionsHelper.GetActionsFromTypesAttributes(fixtureType);
#endif
        }

        protected override void DoOneTimeSetUp(TestResult suiteResult)
        {
            base.DoOneTimeSetUp(suiteResult);

			suiteResult.AssertCount = NUnitFramework.Assert.GetAssertCount(); ;
        }

        protected override void DoOneTimeTearDown(TestResult suiteResult)
        {
            base.DoOneTimeTearDown(suiteResult);

			suiteResult.AssertCount += NUnitFramework.Assert.GetAssertCount();
        }
    }
}
