// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Collections.Specialized;

namespace NUnit.Framework
{
	/// <summary>
	/// PropertyAttribute is used to attach information to a test as a name/value pair..
	/// </summary>
	[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Assembly, AllowMultiple=true, Inherited=true)]
	public class PropertyAttribute : Attribute
	{
        private IDictionary properties = new ListDictionary();

        /// <summary>
        /// Construct a PropertyAttribute with a name and string value
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="propertyValue">The property value</param>
        public PropertyAttribute(string propertyName, string propertyValue)
        {
            this.properties.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// Construct a PropertyAttribute with a name and int value
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="propertyValue">The property value</param>
        public PropertyAttribute(string propertyName, int propertyValue)
        {
            this.properties.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// Construct a PropertyAttribute with a name and double value
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="propertyValue">The property value</param>
        public PropertyAttribute(string propertyName, double propertyValue)
        {
            this.properties.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// Constructor for derived classes that set the
        /// property dictionary directly.
        /// </summary>
        protected PropertyAttribute() { }

        /// <summary>
		/// Constructor for use by derived classes that use the
		/// name of the type as the property name. Derived classes
        /// must ensure that the Type of the property value is
        /// a standard type supported by the BCL. Any custom
        /// types will cause a serialization Exception when
        /// in the client.
		/// </summary>
		protected PropertyAttribute( object propertyValue )
		{
			string propertyName = this.GetType().Name;
			if ( propertyName.EndsWith( "Attribute" ) )
				propertyName = propertyName.Substring( 0, propertyName.Length - 9 );
            this.properties.Add(propertyName, propertyValue);
		}

        /// <summary>
        /// Gets the property dictionary for this attribute
        /// </summary>
        public IDictionary Properties
        {
            get { return properties; }
        }
    }
}
