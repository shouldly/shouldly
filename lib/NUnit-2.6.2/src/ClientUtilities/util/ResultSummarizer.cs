// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Util
{
	using System;
	using NUnit.Core;

	/// <summary>
	/// Summary description for ResultSummarizer.
	/// </summary>
	public class ResultSummarizer
	{
	    private int resultCount = 0;
		private int testsRun = 0;
		private int failureCount = 0;
	    private int errorCount = 0;
	    private int successCount = 0;
	    private int inconclusiveCount = 0;
		private int skipCount = 0;
		private int ignoreCount = 0;
	    private int notRunnable = 0;
		
		private double time = 0.0d;
		private string name;

		public ResultSummarizer() { }

		public ResultSummarizer(TestResult result)
		{
			Summarize(result);
		}

		public ResultSummarizer(TestResult[] results)
		{
			foreach( TestResult result in results )
				Summarize(result);
		}

		public void Summarize( TestResult result )
		{
			if (this.name == null )
			{
				this.name = result.Name;
				this.time = result.Time;
			}

			if (!result.Test.IsSuite)
			{
			    resultCount++;

                switch (result.ResultState)
                {
                    case ResultState.Success:
                        successCount++;
                        testsRun++;
                        break;
                    case ResultState.Failure:
                        failureCount++;
                        testsRun++;
                        break;
                    case ResultState.Error:
                    case ResultState.Cancelled:
                        errorCount++;
                        testsRun++;
                        break;
                    case ResultState.Inconclusive:
                        inconclusiveCount++;
                        testsRun++;
                        break;
                    case ResultState.NotRunnable:
                        notRunnable++;
                        //errorCount++;
                        break;
                    case ResultState.Ignored:
                        ignoreCount++;
                        break;
                    case ResultState.Skipped:
                    default:
                        skipCount++;
                        break;
                }
            }

			if ( result.HasResults )
				foreach (TestResult childResult in result.Results)
					Summarize( childResult );
		}

		public string Name
		{
			get { return name; }
		}

		public bool Success
		{
			get { return failureCount == 0; }
		}

        /// <summary>
        /// Returns the number of test cases for which results
        /// have been summarized. Any tests excluded by use of
        /// Category or Explicit attributes are not counted.
        /// </summary>
	    public int ResultCount
	    {
            get { return resultCount;  }    
	    }

        /// <summary>
        /// Returns the number of test cases actually run, which
        /// is the same as ResultCount, less any Skipped, Ignored
        /// or NonRunnable tests.
        /// </summary>
		public int TestsRun
		{
			get { return testsRun; }
		}

	    /// <summary>
	    /// Returns the number of tests that passed
	    /// </summary>
        public int Passed
	    {
            get { return successCount;  }
	    }

        /// <summary>
        /// Returns the number of test cases that had an error.
        /// </summary>
        public int Errors
        {
            get { return errorCount; }
        }

        /// <summary>
        /// Returns the number of test cases that failed.
        /// </summary>
		public int Failures 
		{
			get { return failureCount; }
		}

        /// <summary>
        /// Returns the number of test cases that failed.
        /// </summary>
        public int Inconclusive
        {
            get { return inconclusiveCount; }
        }

        /// <summary>
        /// Returns the number of test cases that were not runnable
        /// due to errors in the signature of the class or method.
        /// Such tests are also counted as Errors.
        /// </summary>
	    public int NotRunnable
	    {
	        get { return notRunnable; }   
	    }

        /// <summary>
        /// Returns the number of test cases that were skipped.
        /// </summary>
		public int Skipped
		{
			get { return skipCount; }
		}

		public int Ignored
		{
			get { return ignoreCount; }
		}

		public double Time
		{
			get { return time; }
		}

		public int TestsNotRun
		{
			get { return skipCount + ignoreCount + notRunnable; }
		}
	}
}
