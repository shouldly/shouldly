// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework
{
    /// <summary>
    /// FactoryAttribute indicates the source to be used to
    /// provide test cases for a test method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class TestCaseSourceAttribute : Attribute
    {
        private readonly string sourceName;
        private readonly Type sourceType;
        private string category;

        /// <summary>
        /// Construct with the name of the data source, which must
        /// be a property, field or method of the test class itself.
        /// </summary>
        /// <param name="sourceName">An array of the names of the factories that will provide data</param>
        public TestCaseSourceAttribute(string sourceName)
        {
            this.sourceName = sourceName;
        }

        /// <summary>
        /// Construct with a Type, which must implement IEnumerable
        /// </summary>
        /// <param name="sourceType">The Type that will provide data</param>
        public TestCaseSourceAttribute(Type sourceType)
        {
            this.sourceType = sourceType;
        }

        /// <summary>
        /// Construct with a Type and name.
        /// that don't support params arrays.
        /// </summary>
        /// <param name="sourceType">The Type that will provide data</param>
        /// <param name="sourceName">The name of the method, property or field that will provide data</param>
        public TestCaseSourceAttribute(Type sourceType, string sourceName)
        {
            this.sourceType = sourceType;
            this.sourceName = sourceName;
        }

        /// <summary>
        /// The name of a the method, property or fiend to be used as a source
        /// </summary>
        public string SourceName
        {
            get { return sourceName; }   
        }

        /// <summary>
        /// A Type to be used as a source
        /// </summary>
        public Type SourceType
        {
            get { return sourceType;  }
        }

        /// <summary>
        /// Gets or sets the category associated with this test.
        /// May be a single category or a comma-separated list.
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
    }
}
