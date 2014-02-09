// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Reflection;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Builders
{
    /// <summary>
    /// TestCaseSourceProvider provides data for methods
    /// annotated with the TestCaseSourceAttribute.
    /// </summary>
    public class TestCaseSourceProvider : ITestCaseProvider2
    {
        #region Constants
        public const string SourceTypeProperty = "SourceType";
        public const string SourceNameProperty = "SourceName";
        #endregion

        #region ITestCaseProvider Members

        /// <summary>
        /// Determine whether any test cases are available for a parameterized method.
        /// </summary>
        /// <param name="method">A MethodInfo representing a parameterized test</param>
        /// <returns>True if any cases are available, otherwise false.</returns>
        public bool HasTestCasesFor(MethodInfo method)
        {
            return Reflect.HasAttribute(method, NUnitFramework.TestCaseSourceAttribute, false);
        }

        /// <summary>
        /// Return an IEnumerable providing test cases for use in
        /// running a parameterized test.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public IEnumerable GetTestCasesFor(MethodInfo method)
        {
            return GetTestCasesFor(method, null);
        }
        #endregion

        #region ITestCaseProvider2 Members

        /// <summary>
        /// Determine whether any test cases are available for a parameterized method.
        /// </summary>
        /// <param name="method">A MethodInfo representing a parameterized test</param>
        /// <returns>True if any cases are available, otherwise false.</returns>
        public bool HasTestCasesFor(MethodInfo method, Test suite)
        {
            return HasTestCasesFor(method);
        }

        /// <summary>
        /// Return an IEnumerable providing test cases for use in
        /// running a parameterized test.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public IEnumerable GetTestCasesFor(MethodInfo method, Test parentSuite)
        {
            ArrayList parameterList = new ArrayList();

            foreach (ProviderReference info in GetSourcesFor(method, parentSuite))
            {
                foreach (object source in info.GetInstance())
                {
                    ParameterSet parms;

                    if (source == null)
                    {
                        parms = new ParameterSet();
                        parms.Arguments = new object[] { null };
                    }
                    else
                        parms = source as ParameterSet;

                    if (parms == null)
                    {
                        Type sourceType = source.GetType();

                        if (sourceType.GetInterface("NUnit.Framework.ITestCaseData") != null ||
                            sourceType.GetInterface("NUnit.Framework.Api.ITestCaseData") != null)
                        {
                            parms = ParameterSet.FromDataSource(source);
                        }
                        else
                        {
                            parms = new ParameterSet();

                            ParameterInfo[] parameters = method.GetParameters();

                            if (parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(sourceType))
                                parms.Arguments = new object[] { source };
                            else if (source is object[])
                                parms.Arguments = (object[])source;
                            else if (source is Array)
                            {
                                Array array = (Array)source;
                                if (array.Rank == 1)
                                {
                                    parms.Arguments = new object[array.Length];
                                    for (int i = 0; i < array.Length; i++)
                                        parms.Arguments[i] = (object)array.GetValue(i);
                                }
                            }
                            else
                                parms.Arguments = new object[] { source };
                        }
                    }

                    if (info.Category != null)
                        foreach (string cat in info.Category.Split(new char[] { ',' }))
                            parms.Categories.Add(cat);

                    parameterList.Add(parms);
                }
            }

            return parameterList;
        }
        #endregion

        #region Helper Methods
        private static IList GetSourcesFor(MethodInfo method, Test parent)
        {
            ArrayList sources = new ArrayList();
            TestFixture parentSuite = parent as TestFixture;

            foreach (Attribute sourceAttr in Reflect.GetAttributes(method, NUnitFramework.TestCaseSourceAttribute, false))
            {
                Type sourceType = Reflect.GetPropertyValue(sourceAttr, SourceTypeProperty) as Type;
                string sourceName = Reflect.GetPropertyValue(sourceAttr, SourceNameProperty) as string;
                string category = Reflect.GetPropertyValue(sourceAttr, "Category") as string;

                if (sourceType == null)
                {
                    if (parentSuite != null)
                        sources.Add(new ProviderReference(parentSuite.FixtureType, parentSuite.arguments, sourceName, category));
                    else
                        sources.Add(new ProviderReference(method.ReflectedType, sourceName, category));
                }
                else
                {
                    sources.Add(new ProviderReference(sourceType, sourceName, category));
                }

            }
            return sources;
        }
        #endregion
    }
}
