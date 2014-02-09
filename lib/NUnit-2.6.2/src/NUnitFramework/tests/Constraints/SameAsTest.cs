// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class SameAsTest : ConstraintTestBase
    {
        private static readonly object obj1 = new object();
        private static readonly object obj2 = new object();

        [SetUp]
        public void SetUp()
        {
            theConstraint = new SameAsConstraint(obj1);
            expectedDescription = "same as <System.Object>";
            stringRepresentation = "<sameas System.Object>";
        }

        internal static object[] SuccessData = new object[] { obj1 };

        internal static object[] FailureData = new object[] { obj2, 3, "Hello" };

        internal static string[] ActualValues = new string[] { "<System.Object>", "3", "\"Hello\"" };
    }
}