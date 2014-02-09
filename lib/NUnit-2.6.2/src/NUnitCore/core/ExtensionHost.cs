// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
using System.Collections;
using NUnit.Core.Extensibility;

namespace NUnit.Core
{
	/// <summary>
	/// ExtensionHost is the abstract base class used for
	/// all extension hosts. It provides an array of 
	/// extension points and a FrameworkRegistry and
	/// implements the IExtensionHost interface. Derived
	/// classes must initialize the extension points.
	/// </summary>
	public abstract class ExtensionHost : IExtensionHost
	{
		#region Protected Fields

	    protected ArrayList extensions;

		protected ExtensionType supportedTypes;
		#endregion

		#region IExtensionHost Interface
		public IExtensionPoint[] ExtensionPoints
		{
			get { return (IExtensionPoint[])extensions.ToArray(typeof(IExtensionPoint)); }
		}

        public IFrameworkRegistry FrameworkRegistry
        {
            get { return (IFrameworkRegistry)GetExtensionPoint("FrameworkRegistry"); }
        }

		public IExtensionPoint GetExtensionPoint( string name )
		{
			foreach ( IExtensionPoint extensionPoint in extensions )
				if ( extensionPoint.Name == name )
					return extensionPoint;

			return null;
		}

		public ExtensionType ExtensionTypes
		{
			get { return supportedTypes; }
		}
		#endregion
	}
}
