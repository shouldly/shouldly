// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;

namespace NUnit.Framework.Tests
{
	/// <summary>
	/// ICollectionAdapter is used in testing to wrap an array or
	/// ArrayList, ensuring that only methods of the ICollection
	/// interface are accessible.
	/// </summary>
	class ICollectionAdapter : ICollection
	{
		private readonly ICollection inner;

		public ICollectionAdapter(ICollection inner)
		{
			this.inner = inner;
		}

		public ICollectionAdapter(params object[] inner)
		{
			this.inner = inner;
		}

		#region ICollection Members

		public void CopyTo(Array array, int index)
		{
			inner.CopyTo(array, index);
		}

		public int Count
		{
			get { return inner.Count; }
		}

		public bool IsSynchronized
		{
			get { return  inner.IsSynchronized; }
		}

		public object SyncRoot
		{
			get { return inner.SyncRoot; }
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return inner.GetEnumerator();
		}

		#endregion
	}
}
