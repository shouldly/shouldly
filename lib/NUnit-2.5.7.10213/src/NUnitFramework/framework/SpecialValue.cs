// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework
{
    /// <summary>
    /// The SpecialValue enum is used to represent TestCase arguments
    /// that cannot be used as arguments to an Attribute.
    /// </summary>
	public enum SpecialValue
	{
        /// <summary>
        /// Null represents a null value, which cannot be used as an 
        /// argument to an attriute under .NET 1.x
        /// </summary>
        Null
	}
}
