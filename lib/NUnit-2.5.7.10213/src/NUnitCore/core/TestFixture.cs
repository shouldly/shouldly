// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Core
{
	/// <summary>
	/// TestFixture is a surrogate for a user test fixture class,
	/// containing one or more tests.
	/// </summary>
	public class TestFixture : TestSuite
	{
		#region Constructors
        public TestFixture(Type fixtureType)
            : base(fixtureType) { }
        public TestFixture(Type fixtureType, object[] arguments)
            : base(fixtureType, arguments) { }
        #endregion

		#region TestSuite Overrides

        /// <summary>
        /// Gets a string representing the kind of test
        /// that this object represents, for use in display.
        /// </summary>
        public override string TestType
        {
            get { return "TestFixture"; }
        }

        public override TestResult Run(EventListener listener, ITestFilter filter)
        {
            using ( new DirectorySwapper( AssemblyHelper.GetDirectoryName( FixtureType.Assembly ) ) )
            {
                return base.Run(listener, filter);
            }
        }
		#endregion
	}
}
