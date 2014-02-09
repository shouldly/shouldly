// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using NUnit.Framework.Constraints;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class NotTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new NotConstraint( new EqualConstraint(null) );
            expectedDescription = "not null";
            stringRepresentation = "<not <equal null>>";
        }

        internal object[] SuccessData = new object[] { 42, "Hello" };
            
        internal object[] FailureData = new object [] { null };

        internal string[] ActualValues = new string[] { "null" };

        [Test, ExpectedException(typeof(AssertionException), ExpectedMessage = "ignoring case", MatchType = MessageMatch.Contains)]
        public void NotHonorsIgnoreCaseUsingConstructors()
        {
            Assert.That("abc", new NotConstraint(new EqualConstraint("ABC").IgnoreCase));
        }

        [Test,ExpectedException(typeof(AssertionException),ExpectedMessage="ignoring case",MatchType=MessageMatch.Contains)]
        public void NotHonorsIgnoreCaseUsingPrefixNotation()
        {
            Assert.That( "abc", Is.Not.EqualTo( "ABC" ).IgnoreCase );
        }

        [Test,ExpectedException(typeof(AssertionException),ExpectedMessage="+/-",MatchType=MessageMatch.Contains)]
        public void NotHonorsTolerance()
        {
            Assert.That( 4.99d, Is.Not.EqualTo( 5.0d ).Within( .05d ) );
        }

        [Test]
        public void CanUseNotOperator()
        {
            Assert.That(42, !new EqualConstraint(99));
        }
    }
}