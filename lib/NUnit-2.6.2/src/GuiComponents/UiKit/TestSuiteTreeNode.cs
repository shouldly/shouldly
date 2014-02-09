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

        private bool showFailedAssumptions = false;

		/// <summary>
		/// Image indices for various test states - the values 
		/// must match the indices of the image list used
		/// </summary>
		public const int InitIndex = 0;
		public const int SkippedIndex = 0; 
		public const int FailureIndex = 1;
		public const int SuccessIndex = 2;
		public const int IgnoredIndex = 3;
	    public const int InconclusiveIndex = 4;

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

        /// <summary>
        /// Return true if the node has a result, otherwise false.
        /// </summary>
        public bool HasResult
        {
            get { return this.result != null; }
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

        public bool ShowFailedAssumptions
        {
            get { return showFailedAssumptions; }
            set
            {
                if (value != showFailedAssumptions)
                {
                    showFailedAssumptions = value;

                    if (HasInconclusiveResults)
                        RepopulateTheoryNode();
                }
            }
        }

        public bool HasInconclusiveResults
        {
            get
            {
                bool hasInconclusiveResults = false;
                if (Result != null)
                {
                    foreach (TestResult result in Result.Results)
                    {
                        hasInconclusiveResults |= result.ResultState == ResultState.Inconclusive;
                        if (hasInconclusiveResults)
                            break;
                    }
                }
                return hasInconclusiveResults;
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
			ImageIndex = SelectedImageIndex = CalcImageIndex();

			foreach(TestSuiteTreeNode node in Nodes)
				node.ClearResults();
		}

        /// <summary>
        /// Gets the Theory node associated with the current
        /// node. If the current node is a Theory, then the
        /// current node is returned. Otherwise, if the current
        /// node is a test case under a theory node, then that
        /// node is returned. Otherwise, null is returned.
        /// </summary>
        /// <returns></returns>
        public TestSuiteTreeNode GetTheoryNode()
        {
            if (this.Test.TestType == "Theory")
                return this;

            TestSuiteTreeNode parent = this.Parent as TestSuiteTreeNode;
            if (parent != null && parent.Test.TestType == "Theory")
                return parent;

            return null;
        }

        /// <summary>
        /// Regenerate the test cases under a theory, respecting
        /// the current setting for ShowFailedAssumptions
        /// </summary>
        public void RepopulateTheoryNode()
        {
            // Ignore if it's not a theory or if it has not been run yet
            if (this.Test.TestType == "Theory" && this.HasResult)
            {
                Nodes.Clear();

                foreach (TestResult result in Result.Results)
                    if (showFailedAssumptions || result.ResultState != ResultState.Inconclusive)
                        Nodes.Add(new TestSuiteTreeNode(result));
            }
        }

        /// <summary>
		/// Calculate the image index based on the node contents
		/// </summary>
		/// <returns>Image index for this node</returns>
		private int CalcImageIndex()
		{
            if (this.result == null)
            {
                switch (this.test.RunState)
                {
                    case RunState.Ignored:
                        return IgnoredIndex;
                    case RunState.NotRunnable:
                        return FailureIndex;
                    default:
                        return InitIndex;
                }
            }
            else
            {
                switch (this.result.ResultState)
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
                        int resultIndex = SuccessIndex;
                        foreach (TestSuiteTreeNode node in this.Nodes)
                        {
                            if (node.ImageIndex == FailureIndex)
                                return FailureIndex; // Return FailureIndex if there is any failure
                            if (node.ImageIndex == IgnoredIndex)
                                resultIndex = IgnoredIndex; // Remember IgnoredIndex - we might still find a failure
                        }
                        return resultIndex;
                    default:
                        return InitIndex;
                }
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

