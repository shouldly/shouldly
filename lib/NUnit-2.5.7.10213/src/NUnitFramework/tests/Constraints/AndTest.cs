// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class AndTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new AndConstraint(new GreaterThanConstraint(40), new LessThanConstraint(50));
            expectedDescription = "greater than 40 and less than 50";
            stringRepresentation = "<and <greaterthan 40> <lessthan 50>>";
        }

		object[] SuccessData = new object[] { 42 };
	
		object[] FailureData = new object[] { 37, 53 };

		string[] ActualValues = new string[] { "37", "53" };

		[Test]
        public void CanCombineTestsWithAndOperator()
        {
            Assert.That(42, new GreaterThanConstraint(40) & new LessThanConstraint(50));
        }
    }
}