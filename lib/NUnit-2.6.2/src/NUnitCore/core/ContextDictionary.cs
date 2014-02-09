// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace NUnit.Core
{
	[Serializable]
    public class ContextDictionary : MarshalByRefObject, IDictionary, ILogicalThreadAffinative
    {
        internal TestExecutionContext _context;
		private readonly Hashtable _storage = new Hashtable();

        public ContextDictionary() { }

        public ContextDictionary(TestExecutionContext context)
        {
            _context = context;
        }

		public object this[object key]
        {
            get
            {
                // Get Result values dynamically, since
                // they may change as execution proceeds
                switch (key as string)
                {
                    case "Test.Name":
                        return _context.CurrentTest.TestName.Name;
                    case "Test.FullName":
                        return _context.CurrentTest.TestName.FullName;
                    case "Test.Properties":
                        return _context.CurrentTest.Properties;
                    case "Result.State":
                        return (int)_context.CurrentResult.ResultState;
                    case "TestDirectory":
                        return AssemblyHelper.GetDirectoryName(_context.CurrentTest.FixtureType.Assembly);
                    case "WorkDirectory":
                        return _context.TestPackage.Settings.Contains("WorkDirectory")
                            ? _context.TestPackage.Settings["WorkDirectory"]
                            : Environment.CurrentDirectory;
                    default:
                        return _storage[key];
                }
            }
            set
            {
                _storage[key] = value;
            }
        }

		#region IDictionary Interface non-implementation

		void IDictionary.Remove(object key)
		{
			throw new NotImplementedException();
		}

		ICollection IDictionary.Keys
		{
			get { throw new NotImplementedException(); }
		}

		ICollection IDictionary.Values
		{
			get { throw new NotImplementedException(); }
		}

		bool IDictionary.IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		bool IDictionary.IsFixedSize
		{
			get { throw new NotImplementedException(); }
		}

		bool IDictionary.Contains(object key)
		{
			throw new NotImplementedException();
		}

		void IDictionary.Add(object key, object value)
		{
			throw new NotImplementedException();
		}

		void IDictionary.Clear()
		{
			throw new NotImplementedException();
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		void ICollection.CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		int ICollection.Count
		{
			get { throw new NotImplementedException(); }
		}

		object ICollection.SyncRoot
		{
			get { throw new NotImplementedException(); }
		}

		bool ICollection.IsSynchronized
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
    }
}