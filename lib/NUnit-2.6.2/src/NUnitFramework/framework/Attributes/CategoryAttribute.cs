// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Framework
{
	/// <summary>
	/// Attribute used to apply a category to a test
	/// </summary>
	[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Assembly, AllowMultiple=true, Inherited=true)]
	public class CategoryAttribute : Attribute
	{
		/// <summary>
		/// The name of the category
		/// </summary>
		protected string categoryName;

		/// <summary>
		/// Construct attribute for a given category based on
        /// a name. The name may not contain the characters ',',
        /// '+', '-' or '!'. However, this is not checked in the
        /// constructor since it would cause an error to arise at
        /// as the test was loaded without giving a clear indication
        /// of where the problem is located. The error is handled
        /// in NUnitFramework.cs by marking the test as not
        /// runnable.
		/// </summary>
		/// <param name="name">The name of the category</param>
		public CategoryAttribute(string name)
		{
			this.categoryName = name.Trim();
		}

		/// <summary>
		/// Protected constructor uses the Type name as the name
		/// of the category.
		/// </summary>
		protected CategoryAttribute()
		{
			this.categoryName = this.GetType().Name;
			if ( categoryName.EndsWith( "Attribute" ) )
				categoryName = categoryName.Substring( 0, categoryName.Length - 9 );
		}

		/// <summary>
		/// The name of the category
		/// </summary>
		public string Name 
		{
			get { return categoryName; }
		}
	}
}
