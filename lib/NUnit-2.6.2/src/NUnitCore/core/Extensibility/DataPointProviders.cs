// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;
using System.Collections;

namespace NUnit.Core.Extensibility
{
    class DataPointProviders : ExtensionPoint, IDataPointProvider2
    {
        public DataPointProviders(ExtensionHost host)
            : base("DataPointProviders", host) { }

        #region IDataPointProvider Members

        /// <summary>
        /// Determine whether any data is available for a parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo representing one
        /// argument to a parameterized test</param>
        /// <returns>True if any data is available, otherwise false.</returns>
        public bool HasDataFor(ParameterInfo parameter)
        {
            foreach (IDataPointProvider provider in Extensions)
                if (provider.HasDataFor(parameter))
                    return true;

            return false;
        }

        /// <summary>
        /// Return an IEnumerable providing data for use with the
        /// supplied parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo representing one
        /// argument to a parameterized test</param>
        /// <returns>An IEnumerable providing the required data</returns>
        public IEnumerable GetDataFor(ParameterInfo parameter)
        {
            ArrayList list = new ArrayList();

            foreach (IDataPointProvider provider in Extensions)
                if (provider.HasDataFor(parameter))
                    foreach (object o in provider.GetDataFor(parameter))
                        list.Add(o);

            return list;
        }
        #endregion

        #region IDataPointProvider2 Members

        /// <summary>
        /// Determine whether any data is available for a parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo representing one
        /// argument to a parameterized test</param>
        /// <returns>True if any data is available, otherwise false.</returns>
        public bool HasDataFor(ParameterInfo parameter, Test suite)
        {
            foreach (IDataPointProvider provider in Extensions)
            {
                if (provider is IDataPointProvider2)
                {
                    IDataPointProvider2 provider2 = (IDataPointProvider2)provider;
                    if (provider2.HasDataFor(parameter, suite))
                        return true;
                }
                else if (provider.HasDataFor(parameter))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Return an IEnumerable providing data for use with the
        /// supplied parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo representing one
        /// argument to a parameterized test</param>
        /// <returns>An IEnumerable providing the required data</returns>
        public IEnumerable GetDataFor(ParameterInfo parameter, Test suite)
        {
            ArrayList list = new ArrayList();

            foreach (IDataPointProvider provider in Extensions)
            {
                if (provider is IDataPointProvider2)
                {
                    IDataPointProvider2 provider2 = (IDataPointProvider2)provider;
                    if (provider2.HasDataFor(parameter, suite))
                        foreach (object o in provider2.GetDataFor(parameter, suite))
                            list.Add(o);
                }
                else if (provider.HasDataFor(parameter))
                    foreach (object o in provider.GetDataFor(parameter))
                        list.Add(o);
            }

            return list;
        }
        #endregion

        #region ExtensionPoint Overrides
        protected override bool IsValidExtension(object extension)
        {
            return extension is IDataPointProvider;
        }
        #endregion
    }
}
