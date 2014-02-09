// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Util
{
	public delegate void AssemblyChangedHandler(string fullPath);

	/// <summary>
	/// AssemblyWatcher keeps track of one or more assemblies to 
	/// see if they have changed. It incorporates a delayed notification
	/// and uses a standard event to notify any interested parties
	/// about the change. The path to the assembly is provided as
	/// an argument to the event handler so that one routine can
	/// be used to handle events from multiple watchers.
	/// </summary>
	public interface IAssemblyWatcher
	{
		/// <summary>
		/// Stops watching for changes.
		/// To release resources call FreeResources.
		/// </summary>
		void Stop();

		/// <summary>
		/// Starts watching for assembly changes.
		/// You need to call Setup before start watching.
		/// </summary>
		void Start();

		/// <summary>
		/// Initializes the watcher with assemblies to observe for changes.
		/// </summary>
		/// <param name="delayInMs">The delay in ms.</param>
		/// <param name="assemblies">The assemblies.</param>
#if CLR_2_0 || CLR_4_0
        void Setup(int delayInMs, System.Collections.Generic.IList<string> assemblies);
#else
        void Setup(int delayInMs, System.Collections.IList assemblies);
#endif

		/// <summary>
		/// Initializes the watcher with assemblies to observe for changes.
		/// </summary>
		/// <param name="delayInMs">The delay in ms.</param>
		/// <param name="assemblyFileName">Name of the assembly file.</param>
		void Setup(int delayInMs, string assemblyFileName);

		/// <summary>
		/// Releases all resources held by the watcher.
		/// </summary>
		void FreeResources();

		/// <summary>
		/// Occurs when an assembly being watched has changed.
		/// </summary>
		event AssemblyChangedHandler AssemblyChanged;
	}
}