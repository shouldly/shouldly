// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;

namespace NUnit.Gui
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
			return NUnit.Gui.AppEntry.Main( args );
		}
	}
}
