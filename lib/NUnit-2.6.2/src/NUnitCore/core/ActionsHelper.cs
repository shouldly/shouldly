// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace NUnit.Core
{
    internal class ActionsHelper
    {
        private static Type _ActionInterfaceType = null;

        static ActionsHelper()
        {
            _ActionInterfaceType = Type.GetType(NUnitFramework.TestActionInterface);
        }

        public static void ExecuteActions(ActionPhase phase, IEnumerable<TestAction> actions, ITest test)
        {
            if (actions == null)
                return;

            TestAction[] filteredActions = GetFilteredAndSortedActions(actions, phase);

            foreach (TestAction action in filteredActions)
            {
                if(phase == ActionPhase.Before)
                    action.ExecuteBefore(test);
                else
                    action.ExecuteAfter(test);
            }
        }

        public static TestAction[] GetActionsFromAttributeProvider(ICustomAttributeProvider attributeProvider)
        {
            if (attributeProvider == null || _ActionInterfaceType == null)
                return new TestAction[0];

            object[] targets = attributeProvider.GetCustomAttributes(_ActionInterfaceType, false);

            List<TestAction> actions = new List<TestAction>();

            foreach (var target in targets)
                actions.Add(new TestAction(target));

            actions.Sort(SortByTargetDescending);

            return actions.ToArray();
        }

        public static TestAction[] GetActionsFromTypesAttributes(Type type)
        {
            if(type == null)
                return new TestAction[0];

            if(type == typeof(object))
                return new TestAction[0];

            List<TestAction> actions = new List<TestAction>();

            actions.AddRange(GetActionsFromTypesAttributes(type.BaseType));

            Type[] declaredInterfaces = GetDeclaredInterfaces(type);

            foreach(Type interfaceType in declaredInterfaces)
                actions.AddRange(GetActionsFromAttributeProvider(interfaceType));

            actions.AddRange(GetActionsFromAttributeProvider(type));

            return actions.ToArray();
        }

        private static Type[] GetDeclaredInterfaces(Type type)
        {
            List<Type> interfaces = new List<Type>(type.GetInterfaces());

            if (type.BaseType == typeof(object))
                return interfaces.ToArray();

            List<Type> baseInterfaces = new List<Type>(type.BaseType.GetInterfaces());
            List<Type> declaredInterfaces = new List<Type>();

            foreach (Type interfaceType in interfaces)
            {
                if (!baseInterfaces.Contains(interfaceType))
                    declaredInterfaces.Add(interfaceType);
            }

            return declaredInterfaces.ToArray();
        }

        private static TestAction[] GetFilteredAndSortedActions(IEnumerable<TestAction> actions, ActionPhase phase)
        {
            List<TestAction> filteredActions = new List<TestAction>();
            foreach (TestAction actionItem in actions)
            {
                if (filteredActions.Contains(actionItem) != true)
                    filteredActions.Add(actionItem);
            }

            if(phase == ActionPhase.After)
                filteredActions.Reverse();

            return filteredActions.ToArray();
        }

        private static int SortByTargetDescending(TestAction x, TestAction y)
        {
            return y.Targets.CompareTo(x.Targets);
        }
    }

    public enum ActionPhase
    {
        Before,
        After
    }
}
#endif
