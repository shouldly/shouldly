// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;
using System.Collections;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Builders
{
    /// <summary>
    /// ValueSourceProvider supplies data items for individual parameters
    /// from named data sources in the test class or a separate class.
    /// </summary>
    public class ValueSourceProvider : IDataPointProvider2
    {
        #region Constants
        public const string SourcesAttribute = "NUnit.Framework.ValueSourceAttribute";
        public const string SourceTypeProperty = "SourceType";
        public const string SourceNameProperty = "SourceName";
        #endregion

        #region IDataPointProvider Members

        /// <summary>
        /// Determine whether any data sources are available for a parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo test parameter</param>
        /// <returns>True if any data is available, otherwise false.</returns>
        public bool HasDataFor(ParameterInfo parameter)
        {
            return Reflect.HasAttribute(parameter, SourcesAttribute, false);
        }

        /// <summary>
        /// Return an IEnumerable providing test data for use with
        /// one parameter of a parameterized test.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IEnumerable GetDataFor(ParameterInfo parameter)
        {
            return GetDataFor(parameter, null);
        }
        #endregion

        #region IDataPointProvider2 Members

        /// <summary>
        /// Determine whether any data sources are available for a parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo test parameter</param>
        /// <param name="parentSuite">The test suite for which the test is being built</param>
        /// <returns>True if any data is available, otherwise false.</returns>
        public bool HasDataFor(ParameterInfo parameter, Test parentSuite)
        {
            return HasDataFor(parameter);
        }

        /// <summary>
        /// Return an IEnumerable providing test data for use with
        /// one parameter of a parameterized test.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parentSuite">The test suite for which the test is being built</param>
        /// <returns></returns>
        public IEnumerable GetDataFor(ParameterInfo parameter, Test parentSuite)
        {
            ArrayList parameterList = new ArrayList();

            foreach (ProviderReference providerRef in GetSourcesFor(parameter, parentSuite))
            {
                IEnumerable instance = providerRef.GetInstance();
                if (instance != null)
                    foreach (object o in instance)
                        parameterList.Add(o);
            }

            return parameterList;
        }
        #endregion

        #region Helper Methods
        private static IList GetSourcesFor(ParameterInfo parameter, Test parent)
        {
            ArrayList sources = new ArrayList();
            TestFixture parentSuite = parent as TestFixture;

            foreach (Attribute sourceAttr in Reflect.GetAttributes(parameter, SourcesAttribute, false))
            {
                Type sourceType = Reflect.GetPropertyValue(sourceAttr, SourceTypeProperty) as Type;
                string sourceName = Reflect.GetPropertyValue(sourceAttr, SourceNameProperty) as string;

                if (sourceType != null)
                    sources.Add(new ProviderReference(sourceType, sourceName, null));
                else if (parentSuite != null)
                    sources.Add(new ProviderReference(parentSuite.FixtureType, parentSuite.arguments, sourceName, null));
                else
                    sources.Add(new ProviderReference(parameter.Member.ReflectedType, sourceName, null));
            }
            return sources;
        }
        #endregion
    }
}
