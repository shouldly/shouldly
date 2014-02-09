// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Reflection;
using System.Text;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Builders
{
    public class TestCaseParameterProvider : ITestCaseProvider 
    {
        /// <summary>
        /// Determine whether any test cases are available for a parameterized method.
        /// </summary>
        /// <param name="method">A MethodInfo representing a parameterized test</param>
        /// <returns>True if any cases are available, otherwise false.</returns>
        public bool HasTestCasesFor(MethodInfo method)
        {
            return Reflect.HasAttribute(method, NUnitFramework.TestCaseAttribute, false);
        }

        /// <summary>
        /// Return an IEnumerable providing test cases for use in
        /// running a parameterized test.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public IEnumerable GetTestCasesFor(MethodInfo method)
        {
			ArrayList list = new ArrayList();

            Attribute[] attrs = Reflect.GetAttributes(method, NUnitFramework.TestCaseAttribute, false);

            ParameterInfo[] parameters = method.GetParameters();
            int argsNeeded = parameters.Length;

            foreach (Attribute attr in attrs)
            {
                ParameterSet parms;

                try
                {
                    parms = ParameterSet.FromDataSource(attr);
                    int argsProvided = parms.Arguments.Length;

                    // Special handling for params arguments
                    if (argsNeeded > 0 && argsProvided >= argsNeeded - 1)
                    {
                        ParameterInfo lastParameter = parameters[argsNeeded - 1];
                        Type lastParameterType = lastParameter.ParameterType;
                        Type elementType = lastParameterType.GetElementType();
                        
                        if (lastParameterType.IsArray && lastParameter.IsDefined(typeof(ParamArrayAttribute), false))
                        {
                            if (argsProvided == argsNeeded)
                            {
                                Type lastArgumentType = parms.Arguments[argsProvided - 1].GetType();
                                if (!lastParameterType.IsAssignableFrom(lastArgumentType))
                                {
                                    Array array = Array.CreateInstance(elementType, 1);
                                    array.SetValue(parms.Arguments[argsProvided - 1], 0);
                                    parms.Arguments[argsProvided - 1] = array;
                                }
                            }
                            else
                            {
                                object[] newArglist = new object[argsNeeded];
                                for (int i = 0; i < argsNeeded && i < argsProvided; i++)
                                    newArglist[i] = parms.Arguments[i];

                                int length = argsProvided - argsNeeded + 1;
                                Array array = Array.CreateInstance(elementType, length);
                                for (int i = 0; i < length; i++)
                                    array.SetValue(parms.Arguments[argsNeeded + i - 1], i);

                                newArglist[argsNeeded - 1] = array;
                                parms.Arguments = newArglist;
                                argsProvided = argsNeeded;
                            }
                        }
                    }

                    //if (method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(object[]))
                    //    parms.Arguments = new object[]{parms.Arguments};

                    // Special handling when sole argument is an object[]
                    if (argsNeeded == 1 && method.GetParameters()[0].ParameterType == typeof(object[]))
                    {
                        if (argsProvided > 1 ||
                            argsProvided == 1 && parms.Arguments[0].GetType() != typeof(object[]))
                        {
                            parms.Arguments = new object[] { parms.Arguments };
                        }
                    }


                    if (argsProvided == argsNeeded)
                        PerformSpecialConversions(parms.Arguments, parameters);
                }
                catch (Exception ex)
                {
                    parms = new ParameterSet( ex );
                }

                list.Add( parms );
			}

			return list;
        }
        
        /// <summary>
        /// Performs several special conversions allowed by NUnit in order to
        /// permit arguments with types that cannot be used in the constructor
        /// of an Attribute such as TestCaseAttribute or to simplify their use.
        /// </summary>
        /// <param name="arglist">The arguments to be converted</param>
        /// <param name="parameters">The ParameterInfo array for the method</param>
        private static void PerformSpecialConversions(object[] arglist, ParameterInfo[] parameters)
        {
            for (int i = 0; i < arglist.Length; i++)
            {
                object arg = arglist[i];
                Type targetType = parameters[i].ParameterType;

                if (arg == null)
                    continue;

                if (arg.GetType().FullName == "NUnit.Framework.SpecialValue" &&
                    arg.ToString() == "Null" )
                {
                    arglist[i] = null;
                    continue;
                }

                if (targetType.IsAssignableFrom(arg.GetType()))
                    continue;

                if (arg is DBNull)
                {
                    arglist[i] = null;
                    continue;
                }

                bool convert = false;

                if (targetType == typeof(short) || targetType == typeof(byte) || targetType == typeof(sbyte))
                    convert = arg is int;
                else
                if (targetType == typeof(decimal))
                    convert = arg is double || arg is string || arg is int;
                else 
                if (targetType == typeof(DateTime) || targetType == typeof(TimeSpan))
                    convert = arg is string;

                if (convert)
                    arglist[i] = Convert.ChangeType(arg, targetType, System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
