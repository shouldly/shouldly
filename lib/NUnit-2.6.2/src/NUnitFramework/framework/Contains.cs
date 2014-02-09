// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
    /// <summary>
    /// Static helper class used in the constraint-based syntax
    /// </summary>
    public class Contains
    {
        /// <summary>
        /// Creates a new SubstringConstraint
        /// </summary>
        /// <param name="substring">The value of the substring</param>
        /// <returns>A SubstringConstraint</returns>
        public static SubstringConstraint Substring(string substring)
        {
            return new SubstringConstraint(substring);
        }

        /// <summary>
        /// Creates a new CollectionContainsConstraint.
        /// </summary>
        /// <param name="item">The item that should be found.</param>
        /// <returns>A new CollectionContainsConstraint</returns>
        public static CollectionContainsConstraint Item(object item)
        {
            return new CollectionContainsConstraint(item);
        }
    }
}
