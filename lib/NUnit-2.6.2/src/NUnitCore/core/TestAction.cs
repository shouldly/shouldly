// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NUnit.Core
{
    public class TestAction
    {
        public static readonly int TargetsDefault = 0;
        public static readonly int TargetsTest = 1;
        public static readonly int TargetsSuite = 2;

        private static readonly Type _ActionInterfaceType = null;
        private static readonly Type _TestDetailsClassType = null;

        static TestAction()
        {
            _ActionInterfaceType = Type.GetType(NUnitFramework.TestActionInterface);
            _TestDetailsClassType = Type.GetType(NUnitFramework.TestDetailsClass);
        }

        private readonly object _Action;
        private readonly int _Targets;

        public TestAction(object action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            _Action = action;
            _Targets = (int) Reflect.GetPropertyValue(action, "Targets");
        }

        public void ExecuteBefore(ITest test)
        {
            Execute(test, "Before");
        }

        public void ExecuteAfter(ITest test)
        {
            Execute(test, "After");
        }

        private void Execute(ITest test, string methodPrefix)
        {
            var method = Reflect.GetNamedMethod(_ActionInterfaceType, methodPrefix + "Test");
            var details = CreateTestDetails(test);

            Reflect.InvokeMethod(method, _Action, details);
        }

        private static object CreateTestDetails(ITest test)
        {
            object fixture = null;
            MethodInfo method = null;

            var testMethod = test as TestMethod;
            if (testMethod != null)
                method = testMethod.Method;

            var testObject = test as Test;
            if(testObject != null)
                fixture = testObject.Fixture;

            return Activator.CreateInstance(_TestDetailsClassType,
                                            fixture,
                                            method,
                                            test.TestName.FullName,
                                            test.TestType,
                                            test.IsSuite);
        }

        public bool DoesTarget(int target)
        {
            if(target < 0)
                throw new ArgumentOutOfRangeException("target", "Target must be a positive integer.");

            if(target == 0)
                return _Targets == 0;

            uint self = Convert.ToUInt32(target);
            return (_Targets & self) == self;
        }

        public int Targets { get { return _Targets; } }
    }
}
#endif