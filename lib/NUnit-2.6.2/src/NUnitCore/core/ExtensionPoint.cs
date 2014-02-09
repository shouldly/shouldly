// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using NUnit.Core.Extensibility;

namespace NUnit.Core
{
	public class ExtensionsCollection : IEnumerable
	{
		private static readonly int DEFAULT_LEVEL = 0;
		private static readonly int MAX_LEVELS = 10;

		private ArrayList[] lists;

		public ExtensionsCollection() : this(1) {}

		public ExtensionsCollection( int levels )
		{
			if ( levels < 1 )
				levels = 1;
			else if ( levels > MAX_LEVELS )
				levels = MAX_LEVELS;

			lists = new ArrayList[levels];
		}

	    public int Levels
	    {
            get { return lists.Length; }   
	    }

		public void Add( object extension )
		{
			Add( extension, DEFAULT_LEVEL );
		}

		public void Add( object extension, int level )
		{
			if ( level < 0 || level >= lists.Length )
				throw new ArgumentOutOfRangeException("level", level, 
					"Value must be between 0 and " + lists.Length );

			if ( lists[level] == null )
				lists[level] = new ArrayList();

			lists[level].Insert( 0, extension );
		}

		public void Remove( object extension )
		{
			foreach( IList list in lists )
				list.Remove( extension );
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return new ExtensionsEnumerator( lists );
		}

		public class ExtensionsEnumerator : IEnumerator
		{
			private ArrayList[] lists;
			private IEnumerator listEnum;
			private int currentLevel;

			public ExtensionsEnumerator( ArrayList[] lists )
			{
				this.lists = lists;
				Reset();
			}

			#region IEnumerator Members

			public void Reset()
			{
				this.listEnum = null;
				this.currentLevel = -1;
			}

			public object Current
			{
				get
				{
					return listEnum.Current;
				}
			}

			public bool MoveNext()
			{
				if ( listEnum != null && listEnum.MoveNext() )
					return true;

				while ( ++currentLevel < lists.Length )
				{
					IList list = lists[currentLevel];
					if ( list != null )
					{
						listEnum = list.GetEnumerator();
						if ( listEnum.MoveNext() )
							return true;
					}
				}

				return false;
			}

			#endregion
		}

		#endregion
	}

	/// <summary>
	/// ExtensionPoint is used as a base class for all 
	/// extension points.
	/// </summary>
	public abstract class ExtensionPoint : IExtensionPoint
	{
		private readonly string name;
		private readonly IExtensionHost host;
		private readonly ExtensionsCollection extensions;

		protected IEnumerable Extensions
		{
			get { return extensions; }
		}

		#region Constructor
        public ExtensionPoint(string name, IExtensionHost host) : this( name, host, 0) { }

	    public ExtensionPoint(string name, IExtensionHost host, int priorityLevels)
		{
			this.name = name;
			this.host = host;
            extensions = new ExtensionsCollection(priorityLevels);
		}
		#endregion

		#region IExtensionPoint Members
		/// <summary>
		/// Get the name of this extension point
		/// </summary>
		public string Name
		{
			get { return this.name; }
		}

		/// <summary>
		/// Get the host that provides this extension point
		/// </summary>
		public IExtensionHost Host
		{
			get { return this.host; }
		}

        /// <summary>
        /// Install an extension at this extension point. If the
        /// extension object does not meet the requirements for
        /// this extension point, an exception is thrown.
        /// </summary>
        /// <param name="extension">The extension to install</param>
        public void Install(object extension)
        {
            if (!IsValidExtension(extension))
                throw new ArgumentException(
                    extension.GetType().FullName + " is not {0} extension point", "extension");

            extensions.Add(extension);
        }

        /// <summary>
        /// Install an extension at this extension point specifying
        /// an integer priority value for the extension.If the
        /// extension object does not meet the requirements for
        /// this extension point, or if the extension point does
        /// not support the requested priority level, an exception 
        /// is thrown.
        /// </summary>
        /// <param name="extension">The extension to install</param>
        /// <param name="priority">The priority level for this extension</param>
        public void Install(object extension, int priority )
        {
            if (!IsValidExtension(extension))
                throw new ArgumentException(
                    extension.GetType().FullName + " is not {0} extension point", "extension");

            if (priority < 0 || priority >= extensions.Levels)
                throw new ArgumentException("Priority value not supported", "priority");

            extensions.Add(extension, priority);
        }

        /// <summary>
		/// Removes an extension from this extension point. If the
		/// extension object is not present, the method returns
		/// without error.
		/// </summary>
		/// <param name="extension"></param>
		public void Remove(object extension)
		{
			extensions.Remove( extension );
		}
		#endregion

		#region Abstract Members
		protected abstract bool IsValidExtension(object extension);
		#endregion
	}
}
