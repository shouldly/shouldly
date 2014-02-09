// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework
{
    /// <summary>
    /// RequiredAddinAttribute may be used to indicate the names of any addins
    /// that must be present in order to run some or all of the tests in an
    /// assembly. If the addin is not loaded, the entire assembly is marked
    /// as NotRunnable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly,AllowMultiple=true, Inherited=false)]
    public class RequiredAddinAttribute : Attribute
    {
        private string requiredAddin;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequiredAddinAttribute"/> class.
        /// </summary>
        /// <param name="requiredAddin">The required addin.</param>
        public RequiredAddinAttribute(string requiredAddin)
        {
            this.requiredAddin = requiredAddin;
        }

        /// <summary>
        /// Gets the name of required addin.
        /// </summary>
        /// <value>The required addin name.</value>
        public string RequiredAddin
        {
            get { return requiredAddin; }
        }
    }
}
