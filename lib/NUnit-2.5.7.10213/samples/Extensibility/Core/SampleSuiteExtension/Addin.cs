// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Extensions
{
	/// <summary>
	/// Summary description for Addin.
	/// </summary>
	[NUnitAddin(Name="SampleSuiteExtension", Description = "Recognizes Tests starting with SampleTest...")]
	public class Addin : IAddin
	{
		#region IAddin Members
		public bool Install(IExtensionHost host)
		{
			IExtensionPoint builders = host.GetExtensionPoint( "SuiteBuilders" );
			if ( builders == null )
				return false;

			builders.Install( new SampleSuiteExtensionBuilder() );
			return true;
		}
		#endregion
	}
}
