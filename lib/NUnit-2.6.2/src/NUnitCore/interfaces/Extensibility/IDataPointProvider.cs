// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Collections;
using System.Reflection;

namespace NUnit.Core.Extensibility
{
    /// <summary>
    /// The IDataPointProvider interface is used by extensions
    /// that provide data for a single test parameter.
    /// </summary>
    public interface IDataPointProvider
    {
        /// <summary>
        /// Determine whether any data is available for a parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo representing one
        /// argument to a parameterized test</param>
        /// <returns>True if any data is available, otherwise false.</returns>
        bool HasDataFor(ParameterInfo parameter);

        /// <summary>
        /// Return an IEnumerable providing data for use with the
        /// supplied parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo representing one
        /// argument to a parameterized test</param>
        /// <returns>An IEnumerable providing the required data</returns>
        IEnumerable GetDataFor(ParameterInfo parameter);
    }

    /// <summary>
    /// The IDataPointProvider2 interface extends IDataPointProvider
    /// by making the test fixture for which the test is being built
    /// available for use.
    /// </summary>
    public interface IDataPointProvider2 : IDataPointProvider
    {
        /// <summary>
        /// Determine whether any data is available for a parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo representing one
        /// argument to a parameterized test</param>
        /// <param name="parentSuite">The test suite for which the test is being built</param>
        /// <returns>True if any data is available, otherwise false.</returns>
        bool HasDataFor(ParameterInfo parameter, Test parentSuite);

        /// <summary>
        /// Return an IEnumerable providing data for use with the
        /// supplied parameter.
        /// </summary>
        /// <param name="parameter">A ParameterInfo representing one
        /// argument to a parameterized test</param>
        /// <param name="parentSuite">The test suite for which the test is being built</param>
        /// <returns>An IEnumerable providing the required data</returns>
        IEnumerable GetDataFor(ParameterInfo parameter, Test parentSuite);
    }
}
