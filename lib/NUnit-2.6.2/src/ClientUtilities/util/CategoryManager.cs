// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using NUnit.Core;

namespace NUnit.Util
{
	public class CategoryManager
	{
		private Hashtable categories = new Hashtable();

		public void Add(string name) 
		{
			categories[name] = name;
		}

		public void AddCategories( ITest test )
		{
            if (test.Categories != null)
                foreach (string name in test.Categories)
                    if (NUnitFramework.IsValidCategoryName(name))
                        Add(name);
		}

		public void AddAllCategories( ITest test )
		{
			AddCategories( test );
			if ( test.IsSuite )
				foreach( ITest child in test.Tests )
					AddAllCategories( child );
		}

		public ICollection Categories 
		{
			get { return categories.Values; }
		}

		public void Clear() 
		{
			categories = new Hashtable();
		}
	}
}
