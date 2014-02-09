// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
//#define DEFAULT_APPLIES_TO_TESTCASE
using System.Collections;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif
using System.Reflection;
using System.Text;

namespace NUnit.Core
{
    /// <summary>
    /// ParameterizedMethodSuite holds a collection of individual
    /// TestMethods with their arguments applied.
    /// </summary>
    public class ParameterizedMethodSuite : TestSuite
    {
        private bool isTheory;
        private MethodInfo method;

        /// <summary>
        /// Construct from a MethodInfo
        /// </summary>
        /// <param name="method"></param>
        public ParameterizedMethodSuite(MethodInfo method)
            : base(method.ReflectedType.FullName, method.Name)
        {
            this.maintainTestOrder = true;
            this.isTheory = Reflect.HasAttribute(method, NUnitFramework.TheoryAttribute, true);
            this.method = method;
        }

        /// <summary>
        /// Gets a string representing the kind of test
        /// that this object represents, for use in display.
        /// </summary>
        public override string TestType
        {
            get 
            { 
                return this.isTheory
                    ? "Theory"
                    : "ParameterizedTest"; 
            }
        }

        /// <summary>
        /// Override Run, setting Fixture to that of the Parent.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override TestResult Run(EventListener listener, ITestFilter filter)
        {
            if (this.Parent != null)
            {
                this.Fixture = this.Parent.Fixture;
                TestSuite suite = this.Parent as TestSuite;
                if (suite != null)
                {
                    this.setUpMethods = suite.GetSetUpMethods();
                    this.tearDownMethods = suite.GetTearDownMethods();
                }
            }

#if CLR_2_0 || CLR_4_0
            this.actions = ActionsHelper.GetActionsFromAttributeProvider(this.method);
#endif

            // DYNAMIC: Get the parameters, and add the methods here.
            
            TestResult result = base.Run(listener, filter);
			
            if (this.isTheory && result.ResultState == ResultState.Inconclusive)
                result.SetResult(
                    ResultState.Failure,
                    this.TestCount == 0
                        ? "No test cases were provided"
                        : "All test cases were inconclusive",
                    null);

			this.Fixture = null;
			this.setUpMethods = null;
			this.tearDownMethods = null;
#if CLR_2_0 || CLR_4_0
            this.actions = null;
#endif

            return result;
        }

        /// <summary>
        /// Override DoOneTimeSetUp to avoid executing any
        /// TestFixtureSetUp method for this suite
        /// </summary>
        /// <param name="suiteResult"></param>
        protected override void DoOneTimeSetUp(TestResult suiteResult)
        {
        }

        /// <summary>
        /// Override DoOneTimeTearDown to avoid executing any
        /// TestFixtureTearDown method for this suite.
        /// </summary>
        /// <param name="suiteResult"></param>
        protected override void DoOneTimeTearDown(TestResult suiteResult)
        {
        }

#if CLR_2_0 || CLR_4_0
#if !DEFAULT_APPLIES_TO_TESTCASE
        protected override void ExecuteActions(ActionPhase phase)
        {
            List<TestAction> targetActions = new List<TestAction>();

            if (this.actions != null)
            {
                foreach (var action in this.actions)
                {
                    if (action.DoesTarget(TestAction.TargetsSuite))
                        targetActions.Add(action);
                }
            }

            ActionsHelper.ExecuteActions(phase, targetActions, this);
        }
#endif
#endif
    }
}
