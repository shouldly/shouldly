// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;
using NUnit.Core.Extensibility;

namespace NUnit.Samples.Extensibility
{
	/// <summary>
	/// This is the smallest possible Addin, which does nothing 
	/// but is recognized by NUnit and listed in the Addins dialog.
	/// 
	/// The Addin class is marked by the NUnitAddin attribute and
	/// implements IAddin, as required. Optional property syntax
	/// is used here to override the default name of the addin and
	/// to provide a description. Both are displayed by NUnit in the
	/// Addin Dialog.
	/// 
	/// The addin doesn't actually install anything, but simply
	/// returns false in its Install method.
	/// </summary>
	[NUnitAddin(Name="Minimal Addin", Description="This Addin doesn't do anything")]
	public class Minimal : IAddin
	{
		#region IAddin Members
		public bool Install(IExtensionHost host)
		{
			// TODO:  Add Minimal.Install implementation
			return true;
		}
		#endregion
	}
}
