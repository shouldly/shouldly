// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;
using NUnit.Core;
using NUnit.Util;

namespace NUnit.UiKit
{
	/// <summary>
	/// Summary description for NotRunTree.
	/// </summary>
	public class NotRunTree : TreeView, TestObserver
	{
		#region TestObserver Members and TestEventHandlers

		public void Subscribe(ITestEvents events)
		{
			events.TestLoaded += new TestEventHandler(ClearTreeNodes);
			events.TestUnloaded += new TestEventHandler(ClearTreeNodes);
			events.TestReloaded += new TestEventHandler(OnTestReloaded);
			events.RunStarting += new TestEventHandler(ClearTreeNodes);
			events.TestFinished += new TestEventHandler(OnTestFinished);
			events.SuiteFinished += new TestEventHandler(OnTestFinished);
		}

		private void OnTestFinished( object sender, TestEventArgs args )
		{
			TestResult result = args.Result;
			if ( result.ResultState == ResultState.Skipped || result.ResultState == ResultState.Ignored)
				this.AddNode( args.Result );
		}

		private void ClearTreeNodes(object sender, TestEventArgs args)
		{
			this.Nodes.Clear();
		}

		private void OnTestReloaded(object sender, TestEventArgs args)
		{
			if ( Services.UserSettings.GetSetting( "Options.TestLoader.ClearResultsOnReload", false ) )
				this.Nodes.Clear();
		}

		private void AddNode( TestResult result )
		{
			TreeNode node = new TreeNode(result.Name);
			TreeNode reasonNode = new TreeNode("Reason: " + result.Message);
			node.Nodes.Add(reasonNode);

			Nodes.Add( node );
		}
		#endregion
	}
}
