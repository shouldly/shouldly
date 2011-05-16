// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Gui
{
	using System;
	using System.Windows.Forms;
	using NUnit.Core;
	using NUnit.Util;

	/// <summary>
	/// Summary description for DetailResults
	/// </summary>
	public class DetailResults
	{
		private readonly ListBox testDetails;
		private readonly TreeView notRunTree;

		public DetailResults(ListBox listBox, TreeView notRun)
		{
			testDetails = listBox;
			notRunTree = notRun;
		}

		public void DisplayResults( TestResult results )
		{
			notRunTree.BeginUpdate();
			ProcessResults( results );
			notRunTree.EndUpdate();

			if( testDetails.Items.Count > 0 )
				testDetails.SelectedIndex = 0;
		}

		private void ProcessResults(TestResult result)
		{
			switch( result.ResultState )
			{
                case ResultState.Failure:
                case ResultState.Error:
                case ResultState.Cancelled:
					TestResultItem item = new TestResultItem(result);
					//string resultString = String.Format("{0}:{1}", result.Name, result.Message);
					testDetails.BeginUpdate();
					testDetails.Items.Insert(testDetails.Items.Count, item);
					testDetails.EndUpdate();
			        break;
                case ResultState.Skipped:
                case ResultState.NotRunnable:
                case ResultState.Ignored:
    				notRunTree.Nodes.Add(MakeNotRunNode(result));
			        break;
			}

			if ( result.HasResults )
				foreach (TestResult childResult in result.Results)
	                ProcessResults( childResult );
        }

		private static TreeNode MakeNotRunNode(TestResult result)
		{
			TreeNode node = new TreeNode(result.Name);

			TreeNode reasonNode = new TreeNode("Reason: " + result.Message);
			
			node.Nodes.Add(reasonNode);

			return node;
		}
	}
}
