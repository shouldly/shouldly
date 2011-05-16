// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.UiKit
{
	using System;
	using System.Windows.Forms;
	using System.Drawing;
	using NUnit.Core;
	using NUnit.Util;

	/// <summary>
	/// Type safe TreeNode for use in the TestSuiteTreeView. 
	/// NOTE: Hides some methods and properties of base class.
	/// </summary>
	public class TestSuiteTreeNode : TreeNode
	{
		#region Instance variables and constant definitions

		/// <summary>
		/// The testcase or testsuite represented by this node
		/// </summary>
		private ITest test;

		/// <summary>
		/// The result from the last run of the test
		/// </summary>
		private TestResult result;

		/// <summary>
		/// Private field used for inclusion by category
		/// </summary>
		private bool included = true;

		/// <summary>
		/// Image indices for various test states - the values 
		/// must match the indices of the image list used
		/// </summary>
		public static readonly int InitIndex = 0;
		public static readonly int SkippedIndex = 0; 
		public static readonly int FailureIndex = 1;
		public static readonly int SuccessIndex = 2;
		public static readonly int IgnoredIndex = 3;
	    public static readonly int InconclusiveIndex = 4;

		#endregion

		#region Constructors

		/// <summary>
		/// Construct a TestNode given a test
		/// </summary>
		public TestSuiteTreeNode( TestInfo test ) : base(test.TestName.Name)
		{
			this.test = test;
			UpdateImageIndex();
		}

		/// <summary>
		/// Construct a TestNode given a TestResult
		/// </summary>
		public TestSuiteTreeNode( TestResult result ) : base( result.Test.TestName.Name )
		{
			this.test = result.Test;
			this.result = result;
			UpdateImageIndex();
		}

		#endregion

		#region Properties	
		/// <summary>
		/// Test represented by this node
		/// </summary>
		public ITest Test
		{
			get { return this.test; }
			set	{ this.test = value; }
		}

		/// <summary>
		/// Test result for this node
		/// </summary>
		public TestResult Result
		{
			get { return this.result; }
			set 
			{ 
				this.result = value;
				UpdateImageIndex();
			}
		}

		public string TestType
		{
			get { return test.TestType; }
		}

		public string StatusText
		{
			get
			{
				if ( result == null )
					return test.RunState.ToString();

				return result.ResultState.ToString();
			}
		}

		public bool Included
		{
			get { return included; }
			set
			{ 
				included = value;
				this.ForeColor = included ? SystemColors.WindowText : Color.LightBlue;
			}
		}
		#endregion

		#region Methods

		/// <summary>
		/// UPdate the image index based on the result field
		/// </summary>
		public void UpdateImageIndex()
		{
			ImageIndex = SelectedImageIndex = CalcImageIndex();
		}

		/// <summary>
		/// Clear the result of this node and all its children
		/// </summary>
		public void ClearResults()
		{
			this.result = null;
			ImageIndex = SelectedImageIndex = InitIndex;

			foreach(TestSuiteTreeNode node in Nodes)
				node.ClearResults();
		}

		/// <summary>
		/// Calculate the image index based on the node contents
		/// </summary>
		/// <returns>Image index for this node</returns>
		private int CalcImageIndex()
		{
			if ( this.result == null )
				return InitIndex;
			
			switch( this.result.ResultState )
			{
                case ResultState.Inconclusive:
			        return InconclusiveIndex;
                case ResultState.Skipped:
					return SkippedIndex;
                case ResultState.NotRunnable:
                case ResultState.Failure:
                case ResultState.Error:
                case ResultState.Cancelled:
			        return FailureIndex;
				case ResultState.Ignored:
					return IgnoredIndex;
				case ResultState.Success:
					int index = SuccessIndex;
					foreach( TestSuiteTreeNode node in this.Nodes )
					{
						if ( node.ImageIndex == FailureIndex )
							return FailureIndex;
						if ( node.ImageIndex == IgnoredIndex )
							index = IgnoredIndex;
					}
					return index;
                default:
			        return InitIndex;
            }
		}

		internal void Accept(TestSuiteTreeNodeVisitor visitor) 
		{
			visitor.Visit(this);
			foreach (TestSuiteTreeNode node in this.Nodes) 
			{
				node.Accept(visitor);
			}
		}

		#endregion
	}

	public abstract class TestSuiteTreeNodeVisitor 
	{
		public abstract void Visit(TestSuiteTreeNode node);
	}
}

