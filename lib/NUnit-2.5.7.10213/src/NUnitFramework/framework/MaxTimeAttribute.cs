// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;

namespace NUnit.Framework
{
	/// <summary>
	/// Summary description for MaxTimeAttribute.
	/// </summary>
	[AttributeUsage( AttributeTargets.Method, AllowMultiple=false, Inherited=false )]
	public sealed class MaxTimeAttribute : PropertyAttribute
	{
        /// <summary>
        /// Construct a MaxTimeAttribute, given a time in milliseconds.
        /// </summary>
        /// <param name="milliseconds">The maximum elapsed time in milliseconds</param>
		public MaxTimeAttribute( int milliseconds )
            : base( milliseconds ) { }
	}
}
