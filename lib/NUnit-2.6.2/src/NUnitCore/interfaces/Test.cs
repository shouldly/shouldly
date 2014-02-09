// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Core
{
	using System;
	using System.Collections;
	using System.Collections.Specialized;
    using System.Threading;
	using System.Reflection;

	/// <summary>
	///		Test Class.
	/// </summary>
	public abstract class Test : ITest, IComparable
    {
        #region Constants
        //private static readonly string SETCULTURE = "_SETCULTURE";
        private static readonly string DESCRIPTION = "_DESCRIPTION";
        private static readonly string IGNOREREASON = "_IGNOREREASON";
        private static readonly string CATEGORIES = "_CATEGORIES";
        #endregion

        #region Fields
        /// <summary>
		/// TestName that identifies this test
		/// </summary>
		private TestName testName;

		/// <summary>
		/// Indicates whether the test should be executed
		/// </summary>
		private RunState runState;

		/// <summary>
		/// Test suite containing this test, or null
		/// </summary>
		private Test parent;
		
		/// <summary>
		/// A dictionary of properties, used to add information
		/// to tests without requiring the class to change.
		/// </summary>
		private IDictionary properties;

		#endregion

        #region Properties
        /// <summary>
        /// Return true if the test requires a thread
        /// </summary>
        public bool RequiresThread
        {
            get { return Properties.Contains("RequiresThread") && (bool)Properties["RequiresThread"]; }
        }

        /// <summary>
        /// Get the desired apartment state for running the test
        /// </summary>
        public ApartmentState ApartmentState
        {
            get
            {
                return Properties.Contains("APARTMENT_STATE")
                    ? (ApartmentState)Properties["APARTMENT_STATE"]
                    : GetCurrentApartment();
            }
        }

        /// <summary>
        /// Get the current apartment state of the test
        /// </summary>
        protected ApartmentState GetCurrentApartment()
        {
#if CLR_2_0 || CLR_4_0
            return Thread.CurrentThread.GetApartmentState();
#else
            return Thread.CurrentThread.ApartmentState;
#endif
        }

        /// <summary>
        /// Gets a boolean value indicating whether this 
        /// test should run on it's own thread.
        /// </summary>
        protected virtual bool ShouldRunOnOwnThread
        {
            get
            {
                return RequiresThread
                    || ApartmentState != ApartmentState.Unknown
                    && ApartmentState != GetCurrentApartment();
            }
        }
		#endregion

        #region Construction

        /// <summary>
		/// Constructs a test given its name
		/// </summary>
		/// <param name="name">The name of the test</param>
		protected Test( string name )
		{
			this.testName = new TestName();
			this.testName.FullName = name;
			this.testName.Name = name;
			this.testName.TestID = new TestID();

            this.runState = RunState.Runnable;
		}

		/// <summary>
		/// Constructs a test given the path through the
		/// test hierarchy to its parent and a name.
		/// </summary>
		/// <param name="pathName">The parent tests full name</param>
		/// <param name="name">The name of the test</param>
		protected Test( string pathName, string name ) 
		{ 
			this.testName = new TestName();
			this.testName.FullName = pathName == null || pathName == string.Empty 
				? name : pathName + "." + name;
			this.testName.Name = name;
			this.testName.TestID = new TestID();

            this.runState = RunState.Runnable;
		}

		/// <summary>
		/// Constructs a test given a TestName object
		/// </summary>
		/// <param name="testName">The TestName for this test</param>
		protected Test( TestName testName )
		{
			this.testName = testName;
			
			this.runState = RunState.Runnable;
		}

		/// <summary>
		/// Sets the runner id of a test and optionally its children
		/// </summary>
		/// <param name="runnerID">The runner id to be used</param>
		/// <param name="recursive">True if all children should get the same id</param>
		public void SetRunnerID( int runnerID, bool recursive )
		{
			this.testName.RunnerID = runnerID;

			if ( recursive && this.Tests != null )
				foreach( Test child in this.Tests )
					child.SetRunnerID( runnerID, true );
		}

		#endregion

		#region ITest Members

        #region Properties
		/// <summary>
		/// Gets the TestName of the test
		/// </summary>
        public TestName TestName
		{
			get { return testName; }
		}

		/// <summary>
		/// Gets a string representing the kind of test
		/// that this object represents, for use in display.
		/// </summary>
        public abstract string TestType
        {
            get;
        }


		/// <summary>
		/// Whether or not the test should be run
		/// </summary>
        public RunState RunState
        {
            get { return runState; }
            set { runState = value; }
        }

		/// <summary>
		/// Reason for not running the test, if applicable
		/// </summary>
		public string IgnoreReason
		{
			get { return (string)Properties[IGNOREREASON]; }
			set 
            {
                if (value == null)
                    Properties.Remove(IGNOREREASON);
                else
                    Properties[IGNOREREASON] = value;
            }
		}

		/// <summary>
		/// Gets a count of test cases represented by
		/// or contained under this test.
		/// </summary>
		public virtual int TestCount 
		{ 
			get { return 1; } 
		}

		/// <summary>
		/// Gets a list of categories associated with this test.
		/// </summary>
		public IList Categories 
		{
			get 
            {
                if (Properties[CATEGORIES] == null)
                    Properties[CATEGORIES] = new ArrayList();

                return (IList)Properties[CATEGORIES]; 
            }
			set 
            {
                Properties[CATEGORIES] = value; 
            }
		}

		/// <summary>
		/// Gets a description associated with this test.
		/// </summary>
		public String Description
		{
			get { return (string)Properties[DESCRIPTION]; }
			set 
            {
                if (value == null)
                    Properties.Remove(DESCRIPTION);
                else
                    Properties[DESCRIPTION] = value; 
            }
		}

		/// <summary>
		/// Gets the property dictionary for this test
		/// </summary>
		public IDictionary Properties
		{
			get 
			{
				if ( properties == null )
					properties = new ListDictionary();

				return properties; 
			}
			set
			{
				properties = value;
			}
		}

		/// <summary>
		/// Indicates whether this test is a suite
		/// </summary>
        public virtual bool IsSuite
        {
            get { return false; }
        }


		/// <summary>
		/// Gets the parent test of this test
		/// </summary>
		ITest ITest.Parent 
		{
			get { return parent; }
		}

		/// <summary>
		/// Gets the parent as a Test object.
		/// Used by the core to set the parent.
		/// </summary>
		public Test Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		/// <summary>
		/// Gets this test's child tests
		/// </summary>
		public virtual IList Tests 
        {
            get { return null; } 
        }

		/// <summary>
		/// Gets the Type of the fixture used in running this test
		/// </summary>
		public virtual Type FixtureType
		{
			get { return null; }
		}

        /// <summary>
        /// Gets the name of the class containing this test. Returns
        /// null if the test is not associated with a class.
        /// </summary>
        public string ClassName
        {
            get
            {
                Type type = FixtureType;

                if (type == null)
                    return null;

#if CLR_2_0 || CLR_4_0
                if (type.IsGenericType)
                    type = type.GetGenericTypeDefinition();
#endif

                return type.FullName;
            }
        }

        /// <summary>
        /// Gets the name of the method implementing this test.
        /// Returns null if the test is not implemented as a method.
        /// </summary>
        public virtual string MethodName
        {
            get { return null; }
        }

		/// <summary>
		/// Gets or sets a fixture object for running this test
		/// </summary>
		public  abstract object Fixture
		{
			get; set;
        }
        #endregion

        #region Methods
		/// <summary>
		/// Gets a count of test cases that would be run using
		/// the specified filter.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
        public virtual int CountTestCases(ITestFilter filter)
        {
            if (filter.Pass(this))
                return 1;

            return 0;
        }

        /// <summary>
        /// Runs the test under a particular filter, sending
        /// notifications to a listener.
        /// </summary>
        /// <param name="listener">An event listener to receive notifications</param>
        /// <param name="filter">A filter used in running the test</param>
        /// <returns></returns>
        public abstract TestResult Run(EventListener listener, ITestFilter filter);
        #endregion

        #endregion

		#region IComparable Members
		/// <summary>
		/// Compares this test to another test for sorting purposes
		/// </summary>
		/// <param name="obj">The other test</param>
		/// <returns>Value of -1, 0 or +1 depending on whether the current test is less than, equal to or greater than the other test</returns>
		public int CompareTo(object obj)
		{
			Test other = obj as Test;
			
			if ( other == null )
				return -1;

			return this.TestName.FullName.CompareTo( other.TestName.FullName );
		}
		#endregion
	}
}
