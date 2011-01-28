// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class ExactTypeTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new ExactTypeConstraint(typeof(D1));
            expectedDescription = string.Format("<{0}>", typeof(D1));
            stringRepresentation = string.Format("<typeof {0}>", typeof(D1));
        }

        object[] SuccessData = new object[] { new D1() };
        
        object[] FailureData = new object[] { new B(), new D2() };

        string[] ActualValues = new string[]
            {
                "<NUnit.Framework.Constraints.B>",
                "<NUnit.Framework.Constraints.D2>"
            };
    }

    [TestFixture]
    public class InstanceOfTypeTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new InstanceOfTypeConstraint(typeof(D1));
            expectedDescription = string.Format("instance of <{0}>", typeof(D1));
            stringRepresentation = string.Format("<instanceof {0}>", typeof(D1));
        }

        object[] SuccessData = new object[] { new D1(), new D2() };

        object[] FailureData = new object[] { new B() };

        string[] ActualValues = new string[]
            {
                "<NUnit.Framework.Constraints.B>"
            };
    }

    [TestFixture]
    public class AssignableFromTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new AssignableFromConstraint(typeof(D1));
            expectedDescription = string.Format("assignable from <{0}>", typeof(D1));
            stringRepresentation = string.Format("<assignablefrom {0}>", typeof(D1));
        }

        object[] SuccessData = new object[] { new D1(), new B() };
            
        object[] FailureData = new object[] { new D2() };

        string[] ActualValues = new string[]
            {
                "<NUnit.Framework.Constraints.D2>"
            };
    }

    [TestFixture]
    public class AssignableToTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new AssignableToConstraint(typeof(D1));
            expectedDescription = string.Format("assignable to <{0}>", typeof(D1));
            stringRepresentation = string.Format("<assignableto {0}>", typeof(D1));
        }
        
        object[] SuccessData = new object[] { new D1(), new D2() };
            
        object[] FailureData = new object[] { new B() };

        string[] ActualValues = new string[]
            {
                "<NUnit.Framework.Constraints.B>"
            };
    }

    class B { }

    class D1 : B { }

    class D2 : D1 { }

    [TestFixture]
    public class AttributeExistsConstraintTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new AttributeExistsConstraint(typeof(TestFixtureAttribute));
            expectedDescription = "type with attribute <NUnit.Framework.TestFixtureAttribute>";
            stringRepresentation = "<attributeexists NUnit.Framework.TestFixtureAttribute>";
        }

        object[] SuccessData = new object[] { typeof(AttributeExistsConstraintTest) };
            
        object[] FailureData = new object[] { typeof(D2) };

        string[] ActualValues = new string[]
            {
                "<NUnit.Framework.Constraints.D2>"
            };

        [Test, ExpectedException(typeof(System.ArgumentException))]
        public void NonAttributeThrowsException()
        {
            new AttributeExistsConstraint(typeof(string));
        }

        [Test]
        public void AttributeExistsOnMethodInfo()
        {
            Assert.That(
                System.Reflection.MethodInfo.GetCurrentMethod(),
                new AttributeExistsConstraint(typeof(TestAttribute)));
        }

        [Test, Description("my description")]
        public void AttributeTestPropertyValueOnMethodInfo()
        {
            Assert.That(
                System.Reflection.MethodInfo.GetCurrentMethod(),
                Has.Attribute(typeof(DescriptionAttribute)).Property("Description").EqualTo("my description"));
        }
    }
}